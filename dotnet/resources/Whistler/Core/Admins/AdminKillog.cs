using System;
using System.Linq;
using System.Text.RegularExpressions;
using GTANetworkAPI;
using Whistler.Core.ReportSystem;
using Whistler.Entities;
using Whistler.GUI;
using Whistler.SDK;

namespace Whistler.Core.Admins
{
    public class AdminKillog : Script
    {
        private const string AdminKillogDataName = "adm_killog_enabled";

        public AdminKillog()
        {
            Admin.SetPlayerToAdminGroup += OnAdminLoad;
            Admin.DeletePlayerFromAdminGroup += OnAdminUnload;
        }

        private static bool CanUseKillog(ExtPlayer player)
        {
            return player != null && player.Logged() && Group.CanUseAdminCommand(player, "killog", false);
        }

        private static void OnAdminLoad(ExtPlayer player)
        {
            if (!CanUseKillog(player)) return;
            SafeTrigger.SetData(player, AdminKillogDataName, true);
            CaptureUI.EnableKillLog(player);
        }

        private static void OnAdminUnload(ExtPlayer player)
        {
            if (player == null) return;
            SafeTrigger.SetData(player, AdminKillogDataName, false);
            CaptureUI.DisableKillog(player, true);
        }

        [Command("killog")]
        public static void ToggleAdminKillog(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "killog")) return;

                if (!player.HasData(AdminKillogDataName))
                {
                    SafeTrigger.SetData(player, AdminKillogDataName, true);
                }
                else
                {
                    var currentToggle = Convert.ToBoolean(player.GetData<bool>(AdminKillogDataName));
                    SafeTrigger.SetData(player, AdminKillogDataName, !currentToggle);
                }
                var toggle = Convert.ToBoolean(player.GetData<bool>(AdminKillogDataName));
                if (toggle)
                {
                    Notify.SendInfo(player, "Killog");
                    CaptureUI.EnableKillLog(player);
                }
                else
                {
                    Notify.SendInfo(player, "Killog is turned off");
                    CaptureUI.DisableKillog(player);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + ex);
            }
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(ExtPlayer victim, ExtPlayer killer, uint weapon)
        {
            try
            {
                var adminWithKillog = ReportManager.Admins
                    .Where(a => a != null
                                && a.Logged()
                                && a.HasData(AdminKillogDataName)
                                && a.GetData<bool>(AdminKillogDataName)
                                && CanUseKillog(a));
                
                foreach (var admin in adminWithKillog)
                    CaptureUI.AddKillogEmptyItem(admin, killer, victim, weapon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + ex);
            }
        }
    }
}
