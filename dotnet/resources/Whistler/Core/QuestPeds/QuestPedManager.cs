using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Newtonsoft.Json;
using Whistler.Entities;
using Whistler.Fractions;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Core.QuestPeds
{
    internal class QuestPedManager : Script 
    {
        public static List<QuestPed> QuestPeds { get; } = new List<QuestPed>();
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(QuestPedManager));

        public QuestPedManager()
        {
            var questPed = new QuestPed(PedHash.FbiSuit01, new Vector3(1853, 2598, 45.67), "Karim_Denz", "");
            questPed.PlayerInteracted += (player, ped) =>
            {
                var descPage =
                    new DialogPage("Click the M button to display the entire list of work.", ped.Name, ped.Role)
                        .AddCloseAnswer();
                var workDescriptionPage = new DialogPage("Hello I am an employee of the town hall, on behalf of the entire state I would like to congratulate you on freedom!I hope you benefit in prison and you corrected your worldview.Our state loves its citizens, so I want, so I would like to offer you a job",
                        ped.Name, questPed.Role)
                    .AddAnswer("Of course I am ready to start life from scratch!", descPage)
                    .AddCloseAnswer("Thank you, I'm not interested");
                workDescriptionPage.OpenForPlayer(player);
            };
            
            var medicPed = new QuestPed(PedHash.Doctor01SMM, new Vector3(304.5436, -588.2626, 43.25), "Jock Cranley", "The chief doctor", interactionRange: 2, heading: 68);
            medicPed.PlayerInteracted += (player, ped) =>
            {
                var descPage = new DialogPage($"I will examine you, find the cause of your illness and remove it.It costs. {Ems.HealByBotPrice}$", ped.Name, ped.Role)
                    .AddAnswer("Check me, I feel bad", Ems.HealPlayerByPed)
                    .AddCloseAnswer("I understand, thank you, I'm healthy now");
                var introPage =
                    new DialogPage("Greetings!I am the main doctor in this state. If you have health problems, you are welcome to contact me! At the moment, are you in pain?", ped.Name, ped.Role)
                        .AddAnswer("Doctor, I'm sick!I really need your help", Ems.HealPlayerByPed)
                        .AddAnswer("How will you treat me?", descPage)
                        .AddCloseAnswer("Thank you, I'm healthy now");

                introPage.OpenForPlayer(player);
            };

            var vetPed = new QuestPed(PedHash.Paramedic01SMM, new Vector3(307.1508, -590.4382, 43.129753), "Deve Luxe", "Veterinarian", interactionRange: 2, heading: 140);
            vetPed.PlayerInteracted += (player, ped) =>
            {
                var introPage =
                    new DialogPage("Greetings!I am the head of the hospital's veterinarian.Do you have questions? ", ped.Name, ped.Role);

                Pets.Models.PetData petData = Pets.Controller.GetPet(player);
                if (petData != null)
                {
                    introPage.AddAnswer($"Doctor, my pet urgently needs treated ({Ems.HealPetByBotPrice}$)", Pets.Controller.Pet_Revive);
                    string priceText = petData.FreeRename ? "For free" : $"{Ems.RenamePetByBotPrice}$";
                    introPage.AddAnswer($"Could you help me choose the name of the pet? ({priceText})", Pets.Controller.Pet_Rename);
                    introPage.AddAnswer($"I want to buy a toy for my pet ({Ems.ToyPetByBotPrice}$)", Pets.Controller.Pet_BuyToy);
                }
                introPage.AddCloseAnswer("Thanks, I don't need any services yet");

                introPage.OpenForPlayer(player);
            };
        }
        
        [ServerEvent(Event.PlayerConnected)]
        public static void OnPlayerConnected(ExtPlayer player)
        {
            try
            {
                SafeTrigger.ClientEvent(player,"questPeds:load", JsonConvert.SerializeObject(QuestPeds));
            }
            catch (Exception e) { _logger.WriteError("QuestPeds: " + e.ToString()); }
        }
        
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(ExtPlayer player, DisconnectionType type, string reason)
        {
            try
            {
                if (DialogPage.OpenedPages.ContainsKey(player)) DialogPage.OpenedPages.Remove(player);
            }
            catch (Exception e) { _logger.WriteError("QuestPeds: " + e.ToString()); }
        }
        
        [RemoteEvent("dialogWindow:playerSelectedAnswer")]
        public static void OnPlayerSelectedAnswer(ExtPlayer player, int answerId)
        {
            try
            {
                if (DialogPage.OpenedPages.ContainsKey(player)) 
                    DialogPage.OpenedPages[player].OnPlayerSelectedAnswer(player, answerId);                
            }
            catch (Exception e) { _logger.WriteError("QuestPeds: " + e.ToString()); }
        }

        [RemoteEvent("dialogWindow:playerClosedDialog")]
        public static void OnPlayerClosedDialog(ExtPlayer player)
        {
            try
            {
                if (DialogPage.OpenedPages.ContainsKey(player))
                    DialogPage.OpenedPages[player].OnPlayerClosedDialog(player);
            }
            catch (Exception e) { _logger.WriteError("QuestPeds: " + e.ToString()); }
        }
    }
}