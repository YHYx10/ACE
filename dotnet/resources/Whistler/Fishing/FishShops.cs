using GTANetworkAPI;
using Whistler.Core;
using Whistler.Fishing.Extensions;
using Whistler.Fishing.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Whistler.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whistler.Inventory;
using Whistler.MoneySystem;
using System.IO;
using Whistler.Core.QuestPeds;
using Whistler.Helpers;
using Whistler.Entities;

namespace Whistler.Fishing
{
    class FishShops
    {
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(FishShops));
        private const float _priceCoef = 1.0f;

        public FishShops(List<Trader> traders) 
        {
            if (Directory.Exists("interfaces/gui/src/configs/fishing"))
            {
                ParseConfig();
            }
            foreach (var trader in traders)
            {
                var blip = NAPI.Blip.CreateBlip(trader.BlipSprite, trader.Position, 1, trader.BlipColor, trader.BlipName, 255, 0, true);
                var ped = new QuestPed(trader.Hash, trader.Position, trader.Name, trader.Role, trader.Rotation.Z, 0, 2);
                ped.PlayerInteracted += OpenShop;
            }
        }

        protected List<ShopItem> _shopItems { get; } = new List<ShopItem>
        {
            new ShopItem(Fish.Herring, "Herring", 0, (int)(60 * _priceCoef)),
            new ShopItem(Fish.Bass, "Perch", 0, (int)(70 * _priceCoef)),
            new ShopItem(Fish.Eel, "Acne", 0, (int)(85 * _priceCoef)),
            new ShopItem(Fish.Pike, "Pike", 0, (int)(100 * _priceCoef)),
            new ShopItem(Fish.Sterlet, "Sterlites", 0, (int)(110 * _priceCoef)),
            new ShopItem(Fish.Salmon, "Salmon", 0, (int)(125 * _priceCoef)),
            new ShopItem(Fish.Sturgeon, "Oset", 0, (int)(135 * _priceCoef)),
            new ShopItem(Fish.Amur, "Amur", 0, (int)(150 * _priceCoef)),
            new ShopItem(Fish.Stingray, "Scat", 0, (int)(160 * _priceCoef)),
            new ShopItem(Fish.Tuna, "Tuna", 0, (int)(175 * _priceCoef)),
            new ShopItem(Fish.Trout, "Trout", 0, (int)(185 * _priceCoef)),

            new ShopItem(Fish.PerfectHerring, "Elite herring", 0, (int)(200 * _priceCoef)),
            new ShopItem(Fish.PerfectBass, "The perch is elite", 0,(int)(210 * _priceCoef)),
            new ShopItem(Fish.PerfectEel, "Elite eel", 0, (int)(225 * _priceCoef)),
            new ShopItem(Fish.PerfectPike, "Elite pike", 0, (int)(235 * _priceCoef)),
            new ShopItem(Fish.PerfectSterlet, "The sterlet is elite", 0, (int)(250 * _priceCoef)),
            new ShopItem(Fish.PerfectSalmon, "Elite salmon ", 0, (int)(260 * _priceCoef)),
            new ShopItem(Fish.PerfectSturgeon, "The sturgeon is elite", 0, (int)(270 * _priceCoef)),
            new ShopItem(Fish.PerfectAmur, "Amur elite ", 0, (int)(285 * _priceCoef)),
            new ShopItem(Fish.PerfectStingray, "The slope is elite", 0, (int)(300 * _priceCoef)),
            new ShopItem(Fish.PerfectTuna, "Elite tuna", 0, (int)(310 * _priceCoef)),
            new ShopItem(Fish.PerfectTrout, "The trout is elite", 0, (int)(325 * _priceCoef)),

            new ShopItem(Fish.GoldFish, "Goldfish", 0, (int)(350 * _priceCoef)),
        };     

        private void OpenShop(ExtPlayer player, QuestPed ped)
        {
            try
            {
                DialogPage startPage;
                var inventory = player.GetInventory();
                var cage = inventory.Items.GetNotFreeCage();
                if (cage == null || !cage.Fishings.Any())
                {
                    startPage = new DialogPage("Friends, I have a lot of things..", ped.Name, ped.Role)
                           .AddCloseAnswer("I understand.I will be back with fish soon.");
                }
                else
                {
                    startPage = new DialogPage("Show what is trapped, I am ready to buy your whole catch.", ped.Name, ped.Role)
                        .AddAnswer("I want to sell you a little fish", p => SafeTrigger.ClientEvent(p, Const.CLIENT_EVENT_SHOW_FISH_SHOP, cage.Fishings))
                        .AddCloseAnswer("I have changed my opinion, I'll somehow look later ");
                }              
                startPage.OpenForPlayer(player);
            }
            catch (Exception e) { _logger.WriteError("Open: " + e.ToString()); }
        }

        private void ParseConfig()
        {
            using (var w = new StreamWriter("interfaces/gui/src/configs/fishing/fishShop.js"))
            {
                w.Write($"export default {JsonConvert.SerializeObject(_shopItems, Formatting.Indented)}");
            }
        }

        internal string GetFishName(int fish)
        {
            return _shopItems[fish].Name;
        }

        internal int GetFishPrice(int key)
        {
            return _shopItems.First(i => i.Id == key).Price;
        }

        internal void CellFish(ExtPlayer client, int key)
        {
            try
            {
                var count = 0;
                var inventory = client.GetInventory();
                var cage = inventory.Items.GetCage();
                if (cage == null)
                {
                    Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "The bucket was not found ", 3000);
                    return;
                }
                var price = 0;
                if (key < 0)
                {
                    foreach (var item in cage.Fishings)
                    {
                        price += (GetFishPrice(item.Key) * item.Value);
                        count += item.Value;
                    }
                    cage.Fishings.Clear();
                }
                else
                {
                    if (!cage.Fishings.ContainsKey(key))
                    {
                        Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Such a fish was not found", 3000);
                        return;
                    }
                    count = cage.Fishings[key];
                    price = GetFishPrice(key) * count;
                    cage.Fishings.Remove(key);
                }
                if (price > 0)
                {
                    inventory.MarkAsChanged();
                    inventory.UpdateItemData(cage.Index);
                    SafeTrigger.ClientEvent(client, Const.CLIENT_EVENT_UPDATE_CAGE, cage.Fishings);

                    Wallet.MoneyAdd(client.Character, price, "Sell ​​fish");
                    Notify.Send(client, NotifyType.Success, NotifyPosition.BottomCenter, $"You sold {count}Kilogram fish for: {price}$", 3000);
                }
            }
            catch (Exception e) { _logger.WriteError("CellFish: " + e.ToString()); }
        }
    }
}
