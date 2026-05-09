using GTANetworkAPI;
using System;
using Whistler.MoneySystem;
using Whistler.SDK;
using Whistler.VehicleSystem;
using Whistler.Helpers;
using Whistler.VehicleSystem.Models;
using Whistler.Fractions;
using Whistler.Domain.Phone.Bank;
using Whistler.MoneySystem.Interface;
using Whistler.Businesses.Models;
using Whistler.Common;
using Whistler.Entities;

namespace Whistler.Core
{
    partial class BusinessManager : Script
    {
        public enum PetrolType
        {
            Invalid = 0,
            Standart,
            StandartPlus,
            Diesel,
            Deluxe,
            Electro
        };

        [RemoteEvent("gasStation:buypetrol")]
        public static void FillCar(ExtPlayer player, int fuelType, int liters, int paymentType)
        {
            try
            {
                if (!player.IsLogged() || player.Vehicle == null) return;
                ExtVehicle vehicle = player.Vehicle as ExtVehicle;
                if (player.VehicleSeat != VehicleConstants.DriverSeat) return;
                if (!Enum.IsDefined(typeof(PaymentsType), paymentType)) return;
                var payType = (PaymentsType)paymentType;
                if (liters <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The wrong data was entered", 3000);
                    return;
                }
                VehicleStreaming.SetEngineState(vehicle, false);
                
                if (VehicleStreaming.GetEngineState(vehicle))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter,"To start refueling - drown out the transport", 3000);
                    return;
                }

                var config = VehicleConfiguration.GetConfig(vehicle.Model);
                if (vehicle.Data.Fuel >= config.MaxFuel)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Transport has a full tank", 3000);
                    return;
                }
                // Chat.AdminToAll($"fueltype: {fuelType} / fuel: {config.fuelType}-1");
                if (fuelType != config.fuelType)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Transport has a different kind of fuel", 3000);
                    return;
                } 

                int tfuel = vehicle.Data.Fuel + liters;
                if (tfuel > config.MaxFuel)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Enter the correct data (beforehand {config.MaxFuel})", 3000);
                    return;
                }
                Business biz = GetBusiness(player.GetData<int>("BIZ_ID"));
                int fuelProduct = fuelType - 1;
                if (fuelProduct >= biz.Products.Count)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"This does not sell this type of fuel.", 3000);
                    return;
                }

                int bizPricePerLiter = biz.Products[fuelProduct].Price;
                if (payType == PaymentsType.Gov)
                {
                    var fraction = Manager.GetFraction(player);
                    if (fraction == null || fraction.OrgActiveType != OrgActivityType.Government)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "To recharge your transport for state.Account, you have to be in the state.Organizations", 3000);
                        return;
                    }
                    if (vehicle.Data.OwnerType != OwnerType.Fraction || vehicle.Data.OwnerID != fraction.Id)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You cannot recharge your public transport on the public report.", 3000);
                        return;
                    }
                    if (fraction.FuelLeft < liters * bizPricePerLiter)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Border for tanking state. Transport for one day is exhausted", 3000);
                        return;
                    }
                }
                string petrolName = Enum.GetName(typeof(PetrolType), fuelType);
                if (string.IsNullOrEmpty(petrolName))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Fuel determination errors, contact the administration.", 3000);
                    return;
                }
                TakeProd(player, biz, player.GetMoneyPayment(payType, Manager.GetFraction(6)), 
                    new BuyModel(petrolName, liters, false,
                    (cnt) =>
                    {
                        VehicleStreaming.SetVehicleFuel(vehicle, vehicle.Data.Fuel + cnt);
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Transport tanken", 3000);
                        Chat.Action(player, "Vehicle refueled");
                        if (payType == PaymentsType.Gov)
                        {
                            Manager.GetFraction(player).FuelLeft -= liters * bizPricePerLiter;
                        }
                        biz.UpdateBlip();
                        return cnt;
                    }),
                    $"Buying fuel {petrolName}", null);
            }
            catch (Exception e) { _logger.WriteError("Petrol: " + e.ToString()); }
        }

        public static void OpenPetrolMenu(ExtPlayer player)
        {
            Business biz = BizList[player.GetData<int>("BIZ_ID")];
            SafeTrigger.ClientEvent(player, "openPetrol", biz.Products[0].Price, biz.Products[1].Price, biz.Products[2].Price, biz.Products[3].Price, biz.Products[4].Price);
        }
    }
}
