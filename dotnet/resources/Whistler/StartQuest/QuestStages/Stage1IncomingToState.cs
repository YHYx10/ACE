using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Whistler.Core;
using Whistler.Core.QuestPeds;
using Whistler.Entities;
using Whistler.Fractions.Models;
using Whistler.GUI;
using Whistler.Helpers;

namespace Whistler.StartQuest.QuestStages
{
    class Stage1IncomingToState : BaseStage
    {
        public Stage1IncomingToState()
        {
            StageName = StartQuestNames.Stage1IncomingToState;
            var ped = new QuestPed(StartQuestSettings.PedRailwayStation);
            ped.PlayerInteracted += PedStartRailwayStation_PlayerInteracted;
        }

        private void PedStartRailwayStation_PlayerInteracted(ExtPlayer player, QuestPed ped)
        {
            var level = player.Character.QuestStage;
            DialogPage startPage;
            switch (level)
            {
                case StartQuestNames.Stage1IncomingToState:
                    var giveLicAndTask = new DialogPage("Hold a few more dollars, you will need you.Good luck!",
                        ped.Name,
                        ped.Role)
                        .AddAnswer("startQuest_10", PedStartRailwayStation_CallBack);

                    var startPage3 = new DialogPage("Then they are okay, first they have to earn money to restore the rights.I'll give you a little bit of your leg.This should be enough to rent a scooter.A person from whom you can rent a transport is on the back of this building.",
                        ped.Name,
                        ped.Role)
                        .AddAnswer("Well, thanks, Joe.", giveLicAndTask);
                    
                    var startPage2 = new DialogPage("Yes ... you need help?",
                        ped.Name,
                        ped.Role)
                        .AddAnswer("I feel better, but all the money and documents were stolen to me.", startPage3);
                    
                    var startPage1 = new DialogPage("What happened to you?",
                        ped.Name,
                        ped.Role)
                        .AddAnswer("The last thing I sailed on the liner and how they started to rob myself into my cabin and then hit my head with a racket.", startPage2);
                    
                    
                    startPage = new DialogPage("Hello, how are you?",
                        ped.Name,
                        ped.Role)
                        .AddAnswer("For long time we haven't seen each other.Hello Joe.", startPage1);
                    break;
                case StartQuestNames.Stage2GetRentVehicle:
                    startPage = new DialogPage("startQuest_11",
                        ped.Name,
                        ped.Role)
                        .AddCloseAnswer("startQuest_10");
                    break;
                default:
                    startPage = new DialogPage("startQuest_12",
                        ped.Name,
                        ped.Role)
                        .AddCloseAnswer("startQuest_13");
                    break;
            }
            startPage.OpenForPlayer(player);
        }
        private void PedStartRailwayStation_CallBack(ExtPlayer player)
        {
            MoneySystem.Wallet.MoneyAdd(player.Character, 300, "Help Joe");
            QuestFinish(player);
        }
        protected override void StartStage(ExtPlayer player)
        {
            QuestInformation.Show(player, "Talk to Joe "," Find Joe and talk to him.");
            player.CreateClientBlip(778, 1, "Joe", StartQuestSettings.PedRailwayStation.Position, 1, 46, NAPI.GlobalDimension);
            player.CreateWaypoint(StartQuestSettings.PedRailwayStation.Position);
            player.CreateClientMarker(778, 0, StartQuestSettings.PedRailwayStation.Position + new Vector3(0, 0, 2), 1, NAPI.GlobalDimension, new Color(50, 200, 100), new Vector3());
            player.SendTip("tip_6");
            player.SendTip("tip_5");
        }
        protected override void StopStage(ExtPlayer player)
        {
            QuestInformation.Hide(player);
            player.DeleteClientBlip(778);
            player.DeleteClientBlip(778);
            player.DeleteClientMarker(778);
        }
        protected override void FinishStage(ExtPlayer player)
        {
            QuestInformation.Hide(player);
            player.DeleteClientBlip(778);
            player.DeleteClientBlip(778);
            player.DeleteClientMarker(778);
        }
    }
}