using GTANetworkAPI;
using System;
using System.IO;
using System.Collections.Generic;
using Whistler.GUI;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using Newtonsoft.Json;
using Whistler.SDK;
using Whistler.MoneySystem;
using Whistler.Houses;
using Whistler.Fractions;
using ServerGo.Casino.Business;
using ServerGo.Casino.ChipModels;
using Whistler.ClothesCustom;
using Whistler.VehicleSystem;
using Whistler.VehicleSystem.Models;
using Whistler.Helpers;
using Whistler.Core.Admins;
using Whistler.VehicleSystem.Models.VehiclesData;
using Whistler.Inventory;
using Whistler.Inventory.Enums;
using Whistler.Core.CustomSync;
using Whistler.Core.Weather;
using Whistler.Customization;
using Whistler.Common;
using Whistler.Entities;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Whistler.Core
{
    class Commands: Script
    {
        private static Random rnd = new Random();
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(Commands));
       
        [Command("getvariable")]
        public static void CMD_updateBiz(ExtPlayer player, int id, string variable)
        {
            if (!Group.CanUseAdminCommand(player, "getvariable")) return;
            try
            {
                var vehicle = NAPI.Pools.GetAllVehicles().Where(a => a.Value == id).FirstOrDefault();
                if (vehicle == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Wrong ID from the car!", 3000);
                    return;
                };
                SafeTrigger.ClientEvent(player,"viewVariableData", vehicle, variable);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_updateBiz: {e}");
            }
        }

        [Command("updatepassword")]
        public static void CMD_updatePassword(ExtPlayer player, string loginOrEmail, string newPassword)
        {
            if (!Group.CanUseAdminCommand(player, "updatepassword")) return;
            var ExtPlayer = Main.GetExtPlayerByPredicate(item => item.Account.Email == loginOrEmail || item.Account.Login == loginOrEmail);
            if (ExtPlayer == null) 
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The account was not found", 3000);
            else
            {
                ExtPlayer.Account.changePassword(newPassword);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "The password is successfully changed", 3000);
            }
        }

        [Command("rapeon")]
        public static void CMD_RapeTargetOn(ExtPlayer player, int id)
        {
            if (!Group.CanUseAdminCommand(player, "rape")) return;
            try
            {
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                
                if(target.IsLogged() && player.IsLogged())
                {
                    if(target.Position.DistanceTo(player.Position) > 4)
                    {
                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, "The goal is too far", 3000);
                        return;
                    }
                    var pos = player.Position + new Vector3(0, 0, -0.7);
                    SafeTrigger.ClientEvent(target, "rape:target", pos, player.Value);
                    SafeTrigger.ClientEvent(player,"rape:king", pos, target.Value);
                    SafeTrigger.SetData(player, "rape:target", target);
                    player.SetSkin(0x5442C66B);
                } 
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_RapeTarget: {e}");
            }
        }

        [Command("rapeoff")]
        public static void CMD_RapeTargetOff(ExtPlayer player)
        {
            if (!Group.CanUseAdminCommand(player, "rape")) return;
            try
            {
                if (player.HasData("rape:target"))
                {
                    ExtPlayer target = player.GetData<ExtPlayer>("rape:target");
                    if (target.IsLogged())
                    {
                        SafeTrigger.ClientEvent(target, "rape:off");
                    }
                }
                if (player.IsLogged())
                {
                    SafeTrigger.ClientEvent(player,"rape:off");
                    player.Character.Customization.Apply(player);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_RapeTarget: {e}");
            }
        }

        [Command("updatebiz")]
        public static void CMD_updateBiz(ExtPlayer player, int type)
        {
            try
            {
                BusinessManager.UpdateBusinessCommand(type, false);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_updateBiz: {e}");
            }
        }

        [Command("updatebizprice")]
        public static void CMD_updateBizPrice(ExtPlayer player, int type)
        {
            try
            {
                BusinessManager.UpdateBusinessCommand(type, true);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_updateBizPrice: {e}");
            }
        }

        #region AdminCommands

        [Command("greenscreen")]
        public static void CMD_Test(ExtPlayer player, bool flag = true)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "greenscreen")) return;
                SafeTrigger.ClientEvent(player, "greenscreen:openedMenu", flag);                
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_Test: {e}");
            }
        }

        [Command("checkinv")]
        public static void CMD_CheckInv(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "checkinv")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if(target == null){
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                Admin.CheckInventory(player, target);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_updateBiz: {e}");
            }
        }

        [Command("createadmincar")]
        public static void CMD_CreateAdminCar(ExtPlayer player, string model)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "createadmincar")) return;
                var vehData = new AdminSaveVehicle(model, player.Position, player.Rotation, 0, 0);
                vehData.Spawn();
                GameLog.Admin(player.Name, $"createadmincar({model})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_CreateAdminCar: {e}");
            }
        }

        [Command("saveadmincar")]
        public static void CMD_SaveAdminCar(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "saveadmincar")) return;
                if (player.Vehicle == null)
                    return;
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (vehicle == null) return;
                if (vehicle.Data.OwnerType != OwnerType.AdminSave)
                    return;
                vehicle.Data.Position = player.Vehicle.Position;
                vehicle.Data.Rotation = player.Vehicle.Rotation;
                vehicle.Data.Save();
                GameLog.Admin(player.Name, $"saveadmincar({vehicle.Data.ID})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SaveAdminCar: {e}");
            }
        }

        [Command("deleteadmincar")]
        public static void CMD_DeleteAdminCar(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "deleteadmincar")) return;
                if (player.Vehicle == null)
                    return;
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (vehicle == null) return;

                if (vehicle.Data.OwnerType != OwnerType.AdminSave)
                    return;
                int id = vehicle.Data.ID;
                vehicle.Data.DeleteVehicle(vehicle);
                GameLog.Admin(player.Name, $"deleteadmincar({id})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_DeleteAdminCar: {e}");
            }
        }

        [Command("vct")]
        public static void CMD_ClientTrafficOn(ExtPlayer player, int id, int status = 1)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "vct")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                    return;
                SafeTrigger.ClientEvent(target, "ClientTrafficChangeStatus", status);
                if (status == 1)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have switched the traffic around {target.Name}", 3000);
                else
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have switched off the traffic{target.Name}", 3000);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ClientTrafficOn: {e}");
            }
        }

        [Command("delfracmd")]
        public static void CMD_ChangeFractionCommandAccess(ExtPlayer player, int fraction, string command)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "changefracmdaccess")) return;

                if (Manager.GetFraction(fraction)?.RemoveCommand(command) ?? false)
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You deleted the team {command}", 3000);
                    GameLog.Admin(player.Name, $"delfracmd({fraction},{command})", "");
                }
                else
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This team has no such team", 3000);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ChangeFractionCommandAccess: {e}");
            }
        }

        [Command("addfracmd")]
        public static void CMD_AddFractionCommandAccess(ExtPlayer player, int fraction, string command, int minRank)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "addfracmd")) return;


                if (Manager.GetFraction(fraction) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The faction has not been found ", 3000);
                    return;
                }

                if (Manager.GetFraction(fraction).Commands.ContainsKey(command))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Such a team already has this parliamentary group ", 3000);
                    return;
                }

                if (Manager.GetFraction(fraction)?.AddCommand(command, minRank) ?? false)
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"A team added'{command}' Fracture {fraction} With a minimum rank = {minRank}", 3000);
                    GameLog.Admin(player.Name, $"addfracmd({fraction},{command},rank:{minRank})", "");
                }
                else
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You cannot install min.", 3000);

            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_AddFractionCommandAccess: {e}");
            }
        }

        [Command("givemark")]
        public static void CMD_givemark(ExtPlayer player, int id){
            try{
                if (!Group.CanUseAdminCommand(player, "tpmark")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null){
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                SafeTrigger.ClientEvent(player, "GetWPAdmin");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_GiveStreamerCar: {e}");
            }
        }

        [Command("givecar")]
        public static void CMD_GiveStreamerCar(ExtPlayer player, int targetId, string model, int c1, int c2)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "givecar")) return;

                ExtPlayer target = Trigger.GetPlayerByUuid(targetId);
                if (target == null || !target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                var house = HouseManager.GetHouse(target, true);
                if (house == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player does not have his own home", 3000);
                    return;
                }
                if (house.GarageID == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player has no garage", 3000);
                    return;
                }
                var vModel = VehicleManager.GetModelName(model);

                if (string.IsNullOrEmpty(vModel))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Such a machine does not exist", 3000);
                    return;
                }
                
                var vehData = VehicleManager.Create(target.Character.UUID, vModel, new Color(c1), new Color(c2), status: PropBuyStatus.Given);
                MainMenu.SendProperty(target);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully published a car {vModel} Player {target.Name}", 3000);

                GarageManager.SendVehicleIntoGarage(vehData);
                GameLog.Admin(player.Name, $"givecar({model})", target.Name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_GiveStreamerCar: {e}");
            }
        }

        [Command("givefamcar")]
        public static void CMD_GiveStreamerFamilyCar(ExtPlayer player, int targetId, string model, int c1, int c2)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "givecar")) return;

                ExtPlayer target = Trigger.GetPlayerByUuid(targetId);
                if (target == null || !target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                var family = target.GetFamily();
                if (family == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name}It's not in the family!", 3000);
                    return;
                }
                var vModel = VehicleManager.GetModelName(model);

                if (string.IsNullOrEmpty(vModel))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Such a machine does not exist", 3000);
                    return;
                }

                var vehData = VehicleManager.Create(family.Id, vModel, new Color(c1), new Color(c2), status: PropBuyStatus.Given, typeOwner: OwnerType.Family);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully published a car{vModel} Family {family.Name}", 3000);

                GarageManager.SendVehicleIntoGarage(vehData);
                GameLog.Admin(player.Name, $"givefamcar({model})", family.Name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_GiveStreamerFamilyCar: {e}");
            }
        }

        [Command("delgivencar")]
        public static void CMD_DelGivenCar(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged() || !Group.CanUseAdminCommand(player, "givecar")) return;

                ExtVehicle extVehicle = Trigger.GetVehicleById(id);
                if (extVehicle == null) return;
                if (((extVehicle.Data as PersonalBaseVehicle)?.PropertyBuyStatus ?? PropBuyStatus.Unknown) == PropBuyStatus.Given)
                {
                    GameLog.Admin(player.Name, $"delgivencar({extVehicle.Data.ModelName},{extVehicle.Data.ID})", extVehicle.Data.GetHolderName());
                    VehicleManager.Remove(extVehicle.Data.ID);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_DelGivenCar: {e}");
            }
        }        

        [Command("checkchips")]
        public static void CMD_CheckChips(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "checkchips")) return;
                
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null) return;
                var chips = target.Character.CasinoChips;
                if (chips != null || target.Character.CasinoChips.Length > 0)
                {
                    var chipList = new List<Chip>();
                    for (var i = 0; i < 5; i++)
                    for (var j = 0; j < target.Character.CasinoChips[i]; j++)
                    {
                        chipList.Add(ChipFactory.Create((ChipType)i));
                    }
                    var total = chipList.Sum(c => c.Value);
                    Chat.SendTo(player,$"Chips: [{chips[0]}, {chips[1]}, {chips[2]}, {chips[3]}, {chips[4]}] balance: {total}");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_CheckChips: {e}");
            }
        }

        [Command("playervehnumber")]
        public static void CMD_ChangeNumberPlate(ExtPlayer player, int id, string newNumber)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "vehnumber")) return;

                ExtVehicle vehicle = Trigger.GetVehicleById(id);
                if (vehicle == null) return;

                if (!VehicleManager.ChangeNumber(vehicle.Data.ID, newNumber, true)) 
                    Chat.SendTo(player,$"Car was not found or bad number");
                else
                {
                    Chat.SendTo(player, $"Number for the car changed #{id} to {newNumber}");
                    GameLog.Admin(player.Name, $"playervehnumber({vehicle.Data.ID},{newNumber})", "");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ChangeNumberPlate: {e}");
            }
        }

        [Command("vehnumber")]
        public static void CMD_ChangeNumberPlate(ExtPlayer player, string newNumber)
        {
            try
            {
                newNumber = newNumber.ToUpper();
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "vehnumber")) return;
                if (!player.IsInVehicle)
                {
                    Chat.SendTo(player,"use (playervehnumber [oldNumber] [newNumber]) or sit in car");
                    return;
                }
                var oldNumber = player.Vehicle.NumberPlate;
                player.Vehicle.NumberPlate = newNumber;
                Chat.SendTo(player,$"Number changed by {oldNumber} Zu {newNumber}");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ChangeNumberPlate: {e}");
            }
        }

        [Command("gethwid")]
        public static void CMD_gethwid(ExtPlayer client, int ID)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "setvehdirt")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(ID);
                if (target == null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                string hwid = target.Session.HWID;
                Chat.SendTo(client, $"Real Hwid y {target.Name}: {hwid}");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_gethwid: {e}");
            }
        }
        
        [Command("getsocialclub")]
        public static void CMD_getsc(ExtPlayer client, int ID)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "setvehdirt")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(ID);
                if (target == null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                Chat.SendTo(client, $"Real SocialClub{target.Name}: {client.SocialClubId}");
            }
            catch(Exception e)
            {
                _logger.WriteError($"CMD_getsc: {e}");
            }
        }

        [Command("giveammo")]
        public static void CMD_ammo(ExtPlayer client, int ID, int type, int amount = 1)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "giveammo")) return;

                var target = Trigger.GetPlayerByUuid(ID);
                if (target == null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                var item = ItemsFabric.CreateAmmo((ItemNames)(118 + type), amount, true);
                if (item == null)
                    return;
                if (!target.GetInventory().AddItem(item))
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Not enough space in the inventory ", 3000);
                GameLog.Admin(client.Name, $"giveammo(t:{type},cnt:{amount})", target.Name);
            }
            catch(Exception e)
            {
                _logger.WriteError($"CMD_ammo: {e}");
            }
        }
        [Command("gun")]
        public static void CMD_giveWeaponOrAmmo(ExtPlayer admin, int id, string option, string wnameOrType, int amount = 1)
        {
            try
            {
                // Validate admin permissions
                if (!Group.CanUseAdminCommand(admin, "gun")) return;

                // Validate player ID
                var targetPlayer = Trigger.GetPlayerByUuid(id);
                if (targetPlayer == null)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                        "A player with this ID was not found.", 3000);
                    return;
                }

                option = option.ToLower();

                // Handle weapon option
                if (option == "weapon")
                {
                    wnameOrType = wnameOrType.Replace("_", " ").ToUpper();
                    WeaponHash weaponHash = (WeaponHash)NAPI.Util.GetHashKey(wnameOrType);

                    if (weaponHash == 0)
                    {
                        Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                            "Invalid weapon name.", 3000);
                        return;
                    }

                    // Give the weapon with default ammo (9999 by default)
                    targetPlayer.GiveWeapon(weaponHash, 9999);

                    Notify.Send(admin, NotifyType.Success, NotifyPosition.BottomCenter,
                        $"You have given the weapon {wnameOrType} to {targetPlayer.Name}.", 3000);

                    Chat.SendToAdmins(1,
                        $"~r~[Admin] {admin.Name} has given the weapon {wnameOrType} to {targetPlayer.Name}.");
                    GameLog.Admin($"{admin.Name}", $"giveweapon({targetPlayer.Name}, {wnameOrType})", $"");
                }
                // Handle ammo option
                else if (option == "ammo")
                {
                    int ammoType;
                    if (!int.TryParse(wnameOrType, out ammoType) || ammoType < 0)
                    {
                        Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                            "Invalid ammo type.", 3000);
                        return;
                    }

                    var ammoItem = ItemsFabric.CreateAmmo((ItemNames)(118 + ammoType), amount, true);
                    if (ammoItem == null)
                    {
                        Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                            "Failed to create ammo. Invalid type or amount.", 3000);
                        return;
                    }

                    if (!targetPlayer.GetInventory().AddItem(ammoItem))
                    {
                        Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                            "Not enough inventory space in the target player.", 3000);
                        return;
                    }

                    Notify.Send(admin, NotifyType.Success, NotifyPosition.BottomCenter,
                        $"You have given {amount} ammo of type {ammoType} to {targetPlayer.Name}.", 3000);

                    Chat.SendToAdmins(1,
                        $"~r~[Admin] {admin.Name} has given {amount} ammo of type {ammoType} to {targetPlayer.Name}.");
                    GameLog.Admin($"{admin.Name}", $"giveammo({targetPlayer.Name}, t:{ammoType}, cnt:{amount})", $"");
                }
                else
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid option. Use 'weapon' or 'ammo'.", 3000);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"CMD_giveWeaponOrAmmo\":\n{e}");
            }
        }
        [Command("gunall")]
        public static void CMD_giveWeaponAndAmmoToRadius(ExtPlayer admin, float radius, string weaponName, int ammoCount = 9999)
        {
            try
            {
                // Validate admin permissions
                if (!Group.CanUseAdminCommand(admin, "gunall")) return;

                // Validate radius
                if (radius <= 0)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid radius. Please provide a valid radius.", 3000);
                    return;
                }

                // Validate weapon
                weaponName = weaponName.Replace("_", " ").ToUpper();
                WeaponHash weaponHash = (WeaponHash)NAPI.Util.GetHashKey(weaponName);
                if (weaponHash == 0)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid weapon name. Please provide a valid weapon.", 3000);
                    return;
                }

                // Validate ammo count
                if (ammoCount <= 0)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid ammunition amount. Please provide a valid number.", 3000);
                    return;
                }

                // Get all players within the specified radius
                Vector3 adminPosition = admin.Position;
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(adminPosition) <= radius &&
                    p != admin);

                if (!playersInRadius.Any())
                {
                    Notify.Send(admin, NotifyType.Warning, NotifyPosition.BottomCenter,
                        "No players found within the specified radius.", 3000);
                    return;
                }

                // Give the weapon and ammo to all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        extPlayer.GiveWeapon(weaponHash, ammoCount);

                        // Notify the player
                        Notify.Send(extPlayer, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"You have received {weaponName} with {ammoCount} ammunition.", 3000);
                    }
                }

                // Notify the admin
                Notify.Send(admin, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have given {weaponName} with {ammoCount} ammunition to all players within a {radius}m radius.", 3000);

                // Log and notify all admins
                Chat.SendToAdmins(1,
                    $"~r~[Admin] {admin.Name} has given {weaponName} with {ammoCount} ammo to all players within a {radius}m radius.");
                GameLog.Admin($"{admin.Name}", $"gunall(radius:{radius}, weapon:{weaponName}, ammo:{ammoCount})", $"");
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"CMD_giveWeaponAndAmmoToRadius\":\n{e}");
            }
        }

        [Command("giveammoc")]
        public static void CMD_ammoc(ExtPlayer client, int ID, int type, int amount = 1)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "giveammoc")) return;

                var target = Trigger.GetPlayerByUuid(ID);
                if (target == null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                var item = ItemsFabric.CreateAmmo((ItemNames)(118 + type), amount, false);
                if (item == null)
                    return;
                if (!target.GetInventory().AddItem(item))
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Not enough space in the inventory ", 3000);
                GameLog.Admin(client.Name, $"giveammoc(t:{type},cnt:{amount})", target.Name);
            }
            catch(Exception e)
            {
                _logger.WriteError($"CMD_ammoc: {e}");
            }
        }
		
        [Command("adm")]
        public static void ACMD_redname(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "adm")) return;

                if (!player.HasSharedData("ADM_NAME") || !player.GetSharedData<bool>("ADM_NAME"))
                {
                    Chat.SendTo(player, "ADM_NAME ON");
                    SafeTrigger.SetSharedData(player, "ADM_NAME", true);
                }
                else
                {
                    Chat.SendTo(player, "ADM_NAME OFF");
                    SafeTrigger.SetSharedData(player, "ADM_NAME", false);
                }

            }
            catch (Exception e)
            {
                _logger.WriteError($"ACMD_redname: {e}");
            }
        }
		
        [Command("hidenick")]
        public static void CMD_hidenick(ExtPlayer player) {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                if (!player.HasSharedData("HideNick") || !player.GetSharedData<bool>("HideNick"))
                {
                    Chat.SendTo(player, "HideNick ON");
                    SafeTrigger.SetSharedData(player, "HideNick", true);
                }
                else
                {
                    Chat.SendTo(player, "HideNick OFF");
                    SafeTrigger.SetSharedData(player, "HideNick", false);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_hidenick: {e}");
            }

        }

        [Command("givedonate")]
        public static void CMD_GiveDonatePoints(ExtPlayer player, int id, int amount)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                Admin.GiveDonatePoints(player, target, amount);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_GiveDonatePoints: {e}");
            }
        }

        [Command("checkprop")]
        public static void CMD_checkProperety(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "checkprop")) return;

                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!target.IsLogged()) return;
                var house = Houses.HouseManager.GetHouse(target);
                if (house != null)
                {
                    if (house.OwnerID == target.Character.UUID)
                    {
                        Chat.SendTo(player, $"The player has a house worth:{house.Price}$ Class '{Houses.HouseManager.HouseTypeList[house.Type].Name}'");
                    }
                    else
                        Chat.SendTo(player, $"The player is filled into the house too{house.GetHouseOwnerName()} wert {house.Price}$");
                }
                else
                    Chat.SendTo(player,"The player has no home");
                var targetVehicles = VehicleManager.getAllHolderVehicles(target.Character.UUID, OwnerType.Personal);
                if (targetVehicles.Count() > 0)
                {
                    foreach (var veh in targetVehicles)
                        Chat.SendTo(player, $"The player has a car '{VehicleManager.Vehicles[veh].ModelName}' With a number '{VehicleManager.Vehicles[veh].Number}'");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_checkProperety: {e}");
            }
        }
        
        [Command("moneypickup")]
        public static void CMD_createPickup(ExtPlayer player, int money)
        {
            try
            {
                if (player.Character.AdminLVL < 7) return;
                var pos = player.Position - new Vector3(0, 0, 1);
                BonusPickup.Create(pos, money);
                GameLog.Admin(player.Name, "moneypickup", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_createPickup: {e}");
            }
        }
        
        [Command("checkpickups")]
        public static void CMD_checkPickup(ExtPlayer player)
        {
            try
            {
                if (player.Character.AdminLVL < 7) return;
                NAPI.Chat.SendChatMessageToPlayer(player, $"Left {BonusPickup.Pickups.Count}/{BonusPickup.Counter}");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_checkPickup: {e}");
            }
        }

        [Command("pa")]
        public void PlayeAnim(ExtPlayer player, string dict, string name, int flag)
        {
            if (!Group.CanUseAdminCommand(player, "id")) return;
            AnimSync.PlayAnimGo(player, dict, name, (AnimFlag)flag);
        }

        [Command("sa")]
        public void StopAnim(ExtPlayer player)
        {
            if (!Group.CanUseAdminCommand(player, "id")) return;
            AnimSync.StopAnimGo(player);
        }

      
        [Command("id", "/id [name/id]")]
        public static void CMD_checkId(ExtPlayer player, string target)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "id")) return;

                int id;
                if (Int32.TryParse(target, out id))
                {
                    var sendPlayer = Trigger.GetPlayerByUuid(id);
                    if (sendPlayer.IsLogged() && (sendPlayer.Character.AdminLVL < 7 || sendPlayer.Character.AdminLVL <= player.Character.AdminLVL)) 
                        Chat.SendTo(player, $"ID: {sendPlayer.Value} | STATIC ID: {sendPlayer.Character.UUID} | {sendPlayer.Name}");
                    else
                        Chat.SendTo(player,"A player with such an ID was not found");
                }
                else
                {
                    var players = 0;
                    Main.ForEachAllPlayer((p) =>
                    {
                        if (p.Character.AdminLVL >= 7 && player.Character.AdminLVL < p.Character.AdminLVL) return;
                        if (p.Name.ToUpper().Contains(target.ToUpper()))
                        {
                            Chat.SendTo(player, $"ID: {p.Value} | STATIC ID: {p.Character.UUID} | {p.Name}");
                            players++;
                        }
                    });
                    if (players == 0)
                        Chat.SendTo(player,"The player was not found");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_checkId: {e}");
            }
        }

        [Command("setdim")]
        public static void CMD_setDim(ExtPlayer player, int id, int dim)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "setdim")) return;

                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!target.IsLogged()) return;
                SafeTrigger.UpdateDimension(target, Convert.ToUInt32(dim));
                GameLog.Admin($"{player.Name}", $"setDim({dim})", $"{target.Name}");
            } catch(Exception e)
            {
                _logger.WriteError("setdim: " + e.ToString());
            }
        }

        [Command("setvehdim")]
        public static void CMD_setVehDim(ExtPlayer player, int id, int dim)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setdim")) return;
                var vehicle = NAPI.Pools.GetAllVehicles().FirstOrDefault(a => a.Value == id);
                if (vehicle == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                vehicle.Dimension = (uint)dim;
                GameLog.Admin($"{player.Name}", $"setVehDim({dim})", $"{vehicle.Value}");
            }
            catch (Exception e)
            {
                _logger.WriteError("setdim: " + e.ToString());
            }
        }

        [Command("checkdim")]
        public static void CMD_checkDim(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "checkdim")) return;

                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!target.IsLogged()) return;
                GameLog.Admin($"{player.Name}", $"checkDim", $"{target.Name}");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Measurement of the player - {target.Dimension.ToString()}", 4000);
            }
            catch (Exception e)
            {
                _logger.WriteError("checkdim: " + e.ToString());
            }
        }

        [Command("takeoffbiz")]
        public static void CMD_takeOffBusiness(ExtPlayer admin, int bizid)
        {
            try
            {
                if (!admin.IsLogged()) return;
                if (!Group.CanUseAdminCommand(admin, "takeoffbiz")) return;

                var biz = BusinessManager.BizList[bizid];
                string owner = biz.GetOwnerName();
                if (biz.TakeBusinessFromOwner(Convert.ToInt32(biz.SellPrice * 0.8), $"Compensation for business {biz.ID}", "The administrator has selected a company from you"))
                {
                    Notify.Send(admin, NotifyType.Info, NotifyPosition.BottomCenter, $"You have selected the business{owner}", 3000);
                    GameLog.Admin($"{admin.Name}", $"takeoffBiz({biz.ID})", owner);
                }
            }
            catch (Exception e) { _logger.WriteError("takeoffbiz: " + e.ToString()); }
        }
      

        [Command("removeobj")]
        public static void CMD_removeObject(ExtPlayer player)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "removeobj")) return;

                SafeTrigger.SetData(player, "isRemoveObject", true);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"The object is deleted", 3000);
            }
            catch (Exception e) { _logger.WriteError("removeobj: " + e.ToString()); }
        }

        [Command("unwarn")]
        public static void CMD_unwarn(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "unwarn")) return;

                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!target.IsLogged()) return;
                if (target.Character.Warns <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player has no warnings", 3000);
                    return;
                }

                target.Character.Warns--;
                GUI.MainMenu.SendStats(target);

                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You warned the player{target.Name}. The rest of the warning: {target.Character.Warns}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"Your warning is removed. Warns left are  {target.Character.Warns}", 3000);
                GameLog.Admin($"{player.Name}", $"unwarn", $"{target.Name}");
            }
            catch (Exception e) { _logger.WriteError("unwarn: " + e.ToString()); }
        }

        [Command("offunwarn")]
        public static void CMD_offunwarn(ExtPlayer player, string target)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "unwarn")) return;

                if (!Main.PlayerNames.ContainsValue(target))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player was not found", 3000);
                    return;
                }
                if (Trigger.GetPlayerByName(target) != null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Online player", 3000);
                    return;
                }

                var split = target.Split('_');
                var data = MySQL.QueryRead("SELECT warns FROM characters WHERE firstname = @prop0 AND lastname = @prop1", split[0], split[1]);
                var warns = 0;
                foreach (DataRow Row in data.Rows)
                {
                    warns = Convert.ToInt32(Row["warns"]);
                }

                if (warns <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player has no warnings", 3000);
                    return;
                }

                warns--;
                GameLog.Admin($"{player.Name}", $"offUnwarn", $"{target}");
                MySQL.Query("UPDATE characters SET warns = @prop0 WHERE firstname = @prop1 AND lastname = @prop2", warns, split[0], split[1]);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You warned the player {target}. The rest of the warnings: {warns}", 3000);
            }
            catch (Exception e) { _logger.WriteError("offunwarn: " + e.ToString()); }
        }

        [Command("rescar")]
        public static void CMD_respawnCar(ExtPlayer player)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "rescar")) return;
                if (!player.IsInVehicle) return;
                ExtVehicle extVehicle = player.Vehicle as ExtVehicle;
                switch (extVehicle.Data.OwnerType)
                {
                    case OwnerType.Personal:
                    case OwnerType.Family:
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player's personal car production function is currently deactivated", 3000);
                        break;
                    case OwnerType.Fraction:
                        extVehicle.Data.RespawnVehicle();
                        break;
                }

                GameLog.Admin($"{player.Name}", $"rescar", $"");
            }
            catch (Exception e) { _logger.WriteError("ResCar: " + e.ToString()); }
        }

        [Command("spawncar")]
        public static void CMD_spawnCar(ExtPlayer player, int carId)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "rescar")) return;

                ExtVehicle extVehicle = SafeTrigger.GetVehicleById(carId);
                
                if (extVehicle == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The car is not found", 3000);
                    return;
                }
                switch (extVehicle.Data.OwnerType)
                {
                    case OwnerType.Personal:
                    case OwnerType.Family:
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player's personal car restoration function is currently deactivated", 3000);
                        break;
                    case OwnerType.Fraction:
                        extVehicle.Data.RespawnVehicle();
                        break;
                }

                GameLog.Admin($"{player.Name}", $"rescar", $"");
            }
            catch (Exception e) { _logger.WriteError("SpawnCar: " + e.ToString()); }
        }


        [Command("vehp")]
        public static void CreateObject(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "createobject")) return;
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                //int passengerSize = NAPI.Vehicle.GetVehicleMaxPassengers((VehicleHash)NAPI.Util.GetHashKey(vehicle.GetModelName()));
                Console.WriteLine($"hash: {NAPI.Util.GetHashKey(vehicle.GetModelName())}");
                Console.WriteLine($"occ: {NAPI.Vehicle.GetVehicleOccupants(vehicle).ToArray()}");
                
                // foreach (var oc in extVehicle.AllOccupants)
                // {
                //     Console.WriteLine($"pass: {oc.Name}");
                // }

            }
            catch (Exception e) { _logger.WriteError("ResCar: " + e.ToString()); }
        }


        [Command("crob")]
        public static void CreateObject(ExtPlayer player, string model, float zOffset)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "createobject")) return;
                Console.WriteLine($"hash: {NAPI.Util.GetHashKey(model)}");
                var obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(model), player.Position + new Vector3(0, 0, zOffset), new Vector3(), 255, player.Dimension);

            }
            catch (Exception e) { _logger.WriteError("ResCar: " + e.ToString()); }
        }
        [Command("bansync")]
        public static void CMD_banlistSync(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "ban")) return;
                Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "I start the synchronization process...", 4000);
                Ban.Sync();
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "The procedure is complete!", 3000);
                GameLog.Admin(player.Name, "bansync", "");
            }
            catch (Exception e) { _logger.WriteError("bansync: " + e.ToString()); }
        }
        [Command("zonecolor")]
        public static void CMD_setTerritoryColor(ExtPlayer player, int gangid)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "zonecolor")) return;

                if (player.GetData<int>("GANGPOINT") == -1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You must be in one of the regions for recordings", 3000);
                    return;
                }
                var terrid = player.GetData<int>("GANGPOINT");

                if (!Fractions.GangsCapture.gangPointsColor.ContainsKey(gangid))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Bangs with such identity not found ", 3000);
                    return;
                }

                GangsCapture.gangPoints[terrid].GangOwner = gangid;
                SafeTrigger.ClientEventForAll("setZoneColor", Fractions.GangsCapture.gangPoints[terrid].ID, Fractions.GangsCapture.gangPointsColor[gangid]);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Now the territory #{(int)terrid} has {Manager.getName(gangid)}", 3000);
                GameLog.Admin($"{player.Name}", $"setColour({terrid},{gangid})", $"");
                GangsCapture.SavingRegions();
            }
            catch (Exception e) { _logger.WriteError("CMD_SetColour: " + e.ToString()); }
        }
        [Command("clothes")]
        public static void CMD_SetClothesGo(ExtPlayer player, int id, int draw, int texture)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "clothes")) return;
                player.SetWhistlerClothes(id, draw, texture);
                if (id == 11) player.SetWhistlerClothes(3, OldCustomization.CorrectTorso[player.GetGender()].GetValueOrDefault(draw), 0);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SetClothesGo {id} {draw} {texture}: {e}");
            }
        }
        [Command("props")]
        public static void CMD_SetPropsGo(ExtPlayer player, int id, int draw, int texture)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "props")) return;
                if (draw > -1)
                    player.SetWhistlerProps(id, draw, texture);
                else
                    player.ClearAccessory(id);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SetPropsGo: {e}");
            }
        }

        // [Command("forbes")]
        // public async static void CMD_Forbes(ExtPlayer player, int page)
        // {
        //     try
        //     {
        //         if (!player.IsLogged()) return;
        //         if (!Group.CanUseAdminCommand(player, "forbes")) return;
        //         var result = await MySQL.QueryReadAsync("SELECT ch.`uuid`, `firstname`,`lastname`, ch.`money`, IFNULL(m.`Balance`, 0) as balance, ch.`money` + IFNULL(m.`Balance`, 0) as amountmoney FROM `characters` ch LEFT JOIN `efcore_bank_account` m ON m.ID = ch.banknew WHERE `deleted` = false ORDER BY amountmoney DESC");
        //         if (result != null)
        //         {
        //             NAPI.Task.Run(() =>
        //             {
        //                 for (int i = (page - 1) * 10; i < page * 10; i++)
        //                 {
        //                     if (i < result.Rows.Count)
        //                     {
        //                         var uuid = Convert.ToInt32(result.Rows[i]["uuid"]);
        //                         var name = $"{Convert.ToString(result.Rows[i]["firstname"])}_{Convert.ToString(result.Rows[i]["lastname"])}";
        //                         var money = Convert.ToInt32(result.Rows[i]["money"]);
        //                         var bank = Convert.ToInt32(result.Rows[i]["balance"]);
        //                         var amount = Convert.ToInt32(result.Rows[i]["amountmoney"]);
        //                         Chat.SendTo(player, "Топ 20 богатых людей штата".Translate(i + 1, name, uuid, money, bank, amount));
        //                     }
        //                 }
        //             });
        //         }
        //         GameLog.Admin(player.Name, "forbes", "");
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.WriteError("EXCEPTION AT \"CMD_Forbes\":" + e.ToString());
        //     }

        // }


        [Command("checkwanted")]
        public static void CMD_checkwanted(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "checkwanted")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such a ID was not found", 3000);
                    return;
                }
                var stars = (target.Character.WantedLVL == null) ? 0 : target.Character.WantedLVL.Level;
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"The amount is in the warehouse - {stars}", 3000);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SetPropsGo: {e}");
            }
        }
        [Command("fixcar")]
        public static void CMD_fixcar(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "fixcar")) return;
                if (!player.IsInVehicle) return;
                VehicleManager.RepairBody(player.Vehicle as ExtVehicle);
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_fixcar\":" + e.ToString());
            }
        }
        [Command("fixcarid")]
        public static void CMD_fixcarById(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "fixcar")) return;
                var vehicle = Trigger.GetVehicleById(id);
                if (vehicle == null)
                {
                    Notify.SendError(player, "No low transportation");
                    return;
                }
                VehicleManager.RepairBody(vehicle);
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_fixcar\":" + e.ToString());
            }
        }
        [Command("propertystats")]
        public static void CMD_showPlayerHouseStats(ExtPlayer admin, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(admin, "propertystats")) return;

                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }

                ExtPlayer player = Trigger.GetPlayerByUuid(id);
                if (!player.IsLogged())
                    return;

                var house = HouseManager.GetHouse(player, true);
                if (house == null)
                    Chat.SendTo(admin,"Player has no house");
                else
                    Chat.SendTo(admin,$"Player has {HouseManager.HouseTypeList[house.Type].Name} haus | ID {house.ID}");

                var vehicles = VehicleManager.getAllHolderVehicles(player.Character.UUID, OwnerType.Personal);
                if (vehicles.Count() > 0)
                {
                    Chat.SendTo(admin, $"Player vehicles:");
                    foreach (var veh in vehicles)
                        Chat.SendTo(admin, $"{VehicleManager.Vehicles[veh].Number} - {VehicleManager.Vehicles[veh].ModelName}");
                }


                var biz = player.GetBusiness();
                if (biz == null)
                    Chat.SendTo(admin,$"Player has no business");
                else
                    Chat.SendTo(admin,$"Player has {biz.TypeModel.TypeName} (ID {biz.ID})");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_housestats\":" + e.ToString());
            }
        }
        [Command("stats")]
        public static void CMD_showPlayerStats(ExtPlayer admin, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(admin, "stats")) return;

                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                ExtPlayer player = Trigger.GetPlayerByUuid(id);

                #region Make statistics
                var character = player.Character;
                string status =
                    (character.AdminLVL >= 1) ? "administrator":
                    player.Character.IsPrimeActive() ? 
                    $"Premium -Account ({player.Character.VipDate.ToString("dd.MM.yyyy")})" :
                    "Basic Account";
                long bank = character.BankModel?.Balance ?? 0;
                var lic = player.GetLicenses();
                if (lic == "") 
                    lic = "keiner";
                string work = (character.WorkID > 0 && (character.WorkID != Jobs.Technician.Work.WorkID && character.WorkID != Jobs.CarThief.Work.WorkID)) ? Jobs.WorkManager.JobStats[character.WorkID - 1] : "Безработный";
                string fraction = (character.FractionID > 0) ? Manager.getName(character.FractionID) : "";
                #endregion

                //Chat.SendTo(admin, "local_17".Translate(admin, character.LVL, character.EXP, 3 + character.LVL * 3));
                //Chat.SendTo(admin, "local_18".Translate(admin, status, character.Warns, character.CreateDate.ToString("dd.MM.yyyy")));
                //Chat.SendTo(admin, "local_19".Translate(admin, number));
                //Chat.SendTo(admin, "local_14".Translate(admin, lic));
                //Chat.SendTo(admin, "local_20".Translate(admin, character.UUID, character.Bank));
                //Chat.SendTo(admin, "local_16".Translate(admin, work, fraction, character.FractionLVL));

                admin.SendChatMessage("----------------------------");
                admin.SendChatMessage($"Statistics from{player.Name}:");

                admin.SendChatMessage($"Lvl: {character.LVL}, Exp: {character.EXP}/{character.LVL * 3 + 3}");
                var phoneNumber = character.PhoneTemporary.Phone?.SimCard?.Number.ToString() ?? "none";
                admin.SendChatMessage($"Phone number: {phoneNumber}");
                admin.SendChatMessage($"License: {lic}");
                admin.SendChatMessage($"fraction: {character.FractionID} | rang: {character.FractionLVL}");

                admin.SendChatMessage("----------------------------");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_showPlayerStats\":" + e.ToString());
            }
        }
        [Command("admins")]
        public static void CMD_AllAdmins(ExtPlayer client)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "admins")) return;

                Chat.SendTo(client, "Administrator online:");
                Main.ForEachAllPlayer((p) =>
                {
                    if (p.Character.AdminLVL < 1 || p.Character.AdminLVL >= 7 && client.Character.AdminLVL < p.Character.AdminLVL) return;
                    Chat.SendTo(client, $"Administrator: {p.Name}({p.Character.UUID}) / level: {p.Character.AdminLVL}");
                });
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_AllAdmins\":" + e.ToString());
            }
        }

      
        [Command("fixweaponsshops")]
        public static void CMD_fixweaponsshops(ExtPlayer client)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "fixweaponsshops")) return;

                foreach (var biz in BusinessManager.BizList.Values)
                {
                    if (biz.Type != 6) continue;
                    biz.Products = BusinessManager.fillProductList(6);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_fixweaponsshops\":\n" + e.ToString());
            }
        }
        [Command("giveproduct")]
        public static void CMD_setproductbyindex(ExtPlayer player, int id, int index, int product)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "giveproduct")) return;

                var biz = BusinessManager.BizList[id];
                biz.Products[index].Lefts = product;
                GameLog.Admin(player.Name, $"giveproduct({id},{index},{product})", "") ;
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_setproductbyindex\":\n" + e.ToString());
            }
        }
        [Command("removeproducts")]
        public static void CMD_deleteproducts(ExtPlayer client, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "removeproducts")) return;

                var biz = BusinessManager.BizList[id];
                foreach (var p in biz.Products)
                    p.Lefts = 0;
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_setproductbyindex\":\n" + e.ToString());
            }
        }
        [Command("changebizprice")]
        public static void CMD_changeBusinessPrice(ExtPlayer player, int newPrice)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "changebizprice")) return;
                if (player.GetData<int>("BIZ_ID") == -1)
                {
                    Chat.SendTo(player, "You must be at business ");
                    return;
                }
                Business biz = BusinessManager.BizList[player.GetData<int>("BIZ_ID")];
                biz.SellPrice = newPrice;
                GameLog.Admin(player.Name, $"changebizprice({biz.ID},{newPrice})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_changeBusinessPrice: {e}");
            }
        }
        
        [Command("tpc")]
        public static void CMD_tpCoord(ExtPlayer player, double x, double y, double z)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tpc")) return;
                var pos = new Vector3(x, y, z);               
                player.ChangePosition(pos);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_tpCoord: {e}");
            }
        }

        [Command("btp")]
        public static void CMD_BizTP(ExtPlayer player, int bizId)
        {
            if (!Group.CanUseAdminCommand(player, "tpc")) return;
            if (!BusinessManager.BizList.ContainsKey(bizId))
            {
                Notify.SendError(player, "Unfortunately, such an ID business was not found in the system.");
                return;
            }

            Business business = BusinessManager.BizList[bizId];
            if (business == null) return;
            if (bizId != business.ID)
            {
                Notify.SendError(player, "Something went wrong, try again.");
                return;
            }
            Vector3 bPosition = new Vector3(business.EnterPoint.X, business.EnterPoint.Y, business.EnterPoint.Z);
            player.ChangePosition(bPosition);
            Notify.SendSuccess(player, $"You have successfully teleported to business {business.ID}.");
        }

        [Command("htp")]
        public static void CMD_HouseTP(ExtPlayer player, int houseId)
        {
            if (!Group.CanUseAdminCommand(player, "tpc")) return;

            House house = HouseManager.Houses.FirstOrDefault(x => x.ID == houseId);
            if (house == null)
            {
                Notify.SendError(player, "Unfortunately, such ID at home was not found in the system.");
                return;
            }
            if (houseId != house.ID)
            {
                Notify.SendError(player, "Something went wrong, try again.");
                return;
            }
            Vector3 bPosition = new Vector3(house.Position.X, house.Position.Y, house.Position.Z);
            player.ChangePosition(bPosition);
            Notify.SendSuccess(player, $"You have successfully teleported to the house {house.ID}.");
        }

        [Command("inv")]
        public static void CMD_ToogleInvisible(ExtPlayer player, int state)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "inv")) return;

                Admin.SetInvisible(player, state > 0);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_tpCoord: {e}");
            }
        }

        [Command("removefrac")]
        public static void CMD_delFrac(ExtPlayer player, int id)
        {
            try
            {

                if (!Group.CanUseAdminCommand(player, "removefrac")) return;
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such a ID was not found", 3000);
                    return;
                }
                Admin.DelFrac(player, Trigger.GetPlayerByUuid(id), false);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_delFrac: {e}");
            }
        }

        [Command("tpcar")]
        public static void CMD_tpcar(ExtPlayer player, int carID)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tpcar")) return;
                var vehicles = NAPI.Pools.GetAllVehicles();
                var vehicle = vehicles.Where(a => a.Value == carID).FirstOrDefault();
                if (vehicle == null) {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Incorrect ID of the car!", 3000);
                    return;
                };
                vehicle.Position = player.Position + new Vector3(1.0, 1.0, 0);
                vehicle.Rotation = player.Rotation;
                vehicle.Dimension = player.Dimension;
            }
            catch(Exception e)
            {
                _logger.WriteError($"CMD_tpcar: {e}");
            }
        }

        [Command("sendcreator")]
        public static void CMD_SendToCreator(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "sendcreator")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (player.Character.AdminLVL < target.Character.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player has higher Admin level than you".Translate(), 3000);
                    return;
                }
                player.Character.ExteriorPos = player.Position;
                CustomizationService.SendToCreator(target, -1);
                GameLog.Admin(player.Name, $"sendCreator", target.Name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SendToCreator: {e}");
            }
        }

        [Command("fuel")]
        public static void CMD_setVehiclePetrol(ExtPlayer player, int fuel)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "fuel")) return;
                if (!player.IsInVehicle) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                VehicleStreaming.SetVehicleFuel(playerVehicle, fuel);
                GameLog.Admin($"{player.Name}", $"fuel({playerVehicle.Data.ID},{fuel})", $"");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_tpCoord: {e}");
            }
        }
        [Command("changename")]
        public static void CMD_changeName(ExtPlayer client, string current, string newName)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "changename")) return;

                var result = Character.Character.ChangeName(current, newName);

                switch (result)
                {
                    case ChangeNameResult.Success:
                        Notify.Send(client, NotifyType.Alert, NotifyPosition.BottomCenter, "The name is successfully changed", 3000);
                        break;
                    case ChangeNameResult.BadCurrentName:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "A player with that name was not found", 3000);
                        break;
                    case ChangeNameResult.IncorrectNewName:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Incorrect name ", 3000);
                        break;
                    case ChangeNameResult.NewNameIsExist:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "This name already exists", 3000);
                        break;
                }
                GameLog.Admin(client.Name, $"changeName({newName})", current);

            }catch (Exception e)
            {
                _logger.WriteError($"changename: {e}");
            }
        }
        [Command("changenameid")]
        public static void CMD_changeName(ExtPlayer client, int id, string newName)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "changenameid")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such ID is not online ", 3000);
                    return;
                }
                string current = target.Character.FullName;
                var result = Character.Character.ChangeName(current, newName);
                switch (result)
                {
                    case ChangeNameResult.Success:
                        Notify.Send(client, NotifyType.Alert, NotifyPosition.BottomCenter, "The name is successfully changed", 3000);
                        break;
                    case ChangeNameResult.BadCurrentName:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "A player with that name was not found ", 3000);
                        break;
                    case ChangeNameResult.IncorrectNewName:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Incorrect name", 3000);
                        break;
                    case ChangeNameResult.NewNameIsExist:
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "This name already exists", 3000);
                        break;
                }

                GameLog.Admin(client.Name, $"changeName({newName})", current);

            }catch (Exception e)
            {
                _logger.WriteError($"changename: {e}");
            }
        }
        [Command("startarmwar")]
        public static void CMD_startMatWars(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "startarmwar")) return;
                if (Fractions.MatsWar.isWar)
                {
                    Chat.SendTo(player,"The war for the materials began");
                    return;
                }
                Fractions.MatsWar.startMatWarTimer();
                Chat.SendTo(player,"The war for the materials began");
                GameLog.Admin(player.Name, $"startarmwar", "");
            }
            catch (Exception e) { _logger.WriteError("startmatwars: " + e.ToString()); }
        }
        [Command("setexp")]
        public static void CMD_giveExp(ExtPlayer player, int id, int exp)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setexp")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                target.Character.EXP += exp;
                while (target.Character.EXP >= 3 + target.Character.LVL * 3)
                {
                    target.Character.EXP = target.Character.EXP - (3 + target.Character.LVL * 3);
                    target.Character.LVL += 1;
                    if(target.Character.LVL == 1) 
                    {
                        NAPI.Task.Run(() => {SafeTrigger.ClientEvent(target, "disabledmg", false); }, 5000);
                    }
                    if (target.Character.LVL == 5 && !target.Account.PromoReceived && !string.IsNullOrEmpty(target.Account.PromoUsed)) PromoCodesService.GiveReward(target);
                }
                SafeTrigger.SetSharedData(target, "C_LVL", target.Character.LVL);
                MainMenu.SendStats(target);
                player.SendExpUpdate();
                GameLog.Admin(player.Name, $"giveExp({exp})", target.Name);
            }
            catch (Exception e) { _logger.WriteError("setexp" + e.ToString()); }
        }
        [Command("sethtypeprice")]
        public static void CMD_replaceHousePrices(ExtPlayer player, int type, int newPrice)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "sethtypeprice")) return;
                if (!Enum.IsDefined(typeof(HouseTypes), type))
                    return;
                HouseTypes houseTypes = (HouseTypes)type;
                foreach (var h in HouseManager.Houses.Where(item => item.Type == houseTypes && item.OwnerType != OwnerType.Family))
                {
                    h.SetPrice(newPrice);
                }
                GameLog.Admin(player.Name, $"sethtypeprice({type},{newPrice})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_replaceHousePrices: {e}");
            }
        }
        [Command("sethouseinter")]
        public static void CMD_ChangeHouseType(ExtPlayer player, int houseId, int type)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "sethouseinter")) return;
                if (!Enum.IsDefined(typeof(HouseTypes), type))
                    return;
                HouseTypes houseTypes = (HouseTypes)type;
                var house = HouseManager.GetHouseById(houseId);
                house.SetType(houseTypes);
                GameLog.Admin(player.Name, $"sethouseinter({houseId},{type})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ChangeHouseType: {e}");
            }
        }
        [Command("takeoffhouse")]
        public static void CMD_deleteHouseOwner(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "takeoffhouse")) return;
                if (!player.HasData("HOUSEID"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You must be near the house", 3000);
                    return;
                }

                Houses.House house = Houses.HouseManager.Houses.FirstOrDefault(h => h.ID == player.GetData<int>("HOUSEID"));
                if (house == null) return;

                var owner = house.GetHouseOwnerName();
                house.SetOwner(-1, 0);
                GameLog.Admin($"{player.Name}", $"delHouseOwner({house.ID})", owner);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_deleteHouseOwner: {e}");
            }
        }

        [Command("boost")]
        public static void CMD_SetTurboTorque(ExtPlayer player, float power, float torque)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "boost")) return;
                if (!player.IsInVehicle || player.Vehicle == null) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                VehicleCustomization.SetPowerTorque(playerVehicle, power, torque);
                GameLog.Admin(player.Name, $"boost({playerVehicle.Data.ID},{power},{torque})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError("Error at \"STT\":" + e.ToString());
            }
        }

        /// <summary>
        /// Without save to databese
        /// </summary>
        [Command("sttlite")]
        public static void CMD_SetTurboTorqueLite(ExtPlayer player, float power, float torque)
        {
            try
            {
                if (player.Character.AdminLVL < 3) return;
                if (!player.IsInVehicle) return;
                SafeTrigger.ClientEvent(player, "svem", power, torque);
            }
            catch (Exception e)
            {
                _logger.WriteError("Error at \"STTLITE\":" + e.ToString());
            }
        }

        [Command("rtm")]
        public static void CMD_SetVehicleMod(ExtPlayer player, int type, int index)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "rtm")) return;
                if (type == 54 && !Group.CanUseAdminCommand(player, "perl")) return;
                if (!player.IsInVehicle) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                if (Enum.IsDefined(typeof(ModTypes), type))
                {
                    VehicleCustomization.SetMod(playerVehicle, (ModTypes)type, index);
                }
                else
                    player.Vehicle.SetMod(type, index);
                GameLog.Admin(player.Name, $"rtm({playerVehicle.Data.ID},{type},{index})", "");

            }
            catch (Exception e)
            {
                _logger.WriteError("Error at \"SVM\":" + e.ToString());
            }
        }

        [Command("svn")]
        public static void CMD_SetVehicleNeon(ExtPlayer player, int r, int g, int b)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "svn")) return;
                if (!player.IsInVehicle) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                VehicleCustomization.SetNeon(playerVehicle, new List<Color>() { new Color(r, g, b) });
                GameLog.Admin(player.Name, $"svn({playerVehicle.Data.ID})", "");

            }
            catch (Exception e)
            {
                _logger.WriteError("Error at \"SVM\":" + e.ToString());
            }
        }

        [Command("addneon")]
        public static void CMD_AddVehicleNeon(ExtPlayer player, int r, int g, int b)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "addneon")) return;
                if (!player.IsInVehicle) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                VehicleCustomization.AddNeon(playerVehicle,  new Color(r, g, b));
                GameLog.Admin(player.Name, $"addneon({playerVehicle.Data.ID})", "");

            }
            catch (Exception e)
            {
                _logger.WriteError("Error at \"SVM\":" + e.ToString());
            }
        }

        [Command("svh")]
        public static void CMD_SetVehicleHealth(ExtPlayer player, int health = 1000)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "svh")) return;
                if (!player.IsInVehicle) return;
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                vehicle.Repair();
                vehicle.Health = health;
                VehicleManager.RepairVehicle(vehicle);
                GameLog.Admin(player.Name, $"svh({vehicle.Data.ID})", "");

            } catch (Exception e)
            {
                _logger.WriteError("Error at \"SVH\":" + e.ToString());
            }            
        }


        [Command("removecars")]
        public static void CMD_deleteAdminCars(ExtPlayer player)
        {
            try
            {
                NAPI.Task.Run(() =>
                {
                    if (!Group.CanUseAdminCommand(player, "removecars")) return;
                    foreach (var veh in Trigger.GetAllVehicles())
                    {
                        if ((veh.Data.OwnerType == OwnerType.Temporary) && (veh.Data as TemporaryVehicle).Access == VehicleAccess.Admin && veh.HasData("ACCESSADMINBY"))
                            veh.CustomDelete();
                    }
                    GameLog.Admin($"{player.Name}", $"removecars", $"");
                });
            }
            catch (Exception e) { _logger.WriteError("delacars: " + e.ToString()); }
        }
        [Command("removecar")]
        public static void CMD_deleteThisAdminCar(ExtPlayer client)
        {
            try
            {
                if (!Group.CanUseAdminCommand(client, "removecar")) return;
                if (!client.IsInVehicle) return;
                ExtVehicle veh = client.Vehicle as ExtVehicle;
                if ((veh.Data.OwnerType == OwnerType.Temporary) && (veh.Data as TemporaryVehicle).Access == VehicleAccess.Admin)
                    veh.CustomDelete();
                GameLog.Admin($"{client.Name}", $"removecar", $"");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_deleteThisAdminCar: {e}");
            }
        }
        [Command("delmycars", "dmcs")]
        public static void CMD_delMyCars(ExtPlayer client)
        {
            try
            {
                NAPI.Task.Run(() =>
                {
                    if (!Group.CanUseAdminCommand(client, "vehc")) return;
                    foreach (var v in NAPI.Pools.GetAllVehicles())
                    {
                        if (v.GetData<string>("ACCESSADMINBY") == client.Name)
                            v.CustomDelete();
                    }
                    GameLog.Admin($"{client.Name}", $"delmycars", $"");
                });
            }
            catch (Exception e) { _logger.WriteError("delacars: " + e.ToString()); }
        }
        [Command("spawnallcar")]
        public static void CMD_allSpawnCar(ExtPlayer player)
        {
            try
            {
                Admin.respawnAllCars(player);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_allSpawnCar: {e}");
            }
        }
        [Command("scoord")]
        public static void CMD_saveCoord(ExtPlayer player, string name)
        {
            try
            {
                Admin.saveCoords(player, name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_allSpawnCar: {e}");
            }
        }
        [Command("carcoord")]
        public static void CMD_SaveCarCoords(ExtPlayer player, string name)
        {
            try
            {
                Admin.SaveCarCoords(player, name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_allSpawnCar: {e}");
            }
        }
        [Command("loadinteriors")]
        public static void CMD_loadinteriors(ExtPlayer player, int id)
        {
            if (!Group.CanUseAdminCommand(player, "loadinteriors")) return;
            SafeTrigger.ClientEvent(player,"garage:loadInteriors", player.Position, id);
        }

        [Command("stopserver")]
        public static void CMD_stopServer(ExtPlayer player, string text = null)
        {
            if (!Group.CanUseAdminCommand(player, "stop")) return;
            Admin.ServerRestart(player.Name, text);
        }

        [Command("payday")]
        public static void payDay(ExtPlayer player, string text = null)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "payday")) return;
                GameLog.Admin(player.Name, $"payDay", "");
                Main.payDayTrigger(false);
            }
            catch (Exception e)
            {
                _logger.WriteError($"payDay: {e}");
            }
        }

        [Command("giveleader")]
        public static void CMD_setLeader(ExtPlayer player, int id, int fracid)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.setFracLeader(player, Trigger.GetPlayerByUuid(id), fracid);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("sp")]
        public static void CMD_spectateMode(ExtPlayer player, int id)
        {
            if (!Group.CanUseAdminCommand(player, "sp")) return;

            try
            {
                ExtPlayer target = Trigger.GetPlayerByUuid(id);

                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        $"A player with such an ID was not found", 3000);
                    return;
                }

                // Prevent spectating higher-ranked admins
                if (target.Character.AdminLVL >= 7 && player.Character.AdminLVL < target.Character.AdminLVL) return;

                AdminSP.Spectate(player, target);

                // Notify the admin who started spectating
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter,
                    $"You have started spectating player {target.Name}.", 3000);

                // Notify all admins about the spectating action
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has started spectating player {target.Name}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_spectateMode\":\n" + e.ToString());
            }
        }

        [Command("usp")]
        public static void CMD_unspectateMode(ExtPlayer player)
        {
            if (!Group.CanUseAdminCommand(player, "sp")) return;
            try
            {
                AdminSP.UnSpectate(player);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("metp")]
        public static void CMD_teleportToMe(ExtPlayer player, int id)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                // Teleport the target player to the admin
                Admin.teleportTargetToPlayer(player, target, false);

                // Notify the targeted player
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter,
                    $"You have been teleported to admin {player.Name}.", 3000);

                // Notify the admin
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have teleported player {target.Name} to yourself.", 3000);

                // Notify all admins
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has teleported player {target.Name} to themselves.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_metp\":\n" + e.ToString());
            }
        }


        [Command("gethere")]
        public static void CMD_teleportVehToMe(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.teleportTargetToPlayer(player, Trigger.GetPlayerByUuid(id), true);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("kill")]
        public static void CMD_kill(ExtPlayer player, int id)
        {
            try
            {
                var targetPlayer = Trigger.GetPlayerByUuid(id);

                if (targetPlayer == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                // Kill the target player
                Admin.killTarget(player, targetPlayer);

                // Notify the target player
                Notify.Send(targetPlayer, NotifyType.Warning, NotifyPosition.BottomCenter,
                    $"You have been killed by admin {player.Name}.", 3000);

                // Notify all admins
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has killed player {targetPlayer.Name}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_kill\":\n" + e.ToString());
            }
        }

        [Command("hp")]
        public static void CMD_adminHeal(ExtPlayer player, int id, int hp)
        {
            try
            {
                // Check if the player with the given ID exists
                var targetPlayer = Trigger.GetPlayerByUuid(id);
                if (targetPlayer == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found.", 3000);
                    return;
                }

                // Save the original health before modification
                float originalHealth = targetPlayer.Health;

                // Heal the target player
                Admin.healTarget(player, targetPlayer, hp);

                // Notify the admin who used the command
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have healed {targetPlayer.Name} by {hp} points.", 3000);

                // Send log to all admins (regardless of admin level)
                Chat.SendToAdmins(1, $"Admin {player.Name} (Level: {player.Character.AdminLVL}) has increased {targetPlayer.Name}'s health by {hp} points. Previous health: {originalHealth}, New health: {targetPlayer.Health}.");

                // Optionally log to an admin log file or database
                GameLog.Admin($"{player.Name}", $"hp({targetPlayer.Name}, {hp})", $"Health changed from {originalHealth} to {targetPlayer.Health}");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_adminHeal\":\n" + e.ToString());
            }
        }
        [Command("hpall")]
        public static void CMD_adminHealAll(ExtPlayer player, float radius, int hp)
        {
            try
            {
                // Validate radius
                if (radius <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid radius. Please provide a valid radius.", 3000);
                    return;
                }

                // Validate health points
                if (hp <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid health points. Please provide a positive number.", 3000);
                    return;
                }

                // Get all players within the specified radius
                Vector3 playerPosition = player.Position;
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter,
                        "No players found within the specified radius.", 3000);
                    return;
                }

                // Heal all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        float originalHealth = extPlayer.Health;

                        // Heal the target player
                        Admin.healTarget(player, extPlayer, hp);

                        // Notify the healed player
                        Notify.Send(extPlayer, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"You have been healed by an admin. Health increased by {hp} points.", 3000);

                        // Optionally log individual healing actions
                        GameLog.Admin($"{player.Name}", $"hp({extPlayer.Name}, {hp})", $"Health changed from {originalHealth} to {extPlayer.Health}");
                    }
                }

                // Notify the admin about the success
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have healed all players within a {radius}m radius by {hp} points.", 3000);

                // Log the action for all admins
                Chat.SendToAdmins(1,
                    $"~r~[Admin] {player.Name} has healed all players within a {radius}m radius by {hp} points.");
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"CMD_adminHealAll\":\n{e}");
            }
        }


        [Command("frz")]
        public static void CMD_adminFreeze(ExtPlayer player, int id)
        {
            try
            {
                ExtPlayer target = Trigger.GetPlayerByUuid(id);

                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        $"EIn players with such an ID, it was not found", 3000);
                    return;
                }

                Admin.freezeTarget(player, target);

                // Notify the admin who froze the player
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter,
                    $"You have frozen player {target.Name}.", 3000);

                // Notify the frozen player
                Notify.Send(target, NotifyType.Warning, NotifyPosition.BottomCenter,
                    $"You have been frozen by Admin {player.Name}.", 3000);

                // Notify all admins about the freezing action
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has frozen player {target.Name}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_adminFreeze\":\n" + e.ToString());
            }
        }
        [Command("frzall")]
        public static void CMD_adminFreezeAll(ExtPlayer player, float radius)
        {
            try
            {
                if (radius <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid radius. Please provide a positive value.", 3000);
                    return;
                }

                Vector3 playerPosition = player.Position;
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter,
                        "No players found within the specified radius.", 3000);
                    return;
                }

                // Freeze all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        // Freeze the target player
                        Admin.freezeTarget(player, extPlayer);

                        // Notify the frozen player
                        Notify.Send(extPlayer, NotifyType.Warning, NotifyPosition.BottomCenter,
                            $"You have been frozen by Admin {player.Name}.", 5000);
                    }
                }

                // Notify the admin that the freeze process is complete
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"All players within a {radius}m radius have been frozen.", 3000);

                // Notify all admins about the action
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has frozen all players within a {radius}m radius.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_adminFreezeAll\":\n" + e.ToString());
            }
        }

        [Command("unfrz")]
        public static void CMD_adminUnFreeze(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.unFreezeTarget(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("unfrzall")]
        public static void CMD_adminUnFreezeAll(ExtPlayer player, float radius)
        {
            try
            {
                if (radius <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid radius. Please provide a positive value.", 3000);
                    return;
                }

                Vector3 playerPosition = player.Position;
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter,
                        "No players found within the specified radius.", 3000);
                    return;
                }

                // Unfreeze all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        // Unfreeze the target player
                        Admin.unFreezeTarget(player, extPlayer);

                        // Notify the unfrozen player
                        Notify.Send(extPlayer, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"You have been unfrozen by Admin {player.Name}.", 5000);
                    }
                }

                // Notify the admin that the unfreeze process is complete
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"All players within a {radius}m radius have been unfrozen.", 3000);

                // Notify all admins about the action
                Chat.SendToAdmins(1, $"~g~[Admin] {player.Name} has unfrozen all players within a {radius}m radius.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_adminUnFreezeAll\":\n" + e.ToString());
            }
        }

        [Command("makeadmin")]
        public static void CMD_setAdmin(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                Admin.setPlayerAdminGroup(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("takeadmin")]
        public static void CMD_delAdmin(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.delPlayerAdminGroup(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("makemedia")]
        public static void CMD_setMedia(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "makemedia")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                target.Character.Media = 1;
                target.SetSharedData("IS_MEDIA", target.Character.Media > 0);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave media status to the player{target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You were given to media status admin {player.Name}", 3000);
                GameLog.Admin(player.Name, $"makemedia", target.Name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("takemedia")]
        public static void CMD_delMedia(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "takemedia")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such a ID was not found", 3000);
                    return;
                }
                target.Character.Media = 0;
                target.SetSharedData("IS_MEDIA", target.Character.Media > 0);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You took the media status of the player {target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You have taken the media status admin{player.Name}", 3000);
                GameLog.Admin(player.Name, $"takemedia", target.Name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("makemediahelper")]
        public static void CMD_setMediaHelper(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "makemediahelper")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                target.Character.MediaHelper = 1;
                target.SetSharedData("IS_MEDIAHELPER", target.Character.MediaHelper > 0);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave the status 'media assistant' to the player {target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You were given the status of 'media assistant' admin {player.Name}", 3000);
                GameLog.Admin(player.Name, $"makemediahelper", target.Name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("takemediahelper")]
        public static void CMD_delMediaHelper(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "takemediahelper")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Ein Spieler mit einer solchen ID wurde nicht gefunden", 3000);
                    return;
                }
                target.Character.MediaHelper = 0;
                target.SetSharedData("IS_MEDIAHELPER", target.Character.MediaHelper > 0);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You took the status 'media assistant' player{target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You have taken the status of 'media assistant' admin {player.Name}", 3000);
                GameLog.Admin(player.Name, $"takemediahelper", target.Name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("setplayericon")]
        public static void CMD_SetPlayerIcon(ExtPlayer player, int id, string dictionary = "none", string name = "none", int color = -1)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setplayericon")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (!target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                if (dictionary != "none")
                    target.Character.IconOverHead = new PlayerIconOverHead(dictionary, name, color);
                else
                    target.Character.IconOverHead = new PlayerIconOverHead();
                target.Character.IconOverHead?.UpdateSharedData(target);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You installed a new icon for{target.Name}", 3000);
                GameLog.Admin(player.Name, $"setplayericon", target.Name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("changeadminrank")]
        public static void CMD_setAdminRank(ExtPlayer player, int id, int rank)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                Admin.setPlayerAdminRank(player, Trigger.GetPlayerByUuid(id), rank);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("givegun")]
        public static void CMD_adminGuns(ExtPlayer player, int id, string wname)
        {
            try
            {
                // Validate player ID
                var targetPlayer = Trigger.GetPlayerByUuid(id);
                if (targetPlayer == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found.", 3000);
                    return;
                }

                // Normalize weapon name to support multi-word names (e.g., "WEAPON_ASSAULTRIFLE" -> "ASSAULT RIFLE")
                wname = wname.Replace("_", " ").ToUpper();

                // Check if the weapon is supported in the server
                WeaponHash weaponHash = (WeaponHash)NAPI.Util.GetHashKey(wname);
                if (weaponHash == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid weapon name.", 3000);
                    return;
                }

                // Give the weapon to the target player
                targetPlayer.GiveWeapon(weaponHash, 9999); // Give 9999 ammo by default (adjust if needed)

                // Notify the admin who used the command
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have given {wname} to {targetPlayer.Name}.", 3000);

                // Send log to all admins (regardless of admin level)
                Chat.SendToAdmins(1, $"Admin {player.Name} (Level: {player.Character.AdminLVL}) has given {wname} to {targetPlayer.Name}.");

                // Log the action
                GameLog.Admin($"{player.Name}", $"givegun({targetPlayer.Name}, {wname})", $"");

            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_adminGuns\":\n" + e.ToString());
            }
        }

        [Command("takeguns")]
        public static void CMD_takeguns(ExtPlayer player, float radius)
        {
            try
            {
                // Validate the radius
                if (radius <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid radius. Please provide a valid value.", 3000);
                    return;
                }

                // Get the player's position
                Vector3 playerPosition = player.Position;

                // Find all players within the radius
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                // Check if any players are found
                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "No players found within the specified radius.", 3000);
                    return;
                }

                // Remove all weapons from players within the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        // Remove weapons from the player
                        NAPI.Player.RemoveAllPlayerWeapons(extPlayer);

                        // Notify the target player
                        Notify.Send(extPlayer, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"All your weapons have been removed by Admin {player.Name}.", 3000);
                    }
                }

                // Notify the admin about the successful action
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have removed all weapons from players within a {radius}m radius.", 5000);

                // Send a log to all admins
                Chat.SendToAdmins(1,$"~r~[Admin] {player.Name} (Level: {player.Character.AdminLVL}) removed all weapons from players within a {radius}m radius.");
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"CMD_takeguns\":\n{e}");
            }
        }


        [Command("giveguncomponents")]
        public static void CMD_AdminGunsWithComponents(ExtPlayer player, int id, string wname, int muzzle, int flash, int clip, int scope, int grip, int skin)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.giveTargetGunWithComponents(player, Trigger.GetPlayerByUuid(id), wname, muzzle, flash, clip, scope, grip, skin);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("giveganja")]
        public static void GiveGanja(ExtPlayer player, int id, int count)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "giveganja")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                var item = ItemsFabric.CreateNarcotic(ItemNames.Marijuana, count, false);
                if (item == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The issuance of clothing items is prohibited ", 3000);
                    return;
                }
                if (!target.GetInventory().AddItem(item))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player does not have enough space in the inventory", 3000);
                    return;
                }
            }
            catch (Exception e) { Console.WriteLine($"GiveGanja\n{e}"); }
        }

        [Command("givegunc")]
        public static void CMD_adminGunsCommon(ExtPlayer player, int id, string wname)
        {
            try
            {
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.giveTargetGun(player, target, wname, false);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("giveclothes")]
        public static void CMD_adminClothesPromo(ExtPlayer player, int id, int type, int drawable, int texture)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.giveTargetClothes(player, Trigger.GetPlayerByUuid(id), type, drawable, texture, true);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("giveclothesc")]
        public static void CMD_adminClothes(ExtPlayer player, int id, int type, int drawable, int texture)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.giveTargetClothes(player, Trigger.GetPlayerByUuid(id), type, drawable, texture, false);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("setskin")]
        public static void CMD_adminSetSkin(ExtPlayer player, int id, string pedModel)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.giveTargetSkin(player, Trigger.GetPlayerByUuid(id), pedModel);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("givemoney")]
        public static void CMD_adminGiveMoney(ExtPlayer player, int id, int money)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "givemoney")) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                if (Admin.GiveMoney(player, target.Character, money))
                {
                    GameLog.Admin(player.Name, $"giveMoney({money})", target.Name);
                    Notify.SendSuccess(player, $"Money{money} Successfully issued");
                }
                else
                    Notify.SendError(player, "The player doesn't have enough money");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("givebank")]
        public static void CMD_adminGiveBank(ExtPlayer player, long bankAccount, int money)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "givemoney")) return;
                var target = BankManager.GetAccountByNumber(bankAccount);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                if (Admin.GiveMoney(player, target, money))
                {
                    GameLog.Admin(player.Name, $"giveBank({money})", $"{bankAccount}");
                    Notify.SendSuccess(player, $"Money in bank {money} Successfully issued");
                }
                else
                    Notify.SendError(player, "The player doesn't have enough money ");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("removeleader")]
        public static void CMD_delleader(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "removeleader")) return;
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                Admin.DelFrac(player, Trigger.GetPlayerByUuid(id), true);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("removejob")]
        public static void CMD_deljob(ExtPlayer player, int id)
        {
            if (!Group.CanUseAdminCommand(player, "removejob")) return;
            if (Trigger.GetPlayerByUuid(id) == null)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                return;
            }
            Admin.DelJob(player, Trigger.GetPlayerByUuid(id));
        }

        [Command("vehc")]
        public static void CMD_createVehicleCustom(ExtPlayer player, string name, int r, int g, int b)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "vehc")) return;
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(name);
                if (vh == 0) throw null;
                var vehicle = VehicleManager.CreateTemporaryVehicle(vh, player.Position, player.Rotation, "Admin", VehicleAccess.Admin);
                vehicle.Dimension = player.Dimension;
                player.SetIntoVehicle(vehicle, VehicleConstants.DriverSeat);
                VehicleCustomization.SetColor(vehicle, new Color(r, g, b), 1, true);
                VehicleCustomization.SetColor(vehicle, new Color(r, g, b), 1, false);
                SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
                VehicleStreaming.SetEngineState(vehicle, true);
                GameLog.Admin($"{player.Name}", $"vehCreate({name})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD_vehc\":\n" + e.ToString()); }
        }

        [Command("veh")]
        public static void CMD_createVehicle(ExtPlayer player, string name, int a, int b)
        {
            try
            {
                // Check if the player has permission to use the admin command
                if (!Group.CanUseAdminCommand(player, "vehc")) return;

                // Convert the vehicle name to a hash
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(name);
                if (vh == 0)
                {
                    Chat.SendTo(player, "Invalid vehicle name.");
                    return;
                }

                // Create the temporary vehicle at the player's position
                var vehicle = VehicleManager.CreateTemporaryVehicle(vh, player.Position, player.Rotation, "Admin", VehicleAccess.Admin);
                vehicle.Dimension = player.Dimension;
                vehicle.PrimaryColor = a;
                vehicle.SecondaryColor = b;
                SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
                VehicleStreaming.SetEngineState(vehicle, true);

                // Retrieve admin level from the player's Character
                int adminLevel = player.Character.AdminLVL;

                // Notify the admin that the vehicle was created
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"Vehicle '{name}' has been spawned.", 3000);

                // Notify all admins about the vehicle spawn
                Chat.SendToAdmins(1, $"Admin {player.Name} (Level: {adminLevel}) has spawned vehicle '{name}'.");

                // Log the vehicle creation event
                GameLog.Admin($"{player.Name}", $"vehCreate({name})", $"");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_veh\":\n" + e.ToString());
            }
        }


        [Command("vehs")]
        public static void CMD_createVehicleCount(ExtPlayer player, string name, int a, int b, int count)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "vehs")) return;
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(name);
                if (vh == 0)
                {
                    Chat.SendTo(player,"created vehicle");
                    return;
                }
                for (int i=0; i<count; i++)
                {
                    var vehicle = VehicleManager.CreateTemporaryVehicle(vh, player.Position + new Vector3(i, 0, 0), player.Rotation, $"Admin{i}", VehicleAccess.Admin);
                    vehicle.Dimension = player.Dimension;
                    vehicle.PrimaryColor = a;
                    vehicle.SecondaryColor = b;
                    SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
                    VehicleStreaming.SetEngineState(vehicle, true);
                }
                GameLog.Admin($"{player.Name}", $"vehsCreates({name})({count})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD_veh\":\n" + e.ToString()); }
        }
        [Command("vehstest")]
        public static void CMD_createVehicleCountss(ExtPlayer player, string name, int count)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "vehstest")) return;
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(name);
                if (vh == 0)
                {
                    Chat.SendTo(player, "created vehicle");
                    return;
                }
                List<ExtVehicle> vehicles = new List<ExtVehicle>();
                for (int i = 0; i < count; i++)
                {
                    var vehicle = VehicleManager.CreateTemporaryVehicle(vh, player.Position + new Vector3(i, 0, 0), player.Rotation, $"Admin{i}", VehicleAccess.Admin);
                    SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
                    vehicle.Dimension = player.Dimension;
                    vehicles.Add(vehicle);
                }
                foreach (var veh in vehicles)
                {
                    veh.CustomDelete();
                }
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD_veh\":\n" + e.ToString()); }
        }
        // [Command("vehtest")]
        // public static void CMD_createVehicleCountssssdaasfas(ExtPlayer player, string name)
        // {
        //     try
        //     {
        //         if (!Group.CanUseAdminCommand(player, "vehtest")) return;
        //         VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(name);
        //         if (vh == 0)
        //         {
        //             Chat.SendTo(player, "created vehicle");
        //             return;
        //         }
        //         var vehicle = VehicleManager.CreateTemporaryVehicle(vh, player.Position, player.Rotation, $"name", VehicleAccess.Admin);
        //         vehicle.Dimension = player.Dimension;
        //         vehicle.CustomDelete();
        //         vehicle = VehicleManager.CreateTemporaryVehicle(NAPI.Util.GetHashKey("go812"), player.Position, player.Rotation, $"go812", VehicleAccess.Admin);
        //         SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
        //     }
        //     catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD_veh\":\n" + e.ToString()); }
        // }

        [Command("dirt")]
        public static void CMD_setdirt(ExtPlayer player, float dirt)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                if (!player.IsInVehicle) return;

                ExtVehicle playerVehicle = player.Vehicle as ExtVehicle;
                VehicleStreaming.SetVehicleDirt(playerVehicle, dirt);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("godspeed")]
        public static void CMD_godspeedon(ExtPlayer player, int speed = 200, int step = 10)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "godspeed")) return;
                SafeTrigger.ClientEvent(player, "godspeedon", speed, step);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("newjobcar")]
        public static void newjobveh(ExtPlayer player, int typejob, string model, string number, int c1, int c2)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "newjobcar")) return;
                if (!Enum.IsDefined(typeof(WorkType), typejob))
                {
                    Chat.SendTo(player, "Choose one type of work from: Taxi - 3, Bus - 4, DockLoader = 7");
                    return;
                }
                number = number.ToUpper();
                if (VehicleManager.Vehicles.FirstOrDefault(item => item.Value.Number == number).Value != null)
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Bad number", 5000);
                    return;
                }
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(model);
                if (vh == 0) return;
                var jobVeh = new JobVehicle((WorkType)typejob, number, model, player.Position, player.Rotation, c1, c2);
                jobVeh.Spawn();

                Chat.SendTo(player, "You have added a working machine.");
                GameLog.Admin(player.Name, $"newjobcar({typejob},{model})", "");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"newjobveh\":\n" + e.ToString()); }
        }

        [Command("removejobcar")]
        public static void CMD_deletejveh(ExtPlayer player, int carID)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "removejobcar")) return;
                var vehicle = Trigger.GetVehicleById(carID);
                if (vehicle == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Technique with such ID was not found", 3000);
                    return;
                }
                if (vehicle.Data.OwnerType != OwnerType.Job)
                {

                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This technique is not working ", 3000);
                    return;
                }
                vehicle.Data.DeleteVehicle(vehicle);

                Chat.SendTo(player, $"Technique with ID{carID} Remove");
                GameLog.Admin(player.Name, $"removejobcar({carID})", $"");
            }
            catch(Exception e) {
                _logger.WriteError("EXCEPTION AT \"deljobveh\":\n" + e.ToString());
            }
        }

        [Command("setjobcar")]
        public static void setjobveh(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setjobcar")) return;
              
                if (!player.IsInVehicle)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You must be in technology", 3000);
                    return;
                }
                ExtVehicle extVehicle = player.Vehicle as ExtVehicle;

                if(extVehicle.Data.OwnerType != OwnerType.Job)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This technique is not working", 3000);
                    return;
                }
                extVehicle.Data.Position = player.Vehicle.Position;
                extVehicle.Data.Rotation = player.Vehicle.Rotation;
                MySQL.Query("UPDATE `vehicles` SET `position` = @prop0, `rotation`=@prop1 WHERE idket=@prop2", 
                    JsonConvert.SerializeObject(extVehicle.Data.Position), 
                    JsonConvert.SerializeObject(extVehicle.Data.Rotation),
                    extVehicle.Data.ID
                );
                
                Chat.SendTo(player, "You have changed the position of the work machine");
                GameLog.Admin(player.Name, $"setjobcar({extVehicle.Data.ID})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"setjobveh\":\n" + e.ToString()); }
        }

        [Command("checkban")]
        public static void ACMD_checkban(ExtPlayer player, string fullName)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "checkban")) return;
                var info = Ban.Banned.FirstOrDefault(b => b.Name == fullName);
                if (info == null)
                {
                    player.SendChatMessage("Couldn't find banned player");
                    return;
                }
                var hard = info.isHard ? "hard" : string.Empty; 
                player.SendChatMessage($"Player: {fullName} {hard} banned by {info.ByAdmin} until {info.Until.Date.ToString(CultureInfo.CurrentCulture)}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_newfracveh\":\n" + e.ToString()); }
        }
        
        [Command("newfracveh")]
        public static void ACMD_newfracveh(ExtPlayer player, string model, int fracid, int minRank, string number, int color1, int color2) // add rank, number
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "newfracveh")) return;
                number = number.ToUpper();
                if (VehicleManager.Vehicles.FirstOrDefault(item => item.Value.Number == number).Value != null)
                {
                    Chat.SendTo(player, "A car with such a number already exists");
                    return;
                }
                VehicleHash vh = (VehicleHash)NAPI.Util.GetHashKey(model);
                if (minRank <= 0 || color1 < 0 || color1 >= 160 || color2 < 0 || color2 >= 160)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The wrong color of the car!", 3000);
                    return;
                }
                if (vh == 0) return;
                var vehModel = new FractionVehicle(fracid, model, number, minRank, player.Position, player.Rotation, color1, color2, player.Dimension);
                vehModel.Spawn();

                Chat.SendTo(player, "You added a fraction car");
                GameLog.Admin(player.Name, $"newfracveh({fracid},{model})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_newfracveh\":\n" + e.ToString()); }
        }
        [Command("setfraccar")]
        public static void ACMD_setfracveh(ExtPlayer player) {
            try {
                if (!Group.CanUseAdminCommand(player, "setfraccar")) return;
                if (!player.IsInVehicle) 
                {
                    Chat.SendTo(player,"You must sit in a faction that you want to change");
                    return;
                }
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                
                if (vehicle == null) 
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It is necessary to be in the car ", 3000);
                    return;
                }

                if (vehicle.Data.OwnerType == OwnerType.Fraction)
                {
                    Vector3 pos = NAPI.Entity.GetEntityPosition(vehicle) + new Vector3(0, 0, 0.5);
                    Vector3 rot = NAPI.Entity.GetEntityRotation(vehicle);

                    vehicle.Data.Position = pos;
                    vehicle.Data.Rotation = rot;

                    MySQL.Query("UPDATE vehicles SET position = @prop0, rotation = @prop1, dimension = @prop2 WHERE idkey = @prop3", JsonConvert.SerializeObject(pos), JsonConvert.SerializeObject(rot), vehicle.Dimension, vehicle.Data.ID);

                    Chat.SendTo(player,"You have changed the tuning of this machine for the faction");
                    GameLog.Admin(player.Name, $"setfraccar({vehicle.Data.ID})", $"");
                } 
                else 
                    Chat.SendTo(player,"You must sit in a faction car that you want to change ");
            } catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_setfracveh\":\n" + e.ToString()); }
        }
        [Command("changefracvehrank")]
        public static void ACMD_ChangeFractionVehicleMinimalRank(ExtPlayer player, int newRank)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "changefracvehrank")) return;

                if (!player.IsInVehicle)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not in the car!", 3000);
                    return;
                }

                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (vehicle.Data.OwnerType != OwnerType.Fraction)
                    return;

                (vehicle.Data as FractionVehicle).MinRank = newRank;

                MySQL.Query("UPDATE `vehicles` SET `rank` = @prop1 WHERE `idkey` = @prop0", vehicle.Data.ID, newRank);

                Chat.SendTo(player,"changefracvehrank".Translate(newRank));
                GameLog.Admin(player.Name, $"changefracvehrank({vehicle.Data.ID},{newRank})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_delfracveh\":\n" + e.ToString()); }
        }
        [Command("delfracveh")]
        public static void ACMD_delfracvehe(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "delfracvehe")) return;

                if (!player.IsInVehicle)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not in the car ", 3000);
                    return;
                }

                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (vehicle.Data.OwnerType != OwnerType.Fraction)
                    return;
                int carId = vehicle.Data.ID;
                vehicle.Data.DeleteVehicle(vehicle);

                Chat.SendTo(player, "You have successfully removed the faction machine");
                GameLog.Admin(player.Name, $"delfracvehe({carId})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_delfracveh\":\n" + e.ToString()); }
        }
        [Command("delfraccar")]
        public static void ACMD_delfracveh(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "delfraccar")) return;
                var vehicle = Trigger.GetVehicleById(id);
                if (vehicle == null)
                    return;
                if (vehicle.Data.OwnerType != OwnerType.Fraction)
                    return;
                int carId = vehicle.Data.ID;
                vehicle.Data.DeleteVehicle(vehicle);
                Chat.SendTo(player, "You have successfully removed the faction machine");
                GameLog.Admin(player.Name, $"delfraccar({carId})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"ACMD_delfracveh\":\n" + e.ToString()); }
        }

        [Command("vehhash")]
        public static void CMD_createVehicleHash(ExtPlayer player, string name, int a, int b)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "vehhash")) return;
                var vehicle = VehicleManager.CreateTemporaryVehicle(Convert.ToInt32(name, 16), player.Position, player.Rotation, "Admin", VehicleAccess.Admin);
                vehicle.Dimension = player.Dimension;
                vehicle.PrimaryColor = a;
                vehicle.SecondaryColor = b;
                SafeTrigger.SetData(vehicle, "ACCESSADMINBY", player.Name);
                VehicleStreaming.SetEngineState(vehicle, true);
                GameLog.Admin(player.Name, $"vehhash({name})", $"");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD_vehhash\":\n" + e.ToString()); }
        }

        //[Command("aclear")]
        //public static void ACMD_aclear(ExtPlayer player, string target) {
        //    try {
        //        if (!player.IsLogged()) return;
        //        if (!Group.CanUseCmd(player, "aclear")) return;
        //        var uuid = Main.PlayerNames.FirstOrDefault(item => item.Value == target).Key;
        //        if (uuid == 0)
        //        {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Core_224", 3000);
        //            return;
        //        }
        //        if (Trigger.GetPlayerByUuid(uuid) != null)
        //        {
        //            Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "Core_267", 3000);
        //            return;
        //        }
        //        // CLEAR BIZ
        //        DataTable result = MySQL.QueryRead("SELECT uuid, adminlvl, biz, bank, fraction FROM `characters` WHERE uuid = @prop0", uuid);
        //        if (result != null && result.Rows.Count != 0) {
        //            DataRow row = result.Rows[0];
        //            if(Convert.ToInt32(row["adminlvl"]) >= player.Character.AdminLVL) {
        //                Chat.SendToAdmins(3, "Com_100".Translate( player.Name, player.Value, target));
        //                return;
        //            }
        //            Manager.GetFraction(Convert.ToInt32(row["fraction"]))?.DeleteMember(uuid);
        //            var biz = BusinessManager.GetBusinessByOwner(uuid);
        //            if (biz != null)
        //            {
        //                biz.SetOwner(-1);
        //                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Core_269".Translate(target), 3000);
        //            }
        //            // CLEAR BANK MONEY
        //            Bank.Data bankAcc = Bank.Get(Convert.ToInt32(row["bank"]));
        //            if (bankAcc != null)
        //                Bank.Set(bankAcc.ID, 0);
        //        } else {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Core_270", 3000);
        //            return;
        //        }
        //        // CLEAR HOUSE
        //        House house = HouseManager.GetHouse(uuid, OwnerType.Personal, true);
        //        if (house != null)
        //        {
        //            house.SetOwner(-1, 0);
        //            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Core_271".Translate(target), 3000);
        //        }
        //        // CLEAR VEHICLES
        //        var vehicles = VehicleManager.getAllHolderVehicles(uuid, VehicleType.Personal);
        //        foreach (int item in vehicles)
        //        {
        //            VehicleManager.Remove(item);
        //        }


        //        // CLEAR MONEY, HOTEL, FRACTION, SIMCARD, PET
        //        MySQL.Query("UPDATE `characters` SET `money`=0 WHERE uuid = @prop0", uuid);

        //        // CLEAR ITEMS
        //        //if(tuuid != 0) MySQL.Query("UPDATE `inventory` SET `items`='[]' WHERE `uuid` = @prop0", tuuid);
        //        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Core_273".Translate( target), 3000);
        //        GameLog.Admin($"{player.Name}", $"aClear", $"{target}");
        //    } catch (Exception e) { _logger.WriteError("EXCEPTION AT aclear\n" + e.ToString()); }
        //}

        [Command("findbyveh")]
        public static void CMD_FindByVeh(ExtPlayer player, string number) {
            try
            {
                if (!Group.CanUseAdminCommand(player, "findbyveh")) return;
                if (number.Length > 8)
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The number of characters in the license plate cannot exceed 8", 3000);
                    return;
                }
                var vehData = VehicleManager.Vehicles.FirstOrDefault(item => item.Value.Number == number);
                if (vehData.Value != null)
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Machine number: {number} | Model: {vehData.Value.ModelName} | Owner: {vehData.Value.GetHolderName()}", 6000);
                else
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A car with such a license plate was not found", 3000);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_FindByVeh: {e}");
            }
        }

        [Command("findvehbynumber")]
        public static void CMD_FindVehByNumber(ExtPlayer player, string number)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "findvehbynumber")) return;
                var vehicles = Trigger.GetVehiclesByNumberPlate(number);
                foreach (var veh in vehicles)
                {
                    Chat.SendTo(player, $"model: {veh.GetModelName()}, pos: {veh.Position.X} {veh.Position.Y} {veh.Position.Z}, dim: {veh.Dimension}, id: {veh.Value}");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"findvehbynumber: {e}");
            }
        }

        [Command("findvehholder")]
        public static void CMD_FindVehByHolder(ExtPlayer player, int holder, int type)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (!Group.CanUseAdminCommand(player, "findvehholder")) return;
                var vehicles = Main.AllVehicles.Where(item => item.Value.Data.OwnerID == holder && (int)item.Value.Data.OwnerType == type);
                foreach (var veh in vehicles)
                {
                    Chat.SendTo(player, $"model: {veh.Value.GetModelName()}, pos: {veh.Value.Position.X} {veh.Value.Position.Y} {veh.Value.Position.Z}, dim: {veh.Value.Dimension}, id: {veh.Value.Value}");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"findvehholder: {e}");
            }
        }

        //[Command("character")]
        //public static void SelectCharacter(ExtPlayer player, int number)
        //{
        //    try
        //    {

        //        number--;
        //        if (!player.IsLogged()) return;
        //        if (number < 0 || number > 2)
        //        {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "selectchar:err", 3000);
        //            return;
        //        }
        //        var acc = player.Account;
        //        if (acc.Characters[number] == -1)
        //        {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "selectchar:err:2", 3000);
        //            return;
        //        }
        //        acc.SetLastCharacter(number);
        //        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "selectchar:succ", 3000);
        //        NAPI.Task.Run(() =>{player.Kick();}, 1000);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.WriteError($"SelectCharacter: {e}");
        //    }
        //}

        [Command("weather")]
        public static void CMD_SetWeather(ExtPlayer player, byte weather)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "weather")) return;
                WeatherManager.ChangeWeather(weather);
                GameLog.Admin($"{player.Name}", $"setWeather({weather})", $"");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SetWeather: {e}");
            }
        }

        [Command("swtime")]
        public static void CMD_StopTime(ExtPlayer player, int val)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "swtime")) return;
                NAPI.ClientEvent.TriggerClientEventForAll("switchTime", val);
                GameLog.Admin(player.Name, $"switchTime({val})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_StopTime: {e}");
            }
        }
        [Command("setRain")]
        public static void CMD_SetRain(ExtPlayer player, float rain)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setRain")) return;
                SafeTrigger.ClientEvent(player,"weather:set:rain", rain);
                GameLog.Admin(player.Name, $"setRain({rain})", "");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_SetRain: {e}");
            }
        }
        [Command("st")]
        public static void CMD_setTime(ExtPlayer player, int hours, int minutes, int seconds)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "st")) return;
                NAPI.World.SetTime(hours, minutes, seconds);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_setTime: {e}");
            }
        }

        [Command("tp")]
        public static void CMD_teleport(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tp")) return;

                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                // Teleport the admin to the target player
                Admin.teleportToPlayer(player, target);

                // Notify the targeted player
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter,
                    $"Admin {player.Name} has teleported to you.", 3000);

                // Notify the admin
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You have teleported to player {target.Name}.", 3000);

                // Notify all admins
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has teleported to player {target.Name}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_tp\":\n" + e.ToString());
            }
        }


        [Command("goto")]
        public static void CMD_teleportveh(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tp")) return;
                if(!player.IsInVehicle) return;
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                
                player.Vehicle.Dimension = NAPI.Entity.GetEntityDimension(target);
                player.Vehicle.Position = target.Position + new Vector3(2, 2, 2);
                AdminParticles.PlayAdminAppearanceEffect(player);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("flip")]
        public static void CMD_flipveh(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tp")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such a ID was not found", 3000);
                    return;
                }
                if(!target.IsInVehicle) {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player is not in the car", 3000);
                    return;
                }
                target.Vehicle.Position = target.Vehicle.Position + new Vector3(0,0,2.5f);
                target.Vehicle.Rotation = new Vector3(0,0,target.Vehicle.Rotation.Z);
                GameLog.Admin($"{player.Name}", $"flipVeh", $"{target.Name}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("flipcar")]
        public static void CMD_flipCar(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "tp")) return;
                var vehicle = NAPI.Pools.GetAllVehicles().FirstOrDefault(v => v.Value == id);
                if (vehicle == null)
                {
                    Notify.SendError(player, "Not low transportation");
                    return;
                }
                vehicle.Position = vehicle.Position + new Vector3(0,0,2.5f);
                vehicle.Rotation = new Vector3(0,0,vehicle.Rotation.Z);
                GameLog.Admin($"{player.Name}", $"flipVeh", $"{vehicle.Model}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("createbusiness")]
        public static void CMD_createBiz(ExtPlayer player, int govPrice, int type)
        {
            try
            {
                BusinessManager.createBusinessCommand(player, govPrice, type);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("createunloadpoint")]
        public static void CMD_createUnloadPoint(ExtPlayer player, int bizid)
        {
            try
            {
                BusinessManager.createBusinessUnloadpoint(player, bizid);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("deletebusiness")]
        public static void CMD_deleteBiz(ExtPlayer player, int bizid)
        {
            try
            {
                BusinessManager.deleteBusinessCommand(player, bizid);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
 
        [Command("jail", GreedyArg = true)]
        public static void CMD_sendTargetToDemorgan(ExtPlayer player, int id, int time, string reason)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                Admin.sendPlayerToDemorgan(player, Trigger.GetPlayerByUuid(id), time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("loadipl")]
        public static void CMD_LoadIPL(ExtPlayer player, string ipl) {
            try {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                NAPI.World.RequestIpl(ipl);
                Chat.SendTo(player, $"You downloaded IPL: {ipl}");
            } catch {
            }
        }
        [Command("unloadipl")]
        public static void CMD_UnLoadIPL(ExtPlayer player, string ipl) {
            try {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                NAPI.World.RemoveIpl(ipl);
                Chat.SendTo(player, $"You unloaded IPL: {ipl}");
            } catch(Exception e) {
                _logger.WriteError($"CMD_UnLoadIPL:\n{e}");
            }
        }

        [Command("starteffect")]
        public static void CMD_StartEffect(ExtPlayer player, string effect, int dur = 0, bool loop = false) {
            try {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                SafeTrigger.ClientEvent(player, "startScreenEffect", effect, dur, loop);
                Chat.SendTo(player, $"You turned on Effect: {effect}");
            } catch {
            }
        }
        [Command("stopeffect")]
        public static void CMD_StopEffect(ExtPlayer player, string effect) {
            try {
                if (!Group.CanUseAdminCommand(player, "setvehdirt")) return;
                SafeTrigger.ClientEvent(player, "stopScreenEffect", effect);
                Chat.SendTo(player, "You turned on Effect:{effect}");
            } catch {
            }
        }
        [Command("aunjail")]
        public static void CMD_releaseTargetFromDemorgan(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "udemorgan")) return;
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.ReleasePlayerFromDemorgan(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("unjail")]
        public static void CMD_releaseTargetFromJail(ExtPlayer player, int id)
        {
            if (!Group.CanUseAdminCommand(player, "unjail")) return;

            ExtPlayer target = Trigger.GetPlayerByUuid(id);
            if (target == null)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                return;
            }

            Admin.ReleasePlayerFromJail(player, target);
        }

        [Command("offjail", GreedyArg = true)]
        public static void CMD_offlineJailTarget(ExtPlayer player, string target, int time, string reason)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "offjail")) return;
                if (!Main.PlayerNames.ContainsValue(target))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player was not found", 3000);
                    return;
                }
                if(player.Name.Equals(target)) return;
                if (Trigger.GetPlayerByName(target) != null)
                {
                    Admin.sendPlayerToDemorgan(player, Trigger.GetPlayerByName(target), time, reason);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so Offjail was replaced by Demorgan", 3000);
                    return;
                }

                var uuid = Main.PlayerNames.FirstOrDefault(p => p.Value == target).Key;
                if (uuid == default) return;

                var firstTime = time * 60;
                var responce = MySQL.QueryRead("SELECT `vipdate` FROM `characters` WHERE `uuid`=@prop0", uuid);
                if (responce != null && responce.Rows.Count > 0)
                {
                    var vipDate = (DateTime)responce.Rows[0]["vipdate"];
                    if (vipDate > DateTime.UtcNow) time /= 2;
                }

                var deTimeMsg = "m";
                if (time > 60)
                {
                    deTimeMsg = "h";
                    time /= 60;
                    if (time > 24)
                    {
                        deTimeMsg = "d";
                        time /= 24;
                    }
                }
                MySQL.QuerySync("UPDATE characters SET demorgan = @prop0, arrest = 0 WHERE uuid = @prop1", firstTime, uuid);
                Chat.AdminToAll($"{player.Name} Plant the player{target} in special.Prison on {time} {deTimeMsg} for: {reason}");
                GameLog.Admin($"{player.Name}", $"demorgan({time}{deTimeMsg},{reason})", $"{target}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("offwarn", GreedyArg = true)]
        public static void CMD_offlineWarnTarget(ExtPlayer player, string target, int time, string reason)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "offwarn")) return;
                var uuid = Main.PlayerNames.FirstOrDefault(item => item.Value == target).Key;
                if (uuid == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player is not found ", 3000);
                    return;
                }
                if(player.Name.Equals(target)) return;
                if (Trigger.GetPlayerByName(target) != null)
                {
                    Admin.warnPlayer(player, Trigger.GetPlayerByName(target) as ExtPlayer, reason);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "Online player", 3000);
                    return;
                } 
                else 
                {
                    string[] split1 = target.Split('_');
                    DataTable result = MySQL.QueryRead("SELECT adminlvl FROM characters WHERE uuid = @prop0", uuid);
                    DataRow row = result.Rows[0];
                    int targetadminlvl = Convert.ToInt32(row[0]);
                    if(targetadminlvl >= player.Character.AdminLVL) {
                        Chat.SendToAdmins(3, $"[OFFWARN-DENIED] {player.Name} ({player.Character.UUID}) I tried to block{target} (offline), High -level administrator.");
                        return;
                    }
                }

                
                var split = target.Split('_');
                var data = MySQL.QueryRead("SELECT warns, fraction FROM characters WHERE firstname = @prop0 AND lastname = @prop1", split[0], split[1]);
                var warns = Convert.ToInt32(data.Rows[0]["warns"]);
                var frac = Convert.ToInt32(data.Rows[0]["fraction"]);
                Manager.GetFraction(frac)?.DeleteMember(uuid);
                warns++;
                if (warns >= 3)
                {
                    MySQL.Query("UPDATE `characters` SET `warns`=0 WHERE uuid = @prop0", uuid);
                    Ban.Offline(target, DateTime.Now.AddMinutes(43200), false, "Warns 3/3", $"{player.Name}");
                }
                else
                    MySQL.Query("UPDATE `characters` SET `unwarn`=@prop0,`warns`=@prop1 WHERE uuid=@prop2 ", MySQL.ConvertTime(DateTime.Now.AddDays(14)), warns, uuid);

                Chat.AdminToAll($"{player.Name} I gave a warning to the player {target} ({warns}/3 | cause: {reason})");
                GameLog.Admin($"{player.Name}", $"warn({time},{reason})", $"{target}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }


        [Command("ban")]
        public static async Task CMD_banTarget(ExtPlayer player, int id, int time, string reason)
        {
            try
            {
                // Check if the player has permission to ban
                if (!Group.CanUseAdminCommand(player, "ban")) return;

                // Check if the player with the given ID exists
                var targetPlayer = Trigger.GetPlayerByUuid(id);
                if (targetPlayer == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"No player found with ID: {id}.", 3000);
                    return;
                }

                if (player.Character.AdminLVL < targetPlayer.Character.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player has higher Admin level than you".Translate(), 3000);
                    return;
                }

                // Trigger ban animation for the target player
                string banDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                NAPI.ClientEvent.TriggerClientEvent(targetPlayer, "showBanAnimation",
                   targetPlayer.Name,  // playerName
                   player.Name,        // adminName
                   reason,             // reason
                   time,               // daysLeft
                   banDate             // banDate
                );

                // Add a delay before banning the player (3 seconds delay)
                await Task.Delay(5000);  // Delay in milliseconds

                // Ban the player
                Admin.BanPlayer(player, targetPlayer, time, reason, false);

                // Notify the admin who used the command
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully banned player with ID: {id}.", 3000);

                // Log the banning action
                GameLog.Admin($"{player.Name}", $"banPlayer(ID: {id}, time: {time}, reason: {reason})", $"Player with ID {id} was banned.");

                // Send log message to all admins
                Chat.SendToAdmins(1, $"Admin {player.Name} (Level: {player.Character.AdminLVL}) has banned player with ID: {id}. Reason: {reason}");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_banTarget\":\n" + e.ToString());
            }
        }

        [Command("hardban", GreedyArg = true)]
        public static void CMD_hardbanTarget(ExtPlayer player, int id, int time, string reason)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.hardbanPlayer(player, Trigger.GetPlayerByUuid(id), time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("templeader")]
        public static void CMD_tempLeader(ExtPlayer player, int orgId)
        {
            try
            {
                // Validate organization ID
                if (orgId <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,
                        "Invalid organization ID. Please provide a valid value.", 3000);
                    return;
                }

                // Check if the admin is already set as a temporary leader
                if (player.GetData<bool>("TEMP_LEADER"))
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter,
                        "You are already a temporary leader for an organization.", 3000);
                    return;
                }

                // Mark the admin as a temporary leader
                player.SetData("TEMP_LEADER", true);
                player.SetData("TEMP_ORG_ID", orgId);

                // Notify the admin
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"You are now temporarily the leader of Organization ID: {orgId}.", 5000);

                // Grant organization leader privileges
                player.SetData("OrgAccess", "Leader");
                player.SetData("OrgMoneyAccess", true);

                // Log the action in admin chat
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} is now temporarily the leader of Organization ID: {orgId}.");
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"CMD_tempLeader\":\n{e}");
            }
        }

        // Event listener for player disconnection
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(ExtPlayer player, DisconnectionType type, string reason)
        {
            try
            {
                // Check if the player was a temporary leader
                if (player.GetData<bool>("TEMP_LEADER"))
                {
                    // Reset temporary leadership data
                    player.ResetData("TEMP_LEADER");
                    player.ResetData("TEMP_ORG_ID");
                    player.ResetData("OrgAccess");
                    player.ResetData("OrgMoneyAccess");

                    // Log the reset
                    _logger.WriteInfo($"Player {player.Name}'s temporary leadership was reset on disconnection.");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"EXCEPTION AT \"OnPlayerDisconnected\":\n{e}");
            }
        }


        [Command("offban", GreedyArg = true)]
        public static void CMD_offlineBanTarget(ExtPlayer player, string name, int time, string reason)
        {
            try
            {
                if (!Main.PlayerNames.ContainsValue(name))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with that name was not found ", 3000);
                    return;
                }
                Admin.offBanPlayer(player, name, time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("offhardban", GreedyArg = true)]
        public static void CMD_offlineHardbanTarget(ExtPlayer player, string name, int time, string reason)
        {
            try
            {
                if (!Main.PlayerNames.ContainsValue(name))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with that name was not found", 3000);
                    return;
                }
                Admin.offHardBanPlayer(player, name, time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("unban")]
        public static void CMD_unbanTarget(ExtPlayer player, int id)
        {
            try
            {
                // Check if the player has permission to unban
                if (!Group.CanUseAdminCommand(player, "unban")) return;

                // Find the player by their ID (either online or offline)
                var targetPlayer = Trigger.GetPlayerByUuid(id);

                // If the player is online, use their name for unban
                if (targetPlayer != null)
                {
                    string playerName = targetPlayer.Name;

                    // Proceed to unban the player (online case)
                    Admin.unbanPlayer(player, playerName);

                    // Notify the admin who used the command
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully unbanned player with ID: {id}.", 3000);

                    // Log the unbanning action
                    GameLog.Admin($"{player.Name}", $"unbanPlayer(ID: {id})", $"Player with ID {id} ({playerName}) was unbanned.");

                    // Send log message to all admins
                    Chat.SendToAdmins(1, $"Admin {player.Name} has unbanned player with ID: {id} ({playerName}).");
                }
                else
                {
                    // Player is offline, check the database to unban
                    string connectionString = "Server=localhost;Database=aqua;User=root;Password=;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check if the player is in the banned list by their ID (uuid column)
                        string checkQuery = "SELECT COUNT(*) FROM banned WHERE uuid = @uuid";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@uuid", id);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                            // If no record is found, notify the admin
                            if (count == 0)
                            {
                                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player with ID {id} is not found in the banned list.", 3000);
                                return;
                            }
                        }

                        // If the player is banned, proceed to unban them by deleting the record from the banned table
                        string deleteQuery = "DELETE FROM banned WHERE uuid = @uuid";
                        using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCmd.Parameters.AddWithValue("@uuid", id);
                            deleteCmd.ExecuteNonQuery(); // Delete the record from the banned table
                        }
                    }

                    // Notify the admin who used the command for offline player
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully unbanned player with ID: {id} (Offline).", 3000);

                    // Log the unbanning action for offline player
                    GameLog.Admin($"{player.Name}", $"unbanPlayer(ID: {id})", $"Player with ID {id} was unbanned (Offline).");

                    // Send log message to all admins
                    Chat.SendToAdmins(1, $"Admin {player.Name} has unbanned player with ID: {id} (Offline).");
                }
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_unbanTarget\":\n" + e.ToString());
            }
        }


        [Command("unhardban")]
        public static void CMD_unhardbanTarget(ExtPlayer player, string uuid)
        {
            try
            {
                // Check if the player has permission to unhardban
                if (!Group.CanUseAdminCommand(player, "unhardban")) return;

                // Database connection string (modify if necessary)
                string connectionString = "Server=localhost;Database=aqua;User=root;Password=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the player is in the banned list by their UUID
                    string checkQuery = "SELECT COUNT(*) FROM banned WHERE uuid = @uuid";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@uuid", uuid);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        // If no record is found, notify the admin
                        if (count == 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player with UUID {uuid} is not found in the banned list.", 3000);
                            return;
                        }
                    }

                    // If the player is banned, proceed to unban them by deleting the record
                    string deleteQuery = "DELETE FROM banned WHERE uuid = @uuid";
                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@uuid", uuid);
                        deleteCmd.ExecuteNonQuery(); // Delete the record from the banned table
                    }
                }

                // Notify the admin who used the command
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have successfully unhardbanned player with UUID: {uuid}.", 3000);

                // Log the unhardban action
                GameLog.Admin($"{player.Name}", $"unhardbanPlayer(UUID: {uuid})", $"Player with UUID {uuid} was unhardbanned.");

                // Send log message to all admins
                Chat.SendToAdmins(1, $"Admin {player.Name} has unhardbanned player with UUID: {uuid}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_unhardbanTarget\":\n" + e.ToString());
            }
        }



        [Command("givedonateoff")]
        public static void CMD_offdonate(ExtPlayer client, string name, long amount)
        {
            if (!Group.CanUseAdminCommand(client, "givedonateoff")) return;
            try
            {
                name = name.ToLower();
                
                var ExtPlayer = Main.GetExtPlayerByPredicate(ExtPlayer => ExtPlayer.Account.Login == name);
                if (ExtPlayer != null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, $"The player is online!", 8000);
                    return;
                }
                MySQL.Query("update `accounts` set `mcoins`=`mcoins`+@prop0 where `login`=@prop1", amount, name);
                GameLog.Admin(client.Name, $"offgivedonate({amount})", name);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("updatemail")]
        public static void CMD_UpdateMail(ExtPlayer client, string login, string newEmail)
        {
            if (!Group.CanUseAdminCommand(client, "updatemail")) return;
            var currEmail = Main.Emails.FirstOrDefault(item => item.Value == login);
            if (currEmail.Key == null)
            {
                Notify.SendError(client, "Login was not found");
                return;
            }
            Main.Emails.Remove(currEmail.Key);
            Main.Emails.Add(newEmail, currEmail.Value);

            var ExtPlayer = Main.GetExtPlayerByPredicate(ExtPlayer => ExtPlayer.Account.Login == login);
            if (ExtPlayer != null)
                ExtPlayer.Account.UpdateEmail(newEmail);
            else
                MySQL.Query("update `accounts` set `email` = @prop0 where `login`=@prop1", newEmail, login);
            GameLog.Admin(client.Name, $"updatemail({newEmail})", login);
        }
        [Command("mute", GreedyArg = true)]
        public static void CMD_muteTarget(ExtPlayer player, int id, int time, string reason)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.mutePlayer(player, Trigger.GetPlayerByUuid(id) as ExtPlayer, time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("offmute", GreedyArg = true)]
        public static void CMD_offlineMuteTarget(ExtPlayer player, string target, int time, string reason)
        {
            try
            {
                if (!Main.PlayerNames.ContainsValue(target))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player was not found", 3000);
                    return;
                }
                Admin.OffMutePlayer(player, target, time, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("unmute")]
        public static void CMD_muteTarget(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.unmutePlayer(player, Trigger.GetPlayerByUuid(id) as ExtPlayer);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("vmute")]
        public static void CMD_voiceMuteTarget(ExtPlayer player, int id)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!Group.CanUseAdminCommand(player, "mute")) return;

                if (player.Character.AdminLVL < target.Character.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player has higher Admin lvl than you", 3000);
                    return;
                }

                target.SetSharedData("voice.muted", true);
                SafeTrigger.ClientEvent(target, "voice.mute");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("vunmute")]
        public static void CMD_voiceUnMuteTarget(ExtPlayer player, int id)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }

                if (!Group.CanUseAdminCommand(player, "unmute")) return;
                target.SetSharedData("voice.muted", false);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("sban")]
        public static void CMD_silenceBan(ExtPlayer player, int id, int time)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "sban")) return;
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.BanPlayer(player, Trigger.GetPlayerByUuid(id), time, "", true);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("kick", GreedyArg = true)]
        public static void CMD_kick(ExtPlayer player, int id, string reason)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.kickPlayer(player, Trigger.GetPlayerByUuid(id), reason, false);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("skick")]
        public static void CMD_silenceKick(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not foundn", 3000);
                    return;
                }
                Admin.kickPlayer(player, Trigger.GetPlayerByUuid(id), "Silence kick", true);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
      [Command("gm")]
public static void CMD_enableGodmode(ExtPlayer player)
{
    // Check if the player has permission to use the command
    if (!Group.CanUseAdminCommand(player, "gm")) return;

    // Check if god mode is already enabled
    if (!player.HasData("AGM") || !player.GetData<bool>("AGM"))
    {
        // Enable god mode
        SafeTrigger.ClientEvent(player, "AGM", true);
        player.SetData("AGM", true);

        // Notify the player
        player.SendChatMessage("~g~God mode enabled.");
    }
    else
    {
        // Disable god mode
        SafeTrigger.ClientEvent(player, "AGM", false);
        player.SetData("AGM", false);

        // Notify the player
        player.SendChatMessage("~r~God mode disabled.");
    }
}

        
        [Command("warn", GreedyArg = true)]
        public static void CMD_warnTarget(ExtPlayer player, int id, string reason)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.warnPlayer(player, Trigger.GetPlayerByUuid(id), reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("pm", GreedyArg = true)]
        public static void CMD_adminSMS(ExtPlayer player, int id, string msg)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.adminSMS(player, Trigger.GetPlayerByUuid(id), msg);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
       
       
        [Command("setprime")]
        public static void CMD_SetPlayerPrime(ExtPlayer player, int id, int days)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Admin.setPlayerPrimeAccount(player, Trigger.GetPlayerByUuid(id), days);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("checkmoney")]
        public static void CMD_checkMoney(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send (player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such a ID was not found", 3000);
                    return;
                }
                Admin.checkMoney(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("reportoff")]
        public static void CMD_reportoff(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "reportoff")) return;

                if (player.Character.ReportNotification == true)
                {
                    player.Character.ReportNotification = false;
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, "Notifications of reports are disabled", 3000);
                }
                else
                {
                    player.Character.ReportNotification = true;
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Notifications of reports are included", 3000);
                }
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }        

        [Command("allseeingeye")]
        public static void CMD_allseeingeye(ExtPlayer player, int flag)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "takeadmin")) return;
                if (flag == 1)
                    SafeTrigger.SetData(player, "ALLSEEINEYE", true);
                else
                    SafeTrigger.SetData(player, "ALLSEEINEYE", false);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        #endregion

        [Command("farmsave")]
        public static void CMD_SaveFarmPoint(ExtPlayer player, int farmId)
        {
            try
            {
                if (!Directory.Exists("farms"))
                {
                    Directory.CreateDirectory("farms");
                }

                using (StreamWriter saveCoords = new StreamWriter($"farms/{farmId}.txt", true, Encoding.UTF8))
                {
                    var pos = player.Position - new Vector3(0, 0, 1.12);

                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    saveCoords.Write($"new Vector3({pos.X}, {pos.Y}, {pos.Z}),\r\n");
                    saveCoords.Close();
                }
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"farmsave\":\n" + e.ToString()); }
        }

        [Command("checkdonat")]
        public static void CMD_checkDonat(ExtPlayer player, int hour)
        {
            if (!Group.CanUseAdminCommand(player, "checkdonat")) return;
            try
            {
                if (hour < 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The value should be more than 0", 3000);
                    return;
                };
                var result = MySQL.QueryRead($"SELECT SUM(`sum`) FROM `{Main.ServerConfig.DonateConfig.Database}` WHERE unitpayid > 0 AND NOW() -  INTERVAL @prop0 HOUR < `date`;", hour);
                if (result == null || result.Rows.Count == 0 || result.Rows[0][0] is DBNull)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Donate for this period was not found!", 3000);
                    return;
                };
                var sum = Convert.ToInt32(result.Rows[0][0]);
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"For the latter {hour} The clock came {sum} rubles", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        #region VipCommands
        [Command("leave")]
        public static void CMD_leaveFraction(ExtPlayer player)
        {
            try
            {
                if (!player.Character.IsPrimeActive()) return;

                DateTime now = DateTime.Now;
                if (player.Character.LastUsedPrimeLeave != null)
                {
                    DateTime lastUsed = (DateTime)player.Character.LastUsedPrimeLeave;
                    if (lastUsed.AddHours(24) > now)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You can leave the organization with the help of Prime no more than 1 time every 24 hours.", 3000);
                        return;
                    }
                }

                player.Character.LastUsedPrimeLeave = now;
                Fractions.Models.Fraction fraction = Manager.GetFraction(player);
                if (fraction == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not in the organization", 3000);
                    return;
                }
                fraction.DeleteMember(player.Character.UUID);
                Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "You left the organization ", 3000);

            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        #endregion

        [Command("ticket", GreedyArg = true)]
        public static void CMD_govTicket(ExtPlayer player, int id, int sum, string reason)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (sum < 1) return;
                if (target == null || !target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found ", 3000);
                    return;
                }
                if (target.Position.DistanceTo(player.Position) > 2)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player is far away", 3000);
                    return;
                }
                Fractions.FractionCommands.ticketToTarget(player, target, sum, reason);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("respawn")]
        public static void CMD_respawnFracCars(ExtPlayer player)
        {
            try
            {
                var fraction = Manager.GetFraction(player);
                if (fraction == null || !fraction.IsLeaderOrSub(player)) return;
                if (DateTime.Now < FractionCommands.NextCarRespawn[player.Character.FractionID])
                {
                    DateTime g = new DateTime((FractionCommands.NextCarRespawn[player.Character.FractionID] - DateTime.Now).Ticks);
                    var min = g.Minute;
                    var sec = g.Second;
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You can do this only through {min}:{sec}", 3000);
                    return;
                }
                Fractions.FractionCommands.RespawnFractionCars(player.Character.FractionID);

                FractionCommands.NextCarRespawn[player.Character.FractionID] = DateTime.Now.AddHours(2);
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You have grown all fractional machines", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("givemedlic")]
        public static void CMD_givemedlic(ExtPlayer player, int id)
        {
            try
            {
                var target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A player with such an ID was not found", 3000);
                    return;
                }
                if (target.Position.DistanceTo(player.Position) > 2)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is far away", 3000);
                    return;
                }
                FractionCommands.giveMedicalLic(player, target);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
               

        [Command("setgamblingtax")]
        public static void CmdSetGamblingTaxCallBack(ExtPlayer player, int percent)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "setgamblingtax")) return;
                if (percent < 0 || percent >= 100) return;
                var oldTax = CasinoManager.StateShare;
                CasinoManager.UpdateStateShare((double)percent / 100);
                Notify.Send(player, NotifyType.Success, NotifyPosition.Bottom, $"The tax was changed with {oldTax * 100}% on {CasinoManager.StateShare * 100}%", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        
        [Command("password")]
        public static void CMD_ResetPassword(ExtPlayer player, string new_password)
        {
            try
            {
                if (!player.IsLogged()) return;
                player.Account.changePassword(new_password);
                Notify.Send(player, NotifyType.Alert, NotifyPosition.BottomCenter, "You changed your password!Restart your game", 3000);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_ResetPassword: {e}");
            }
        }
        [Command("time")]
        public static void CMD_checkPrisonTime(ExtPlayer player)
        {
            try
            {
                if (player.Character.ArrestDate > DateTime.Now)
                {
                    var period = player.Character.ArrestDate - DateTime.Now;
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have to sit {Convert.ToInt32(period.TotalMinutes)} minutes", 3000);
                }                    
                else if (player.Character.DemorganTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have to sit {Convert.ToInt32(player.Character.DemorganTime / 60.0)} minutes", 3000);
                else if (player.Character.ArrestiligalTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have to sit {Convert.ToInt32(player.Character.ArrestiligalTime / 60.0)} minutes", 3000);
                else if (player.Character.CourtTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have to sit{Convert.ToInt32(player.Character.CourtTime / 60.0)} minutes", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("ptime")]
        public static void CMD_pcheckPrisonTime(ExtPlayer player, int id)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "a")) return;
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Ein Spieler mit einer solchen ID wurde nicht gefunden", 3000);
                    return;
                }
                if (target.Character.ArrestDate > DateTime.Now)
                {
                    var period = target.Character.ArrestDate - DateTime.Now;
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Player {target.Name} left {Convert.ToInt32(period.TotalMinutes)} minutes", 3000);
                }  
                else if (target.Character.DemorganTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Player {target.Name} left {Convert.ToInt32(target.Character.DemorganTime / 60.0)} minutes", 3000);
                    else if (target.Character.ArrestiligalTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Player {target.Name} left {Convert.ToInt32(target.Character.ArrestiligalTime / 60.0)} minutes", 3000);
                    else if (target.Character.CourtTime != 0)
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Player {target.Name} left {Convert.ToInt32(target.Character.CourtTime / 60.0)} minutes", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        
        [Command("q")]
        public static void CMD_disconnect(ExtPlayer player)
        {
            try
            {
                SafeTrigger.ClientEvent(player, "kick", null);
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_disconnect: {e}");
            }
        }

        [Command("eject")]
        public static void CMD_ejectTarget(ExtPlayer player, int id)
        {
            try
            {
                if (!player.IsInVehicle || player.VehicleSeat != VehicleConstants.DriverSeat)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You are not in the car or not in the driver's seat ", 3000);
                    return;
                }
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Ein Spieler mit einer solchen ID wurde nicht gefunden", 3000);
                    return;
                }
                if (!target.IsInVehicle || player.Vehicle != target.Vehicle) return;
                VehicleManager.WarpPlayerOutOfVehicle(target);

                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You threw out {target.Character.UUID} From the car", 3000);
                Notify.Send(target, NotifyType.Warning, NotifyPosition.BottomCenter, $"Player {player.Character.UUID} I threw you out of the car ", 3000);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        //[Command("pocket")]
        //public static void CMD_pocketTarget(ExtPlayer player, int id)
        //{
        //    try
        //    {
        //        if (Trigger.GetPlayerByUuid(id) == null)
        //        {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Core_281", 3000);
        //            return;
        //        }
        //        if (player.Position.DistanceTo(Trigger.GetPlayerByUuid(id).Position) > 2)
        //        {
        //            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Biz_52", 3000);
        //            return;
        //        }

        //        Fractions.FractionCommands.playerChangePocket(player, Trigger.GetPlayerByUuid(id));
        //    }
        //    catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        //}


        [Command("setrank")]
        public static void CMD_setRank(ExtPlayer player, int id, int newrank)
        {
            try
            {
                if (!Manager.CanUseCommand(player, "setrank")) return;
                if (newrank <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It is impossible to set a negative or zero rank", 3000);
                    return;
                }
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Ein Spieler mit einer solchen ID wurde nicht gefunden", 3000);
                    return;
                }
                FractionCommands.SetFracRank(player, target.Character.UUID, newrank);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("arrest")]
        public static void CMD_arrest(ExtPlayer player, int id)
        {
            try
            {
                ExtPlayer target = Trigger.GetPlayerByUuid(id);
                if (target == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                FractionCommands.arrestTarget(player, target);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("rfp")]
        public static void CMD_rfp(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                FractionCommands.releasePlayerFromPrison(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }
        [Command("unmuteall")]
        public static void CMD_unmuteall(ExtPlayer player, float radius, string reason)
        {
            try
            {
                // Check for invalid radius
                if (radius <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid radius. Please provide a valid value.", 3000);
                    return;
                }

                // Check if reason is provided
                if (string.IsNullOrWhiteSpace(reason))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You must provide a reason for unmuting players.", 3000);
                    return;
                }

                // Get the player's position
                Vector3 playerPosition = player.Position;

                // Get all players within the specified radius
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                // Check if any players are found
                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "No players found within the specified radius.", 3000);
                    return;
                }

                // Unmute all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        // Unmute the player using the existing unmute logic
                        Admin.unmutePlayer(player, extPlayer);

                        // Notify the unmuted player
                        Notify.Send(extPlayer, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"You have been unmuted. Reason: {reason}.", 3000);
                    }
                }

                // Notify the admin that the unmute process is complete
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"All players within a {radius}m radius have been unmuted. Reason: {reason}.", 5000);

                // Optionally, you could send an admin notification about the unmute
                Chat.SendToAdmins(1, $"~r~[Admin] {player.Name} has unmuted all players within a {radius}m radius. Reason: {reason}.");
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_unmuteall\":\n" + e.ToString());
            }
        }



        private static Dictionary<string, DateTime> adminLogoutTime = new Dictionary<string, DateTime>(); // To track logout times
        private static Dictionary<string, int> originalAdminLevels = new Dictionary<string, int>(); // To store original admin levels

        private const string AdminPassword = "PrimaryAdmins";  // Admin password for login

        // Admin login command
        [Command("adminlogin")]
        public static void AdminLoginCommand(ExtPlayer player, string password)
        {
            if (player == null || player.Character == null) return;

            // Check if the player is temporarily level 1 (logged out)
            if (player.Character.AdminLVL == 1 && adminLogoutTime.ContainsKey(player.SocialClubId.ToString()))
            {
                DateTime logoutTime = adminLogoutTime[player.SocialClubId.ToString()];
                TimeSpan cooldownTime = DateTime.Now - logoutTime;

                // Check if the 5-minute cooldown is over
                if (cooldownTime.TotalMinutes >= 5)
                {
                    // Restore the original admin level from memory
                    if (originalAdminLevels.ContainsKey(player.SocialClubId.ToString()))
                    {
                        // Update the player's admin level in memory (do not update the database here)
                        player.Character.AdminLVL = originalAdminLevels[player.SocialClubId.ToString()];
                        originalAdminLevels.Remove(player.SocialClubId.ToString()); // Remove from memory after restoring
                        adminLogoutTime.Remove(player.SocialClubId.ToString()); // Remove from memory after restoring

                        // Notify the player that they successfully logged in
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have successfully logged into the Admin Panel.", 3000);

                        // Send message to all admins
                        Chat.SendToAdmins(1,$"Admin {player.Name} (Level: {player.Character.AdminLVL}) has logged in after cooldown.");

                        return;
                    }
                }
                else
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You need to wait for the cooldown before logging in again.", 3000);
                    return;
                }
            }

            // Check if the entered password is correct
            if (password == AdminPassword)
            {
                // Fetch the player's original admin level from the database
                int originalAdminLevel = FetchAdminLevelFromDatabase(player);

                if (originalAdminLevel != -1)
                {
                    // Save the original admin level in memory (not in the database)
                    originalAdminLevels[player.SocialClubId.ToString()] = originalAdminLevel;

                    // Set the player's admin level to 1 temporarily (to indicate they're logged out)
                    player.Character.AdminLVL = 1;

                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You are now logged out as an admin, you can log back in after 5 minutes.", 3000);

                    // Send a message to all admins about the logout
                    Chat.SendToAdmins(1,$"Admin {player.Name} (Level: {originalAdminLevel}) has logged out. They will be able to log in again in 5 minutes.");

                    return;
                }
                else
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Admin level not found. Please try again later.", 3000);
                }
            }
            else
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Incorrect password. Please try again.", 3000);
            }
        }



        // Admin logout command
        [Command("adminlogout")]
        public static void AdminLogoutCommand(ExtPlayer player)
        {
            if (player == null || player.Character == null) return;

            // Check if the player is logged in as an admin
            if (player.Character.AdminLVL > 0)
            {
                int adminLevel = player.Character.AdminLVL;

                // Store the original admin level in memory before setting it temporarily to 1 (logged out)
                originalAdminLevels[player.SocialClubId.ToString()] = adminLevel;

                // Set the admin level temporarily to 1 (for logout status)
                player.Character.AdminLVL = 1;

                // Store the logout time in memory
                adminLogoutTime[player.SocialClubId.ToString()] = DateTime.Now;

                // Send success message to the player
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have logged out as an admin. You will be able to log back in after 5 minutes.", 3000);
                Console.WriteLine($"[ADMIN LOGOUT] {player.Name} (Level: {adminLevel}) has logged out of the Admin Panel.");

                // Notify all admins about the logout
                Chat.SendToAdmins(1, $"Admin {player.Name} (Level: {adminLevel}) has logged out of the Admin Panel.");
            }
            else
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not logged in as an admin.", 3000);
            }
        }

        // Fetch Admin Level from the database based on the player's SocialClubId
        private static int FetchAdminLevelFromDatabase(ExtPlayer player)
        {
            int adminLevel = -1; // Default to -1 if not found

            string socialClubId = player.SocialClubId.ToString();  // Assuming this is how the server stores the Social Club ID

            string connectionString = "Server=localhost;Database=aqua;User ID=root;Password=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT adminlvl FROM characters WHERE SocialClubId = @socialClubId LIMIT 1;";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        // Use parameterized query to avoid SQL injection
                        command.Parameters.AddWithValue("@socialClubId", socialClubId);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            adminLevel = Convert.ToInt32(result);  // Convert the result to int
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching admin level: {ex.Message}");
                }
            }

            return adminLevel;  // Return the admin level (or -1 if not found)
        }



        [Command("muteall")]
        public static void CMD_muteall(ExtPlayer player, float radius, int durationInMinutes, string reason)
        {
            try
            {
                if (radius <= 0 || durationInMinutes <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid radius or duration. Please provide valid values.", 3000);
                    return;
                }

                if (string.IsNullOrWhiteSpace(reason))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You must provide a reason for muting players.", 3000);
                    return;
                }

                Vector3 playerPosition = player.Position;
                var playersInRadius = NAPI.Pools.GetAllPlayers().Where(p =>
                    p != null &&
                    p.Exists &&
                    p.Position.DistanceTo(playerPosition) <= radius &&
                    p != player);

                if (!playersInRadius.Any())
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "No players found within the specified radius.", 3000);
                    return;
                }

                // Mute all players in the radius
                foreach (var targetPlayer in playersInRadius)
                {
                    if (targetPlayer is ExtPlayer extPlayer)
                    {
                        // Mute the player using the existing mute system
                        Admin.mutePlayer(player, extPlayer, durationInMinutes, reason);

                        // Notify the muted player
                        Notify.Send(extPlayer, NotifyType.Warning, NotifyPosition.BottomCenter,
                            $"You have been muted for {durationInMinutes} minutes. Reason: {reason}.", 5000);
                    }
                }

                // Broadcast message to all players
                foreach (var targetPlayer in NAPI.Pools.GetAllPlayers())
                {
                    if (targetPlayer != null && targetPlayer.Exists)
                    {
                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter,
                            $"[Admin] {player.Name} has muted all players within a {radius}m radius for {durationInMinutes} minutes. Reason: {reason}.", 5000);
                    }
                }

                // Notify the admin that the mute process is complete
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter,
                    $"All players within a {radius}m radius have been muted for {durationInMinutes} minutes. Reason: {reason}.", 5000);
            }
            catch (Exception e)
            {
                _logger.WriteError("EXCEPTION AT \"CMD_muteall\":\n" + e.ToString());
            }
        }







        [Command("unfollow")]
        public static void CMD_unfollow(ExtPlayer player)
        {
            try
            {
                FractionCommands.targetUnFollowPlayer(player);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("c")]
        public static void CMD_getCoords(ExtPlayer player)
        {
            try
            {
                if (!Group.CanUseAdminCommand(player, "a")) return;
                Chat.SendTo(player, $"Position: {NAPI.Entity.GetEntityPosition(player)}");
                Chat.SendTo(player, $"Rotation: {NAPI.Entity.GetEntityRotation(player)}");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("pull")]
        public static void CMD_pullOut(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Ein Spieler mit einer solchen ID wurde nicht gefunden", 3000);
                    return;
                }
                FractionCommands.playerOutCar(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("accept")]
        public static void CMD_accept(ExtPlayer player, int id)
        {
            try
            {
                if (Trigger.GetPlayerByUuid(id) == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"A player with such an ID was not found", 3000);
                    return;
                }
                Fractions.FractionCommands.acceptEMScall(player, Trigger.GetPlayerByUuid(id));
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }


        [Command("capture")]
        public static void CMD_capture(ExtPlayer player)
        {
            try
            {
                Fractions.GangsCapture.CMD_startCapture(player);
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [Command("carpass")]
        public static void CMD_CarPass(ExtPlayer player)
        {
            try
            {
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (vehicle == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You must be in the car", 3000);
                    return;
                };
                VehicleManager.ViewVehicleTechnicalCertificate(player, vehicle);
                Chat.Action(player, "veh:carpass:check");
            }
            catch (Exception e) { _logger.WriteError("EXCEPTION AT \"CMD\":\n" + e.ToString()); }
        }

        [RemoteEvent("tpmark")]
        [Command("tpmark")]
        public static void CMD_tpmark(ExtPlayer player){
            try
            {
                if (!Group.CanUseAdminCommand(player, "tpmark")) return;
                SafeTrigger.ClientEvent(player, "GetMyWaypoint");
            }
            catch (Exception e)
            {
                _logger.WriteError($"CMD_tpmark: {e}");
            }
        }

        [Command("changegtype")]
        public static void CMD_changeGarageType(ExtPlayer player, int houseID, int newGarageType) {
            try {
                if (!Group.CanUseAdminCommand(player, "changegtype")) 
                    return;                
                if (newGarageType < 0 || newGarageType > 14) {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Incorrect type of garage!", 3000);
                    return;
                }
                DataTable result = MySQL.QueryRead("SELECT `garage` FROM `houses` WHERE `id` = @prop0", houseID);
                if (result == null || result.Rows.Count == 0) {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"House under the number {houseID} It does not exist!", 3000);
                    return;
                }
                var garageID = Convert.ToInt32(result.Rows[0]["garage"]);
                MySQL.Query("UPDATE `garages` SET `type` = @prop0 WHERE `id` = @prop1", newGarageType, garageID);
                
                var garage =  GarageManager.Garages[garageID];
                garage.Type = newGarageType;
                var house = HouseManager.Houses.Where(x => x.ID == houseID).FirstOrDefault();
                _logger.WriteInfo($"Updated type of garage {garageID}.");
                NAPI.Task.Run(() =>
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Type of garage in the house {houseID}Successfully modified!", 3000);
                });
            } catch (Exception e)
            {
                _logger.WriteError($"CMD_changeGarageType: {e}");
            }
        }
    }
}