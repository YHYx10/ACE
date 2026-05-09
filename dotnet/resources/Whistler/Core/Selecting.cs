using GTANetworkAPI;
using System;
using Whistler.GUI;
using Whistler.Houses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Whistler.SDK;
using Whistler.Families;
using Whistler.Helpers;
using Whistler.VehicleSystem;
using Whistler.VehicleSystem.Models;
using Whistler.Inventory;
using Whistler.Inventory.Enums;
using Whistler.MoneySystem;
using Whistler.Families.Models;
using Whistler.Inventory.Models;
using Whistler.Entities;

namespace Whistler.Core
{
    class Selecting : Script
    {
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(Selecting));

        public static Action<ExtPlayer, GTANetworkAPI.Object> ObjectInteract;

        [RemoteEvent("objectInteracted")]
        public static void objectSelected(ExtPlayer player, GTANetworkAPI.Object entity)
        {
            try
            {
                if (entity == null || player == null || !player.IsLogged()) return;
                if (player.Character.DemorganTime > 0)
                    return;
                ObjectInteract?.Invoke(player, entity);
            }
            catch (Exception e) { _logger.WriteError($"oSelected/: {e.ToString()}\n{e.StackTrace}"); }
        }

        [Command("checkbone")]
        public static void Command_CheckBone(ExtPlayer player, int vehid, string bone)
        {
            var vehicle = SafeTrigger.GetVehicleById(vehid);
            if (vehicle != null)
                SafeTrigger.ClientEvent(player,"checkBone", vehicle, bone);
        }

        public static void playerTransferMoney(ExtPlayer player, string arg)
        {
            try
            {
                Convert.ToInt32(arg);
            }
            catch
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The wrong data was entered", 3000);
                return;
            }
            var amount = Convert.ToInt32(arg);
            if (amount < 1)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The wrong data was entered", 3000);
                return;
            }

            if (amount > 100000)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Maximum amount for transmission $ 100,000", 3000);
                return;
            }

            ExtPlayer target = player.GetData<ExtPlayer>("SELECTEDPLAYER");
            if (!target.IsLogged() || player.Position.DistanceTo(target.Position) > 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is too far away from you", 3000);
                return;
            }
            if (amount > player.Character.Money)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Not enough money", 3000);
                return;
            }
            if (player.HasData("NEXT_TRANSFERM") && DateTime.Now < player.GetData<DateTime>("NEXT_TRANSFERM") && player.Character.AdminLVL == 0)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It has been a while since the last transfer of money.", 3000);
                return;
            }
            SafeTrigger.SetData(player, "NEXT_TRANSFERM", DateTime.Now.AddMinutes(3));
            Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"{target.isFriend(player)} gab {amount}$", 3000);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You handed over {amount}$ {player.isFriend(target)}", 3000);

            Wallet.TransferMoney(player.Character, target.Character, amount, 0, "Transmission of money");
            Chat.Action(player, $"hand over ${amount} to {player.isFriend(target)}");
        }

        public static void PlayerTakeGuns(ExtPlayer player, ExtPlayer target)
        {
            if (player.Position.DistanceTo(target.Position) > 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is far away", 3000);
                return;
            }
            if (!Fractions.Manager.CanUseCommand(player, "takeguns")) return;
            if (!target.Character.Cuffed)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is not in handcuffs", 3000);                
                return;
            }
            Weapons.RemoveAll(target, true);
            Notify.Send(target, NotifyType.Warning, NotifyPosition.BottomCenter, $"{target.isFriend(player)}took all your weapons with them", 3000);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have confiscated all weapons {player.isFriend(target)}", 3000);
            return;
        }
        public static void PlayerTakeIlleagal(ExtPlayer player, ExtPlayer target)
        {
            if (player.Position.DistanceTo(target.Position) > 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is far away", 3000);
                return;
            }
            if (target.GetInventory().RemoveItems(item => item.Type == ItemTypes.Narcotic))
            {
                Notify.Send(target, NotifyType.Warning, NotifyPosition.BottomCenter, $"{target.isFriend(player)}has confiscated illegal items from themt", 3000);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have taken illegal items from{player.isFriend(target)}", 3000);
            }
            else
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player has nothing illegal", 3000);
        }

        public static void playerHandshakeTarget(ExtPlayer player, ExtPlayer target)
        {
            if (!isPlayerCanHandshake(player, target))
            {
                return;
            }
            SafeTrigger.SetData(target, "HANDSHAKER", player);
            target.OpenDialog("HANDSHAKE", $"{target.isFriend(player)}wants to shake your hand.");
            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"She proposed {player.isFriend(target)} Shake your hand", 3000);
        }

        private static bool isPlayerCanHandshake(ExtPlayer player, ExtPlayer target)
        {
            var isInDeath = false;
            if (player.HasSharedData("InDeath"))
            {
                isInDeath = player.GetSharedData<bool>("InDeath");
            }
            if (player.Character.Cuffed || isInDeath)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It is impossible to shake your hand at the moment", 3000);
                return false;
            }

            isInDeath = false;
            if (target.HasSharedData("InDeath"))
            {
                isInDeath = target.GetSharedData<bool>("InDeath");
            }
            if (target.Character.Cuffed || isInDeath)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It is impossible to shake your hand at the moment", 3000);
                return false;
            }
            if (target.Position.DistanceTo(player.Position) >= 5)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "It is impossible to shake your hand at the moment", 3000);
                return false;
            }
            return true;
        }

        public static void hanshakeTarget(ExtPlayer player)
        {
            if (!player.HasData("HANDSHAKER")) return;
            ExtPlayer target = player.GetData<ExtPlayer>("HANDSHAKER");
            if (!target.IsLogged())
                return;
            if (!isPlayerCanHandshake(player, target))
            {
                return;
            }

            player.PlayAnimation("mp_ped_interaction", "handshake_guy_a", 39);
            target.PlayAnimation("mp_ped_interaction", "handshake_guy_a", 39);
           
            Main.OnAntiAnim(player);
            Main.OnAntiAnim(target);

            NAPI.Task.Run(() => 
            {
                Main.OffAntiAnim(player); 
                Main.OffAntiAnim(target); 
                player.StopAnimation(); 
                target.StopAnimation();
            }, 4500);

            if (player.Character.AddFriend(target.Name))
                SafeTrigger.ClientEvent(player, "addFriendToList", target.Name);

            if (target.Character.AddFriend(player.Name))
                SafeTrigger.ClientEvent(target, "addFriendToList", player.Name);
        }

        public static void OpenMoneyTransferMenu(ExtPlayer player, ExtPlayer target)
        {
            if (player.Character.LVL < 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The transmission of money is available according to level 2", 3000);
                return;
            }
            player.OpenInput("Transfer money", "Sum", 4, "player_givemoney");
        }

        public static void TakeMask(ExtPlayer player, ExtPlayer target)
        {
            if (!target.Character.Cuffed || (target.IsAdmin() && player.Character.AdminLVL <= target.Character.AdminLVL))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is not in handcuffs", 3000);
                return;
            }
            var mask = (ClothesBase)target.GetEquip().RemoveItem(target, ClothesSlots.Mask, LogAction.Move);
            if (mask.Drawable > 499 && mask.Drawable < 507) return;

            if (mask.Promo)
                target.GetInventory().AddItem(mask, log: LogAction.Move);
            Chat.Action(player, $"tore off the mask from one player {player.isFriend(target)}");
        }

        public static void Unarrest(ExtPlayer player, ExtPlayer target)
        {
            if (target.Character.Following == null)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Nobody pulls this player", 3000);
                return;
            }
            if (player.Character.Follower != target)
            {
                if (!target.Character.Following.IsLogged() || target.Character.Following.Position.DistanceTo(target.Position) > 100)
                {
                    target.UnFollow();
                    return;
                }
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Someone else pulls this player", 3000);
                return;
            }
            player.LetGoFollower(true);
        }

        public static void MakePenalty(ExtPlayer player, ExtPlayer target)
        {
            SafeTrigger.SetData(player, "TICKETTARGET", target);
            player.OpenInput("Write down a fine (sum)", "Amount from 0 to 7000 $", 4, "player_ticketsum");
        }
        public static void OfferSellMedKit(ExtPlayer player, ExtPlayer target)
        {
            SafeTrigger.SetData(player, "SELECTEDPLAYER", target);
            player.OpenInput("Sell and doctors", "Price $$$", 4, "player_medkit");
        }
        public static void OfferHealTarget(ExtPlayer player, ExtPlayer target)
        {
            SafeTrigger.SetData(player, "SELECTEDPLAYER", target);
            player.OpenInput("Offer treatment", "Price $$$", 4, "player_heal");
        }

        public static void InvitePlayerToFamily(ExtPlayer player, ExtPlayer target)
        {
            Family family = player.GetFamily();
            if (family == null) return;

            if (!target.CheckInviteToFamily(family))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is already in break or family", 3000);
                return;
            }
            if (!FamilyManager.CanAccessToMemberManagement(player)) return;

            DialogUI.Open(target, $"Citizen {target.isFriend(player)}invited them to join the organization{family.Name}.Accept the invitation?", new List<DialogUI.ButtonSetting>
            {
                new DialogUI.ButtonSetting
                {
                    Name = "Yes",// Да
                    Icon = "confirm",
                    Action = (p) => 
                    {
                        if (family == null) return;

                        int familyRank = family.Ranks.Count > 1 ? family.Ranks.Last().Key : 1;
                        FamilyManager.InvitePlayerToFamily(p, family, familyRank);
                    }
                },

                new DialogUI.ButtonSetting
                {
                    Name = "NO",// Нет
                    Icon = "cancel",
                    Action = p => {}
                }
            });
        }
        public static void GiveGunLicense(ExtPlayer player, ExtPlayer target)
        {
            SafeTrigger.SetData(player, "SELECTEDPLAYER", target);
            player.OpenInput("Sell ​​a weapon license ($ 45,000 to $ 50,000)", "Price $$$", 5, "player_givegunlic");
        }
        public static void ToPrison(ExtPlayer player, ExtPlayer target)
        {
            if (Fractions.PrisonFib.CanUsePrisonFib(player) && target != null)
            {
                var minutes = target.Character.WantedLVL.Level * (target.Character.IsPrimeActive() ? 5 : 10);
                //int time = target.GetData<int>("putprison");
                Fractions.PrisonFib.ToPrison(player, target, minutes);
            }
            else
                Chat.SendTo(player, "You cannot put any person to prison!");
        }

        public static void SellHouse(ExtPlayer player, int suggestedAmount)
        {
            if (!player.HasData("SELECTEDPLAYER")) return;
            
            var target = player.GetData<ExtPlayer>("SELECTEDPLAYER");
            DialogUI.Open(player, $"Do you really want to sell the player to the player? {target.Name} ({target.Character.UUID}) for {suggestedAmount}$?", new List<DialogUI.ButtonSetting>
            {
                new DialogUI.ButtonSetting
                {
                    Name = "Yes",// Да
                    Icon = "confirm",
                    Action = p => HouseManager.OfferHouseSell(player, target, suggestedAmount)
                },

                new DialogUI.ButtonSetting
                {
                    Name = "NO",// Нет
                    Icon = "cancel",
                    Action = p => {}
                }
            });
        }

        public static void SellFamilyHouse(ExtPlayer player, int suggestedAmount)
        {
            if (!player.HasData("SELECTEDPLAYER")) return;

            ExtPlayer target = player.GetData<ExtPlayer>("SELECTEDPLAYER");
            DialogUI.Open(player, $"Would you really like to sell a family home to players? {target.Name} ({target.Character.UUID}) for {suggestedAmount}$?", new List<DialogUI.ButtonSetting>
            {
                new DialogUI.ButtonSetting
                {
                    Name = "Yes",// Да
                    Icon = "confirm",
                    Action = p => HouseManager.OfferFamilyHouseSell(player, target, suggestedAmount)
                },

                new DialogUI.ButtonSetting
                {
                    Name = "NO",// Нет
                    Icon = "cancel",
                    Action = p => {}
                }
            });
        }

        public static void OpenCarStock(ExtPlayer player, ExtVehicle vehicle)
        {
            if (player.IsInVehicle)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You cannot open the inventory in the car", 3000);
                return;
            }
            if (vehicle.Class == 13 || vehicle.Class == 8)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This car has no trunk", 3000);
                return;
            }


            if (!vehicle.Data.CanAccessVehicle(player, AccessType.Inventory))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The trunk is closed", 3000);
                return;
            }
            InventoryService.OpenStock(player, vehicle.Data.InventoryId, StockTypes.VehicleTrunk);
        }
    }
}
