using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GTANetworkAPI;
using Newtonsoft.Json;
using Whistler.Core;
using Whistler.Entities;
using Whistler.Families.FamilyMenu;
using Whistler.Families.FamilyMP.DTO;
using Whistler.Families.FamilyMP.Models;
using Whistler.Families.FamilyMP.Models.ModelsMP;
using Whistler.Families.FamilyMP.Models.MPModels;
using Whistler.Families.FamilyWars;
using Whistler.Families.Models;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Families.FamilyMP
{
    class ManagerMP : Script
    {
        private static Dictionary<int, FamilyMPModel> _familyMPs = new Dictionary<int, FamilyMPModel>();
        public static FamilyMPModel CurrentMP;
        public ManagerMP()
        {
            LoadMPFromDB();
            Timers.Start(5 * 60 * 1000, CheckStartBattles);
            CheckStartBattles();
            IslandCaptureMP.Init();
            FamilyMenuManager.FamilyLoad += LoadAllMP;
        }
        private static void LoadMPFromDB()
        {
            DataTable result = MySQL.QueryRead("SELECT * FROM `familymp` where `date` > @prop0", MySQL.ConvertTime(DateTime.Now.AddDays(-3)));
            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    FamilyMPModel mp = FabricMp(row, (FamilyMPType)Convert.ToInt32(row["type"]));
                    _familyMPs.Add(mp.ID, mp);
                }
            }
        }
        private static FamilyMPModel FabricMp(DataRow row, FamilyMPType type)
        {
            switch (type)
            {
                case FamilyMPType.IslandCapture:
                    return new IslandCaptureMP(row);
                case FamilyMPType.BusinessWar:
                    return new BusinessWarMP(row);
            }
            return null;
        }
        private static void LoadAllMP(ExtPlayer player, Family family)
        {
            SafeTrigger.ClientEvent(player, "family:loadMP", JsonConvert.SerializeObject(_familyMPs.Select(item => new FamilyMPModelDTO(item.Value))));
        }
        [Command("createislandcapt")]
        public static void CreateIslandCaptMP(ExtPlayer player, int month, int day, int hour, int minute)
        {
            if (!Group.CanUseAdminCommand(player, "createislandcapt")) return;

            var time = new DateTime(DateTime.Now.Year, month, day, hour, minute, 0);
            if (DateTime.Now.AddMinutes(0) > time)
            {
                Notify.SendError(player, "You can create a MP at least 30 minutes before the start ");
                return;
            }
            if (_familyMPs.FirstOrDefault(item => time.AddHours(-1) < item.Value.Date && item.Value.Date < time.AddHours(1)).Value != null)
            {
                Notify.SendError(player, "At this point there is already MP");
                return;
            }

            FamilyMPModel mp = new IslandCaptureMP(time, BattleLocation.TheCayoPerico);
            _familyMPs.Add(mp.ID, mp);
            FamilyMenu.FamilyMenuManager.UpdateFamilyMP(mp);
            CheckStartBattles();
            Notify.SendSuccess(player, $"You have successfully created a war on the island.");
            GameLog.Admin(player.Name, $"createislandcapt({MySQL.ConvertTime(time)})", "");
        }
        [Command("createbizwar")]
        public static void CreateBusinessWar(ExtPlayer player, int businessId, int month, int day, int hour, int minute)
        {
            if (!Group.CanUseAdminCommand(player, "createbizwar")) return;

            DateTime time = new DateTime(DateTime.Now.Year, month, day, hour, minute, 0);

            if (DateTime.Now.AddMinutes(30) > time)
            {
                Notify.SendError(player, "You can create a MP at least 30 minutes before the start ");
                return;
            }
            if (_familyMPs.FirstOrDefault(item => time.AddHours(-1) < item.Value.Date && item.Value.Date < time.AddHours(1)).Value != null)
            {
                Notify.SendError(player, "At this point there is already MP ");
                return;
            }
            Business biz = BusinessManager.GetBusiness(businessId);
            if (biz == null)
            {
                Notify.SendError(player, "Business with such an ID does not exist");
                return;
            }

            if (biz.FamilyPatronage > 0)
            {
                Notify.SendError(player, "The business is not free");
                return;
            }
            var location = BattleLocation.RedwoodLights;
            if (WarManager.LocationIsOccupied(location, time))
            {
                Notify.SendError(player, "At that time there is a bizwar");
                return;
            }
            FamilyMPModel mp = new BusinessWarMP(time, location, businessId);
            _familyMPs.Add(mp.ID, mp);
            FamilyMenu.FamilyMenuManager.UpdateFamilyMP(mp);
            CheckStartBattles();
            Notify.SendSuccess(player, $"You have successfully created a war {biz.Name}.");
            GameLog.Admin(player.Name, $"createbizwar({businessId},{MySQL.ConvertTime(time)})", "");
        }

        private static void CheckStartBattles()
        {
            foreach (var mp in _familyMPs.Values.Where(item => item.Date > DateTime.Now && item.Date < DateTime.Now.AddMinutes(30) && !item.IsFinished && !item.IsPlaying))
            {
                if (mp._timer == null)
                    mp._timer = Timers.StartOnce((int)(mp.Date - DateTime.Now).TotalMilliseconds, () => StartBattle(mp));
                if ((int)(mp.Date - DateTime.Now).TotalMinutes > 0)
                    Chat.AdminToAll($"Event \"{mp.NameMP}\" It will go through {(int)(mp.Date - DateTime.Now).TotalMinutes} Minute!");
                else
                    Chat.AdminToAll($"Event \"{mp.NameMP}\" It will go through {(int)(mp.Date - DateTime.Now).TotalSeconds} Seconds!");

            }
        }

        private static void StartBattle(FamilyMPModel mp)
        {
            mp._timer = null;
            if (_familyMPs.FirstOrDefault(ItemType => ItemType.Value.IsPlaying).Value != null)
                return;
            if (CurrentMP != null)
                return;
            if (mp.TryStartMP())
                CurrentMP = mp;
        }
        public static bool PlayerDeath(ExtPlayer player, ExtPlayer killer, uint weapon)
        {
            return CurrentMP?.PlayerDeath(player, killer, weapon) ?? false;
        }
        public static void OnPlayerDisconnected(ExtPlayer player)
        {
            IslandCaptureMP.PlayerDisconnected(player);
        }
    }
}