using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Newtonsoft.Json;
using Whistler.Helpers;
using Whistler.Houses;
using Whistler.Houses.Furnitures;
using Whistler.MoneySystem;
using Whistler.SDK;
using Whistler.GUI;
using Whistler.Businesses.Models;
using Whistler.Entities;

namespace Whistler.Core
{
    partial class BusinessManager
    {
        private static void OnFurnitureStoreInteractionPressed(ExtPlayer player, Business businessInstance)
        {
            if (businessInstance.Type != 27) return;
            SafeTrigger.ClientEvent(player,"furnitureStore:open", businessInstance.GetPriceByProductName("Parts").CurrentPrice);
        }

        [RemoteEvent("furnitureStore:playerBought")]
        public static void OnPlayerBoughtFurniture(ExtPlayer player, string data)
        {
            var boughtItems = JsonConvert.DeserializeObject<List<FurnitureStoreItemDTO>>(data);
            DialogUI.Open(player, "In which house you want to buy furniture:", new List<DialogUI.ButtonSetting>
                {
                    new DialogUI.ButtonSetting
                    {
                        Name = "Private",
                        Icon = null,
                        Action = (p) => BuyFurnitures(player, HouseManager.GetHouse(player, true), boughtItems),
                    },

                    new DialogUI.ButtonSetting
                    {
                        Name = "Family",
                        Icon = null,
                        Action = (p) => BuyFurnitures(player, player.GetFamily()?.GetHouse(), boughtItems),
                    },

                    new DialogUI.ButtonSetting
                    {
                        Name = "cancellation",
                        Icon = null,
                        Action = (p) => { },
                    }
                });
        }
        internal static void BuyFurnitures(ExtPlayer player, House house, List<FurnitureStoreItemDTO> boughtItems)
        {
            try
            {
                var businessId = player.GetData<int>("BIZ_ID");
                var business = BusinessManager.GetBusiness(businessId);
                if (business == null)
                    return;
                if (house == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You have no home", 3000);
                    return;
                }
                if (!house.GetAccessFurniture(player, Families.FamilyFurnitureAccess.ManagementFurniture))
                {                    
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You have no access", 3000);
                    return;
                }

                List<BuyModel> buyModels = new List<BuyModel>();
                foreach (var item in boughtItems)
                {
                    if (!FurnitureSettings.AllAvailableFurnitures.TryGetValue(item.Key, out var setting)) continue;
                    buyModels.Add(new BuyModel("Parts", setting.Cost * item.Count, false, 
                        (cnt) =>
                        {
                            int count = cnt / setting.Cost;
                            for (var i = 0; i < count; i++)
                                FurnitureService.AddFurniture(new Furniture(item.Key), house);
                            return cnt;
                        }));
                }

                if (BusinessManager.TakeProd(player, business, player.Character, buyModels, "Buy furniture", null))
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You bought furniture", 3000);
                    player.SendTip("You can install the purchased furniture in the property management menu");
                }
            }
            catch (Exception ex) { _logger.WriteError("FurnituresStore: " + ex); }
        }
    }


    internal class FurnitureStoreItemDTO
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}