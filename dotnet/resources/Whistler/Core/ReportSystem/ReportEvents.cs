using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whistler.Core.ReportSystem.Models;
using Whistler.Entities;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Core.ReportSystem
{
    class ReportEvents : Script
    {
        //Админ взял репорт на себя
        [RemoteEvent("report:takereport")]
        public void RemoteEvent_TakeReport(ExtPlayer player, int id)
        {
            if (!player.IsLogged())
                return;
            if (!ReportManager.Reports.ContainsKey(id))
                return;
            var character = player.Character;
            Report report = ReportManager.Reports[id];
            if (report.AdminName != null)
                return;
            ReportManager.SetAdminToReport(character, report);
        }

        [RemoteEvent("report:sendmessage")]
        public void RemoteEvent_SendMessage(ExtPlayer player, int id, string answer)
        {
            ReportManager.SendMessage(player, id, answer);
        }

        [RemoteEvent("report:kick")]
        public void RemoteEvent_Kick(ExtPlayer player, int id, string reason)
        {
            if (!ReportManager.Reports.ContainsKey(id))
                return;
            Report report = ReportManager.Reports[id];
            ExtPlayer target = Trigger.GetPlayerByUuid(report.AuthorUUID);

            Admin.kickPlayer(player, target, reason, false);
        }

        [RemoteEvent("report:sendclosereport")]
        public void RemoteEvent_CloseReport(ExtPlayer player, int ID, int rating)
        {
            var character = player.Character;
            Report report = ReportManager.Reports[ID];
            var playerAuthor = Trigger.GetPlayerByUuid(report.AuthorUUID);
            if (playerAuthor != null)
            {
                var characterAuthor = playerAuthor.Character;
                characterAuthor.NumberOfRatings++;
                characterAuthor.AmountOfRatings += rating;
                characterAuthor.OpenedReport = -1;
                Chat.SendTo(playerAuthor, $"{player.Name} Completion your calling #{report.ID}. You can evaluate the administrator's work in the report in the menu on a 5-not-present scale");
            }
            else
            {
                MySQL.Query("UPDATE `character` SET `numberofratings` = `numberofratings` + 1, `amountofratings` = `amountofratings` + @prop0 WHERE `uuid` = @prop1;", rating, report.AuthorUUID);
            }
            ReportManager.CloseReport(character, report);
        }

        [RemoteEvent("report:presshotkey")]
        public void RemoteEvent_PressHotKey(ExtPlayer player, int id, int key)
        {
            if (!player.IsLogged())
                return;
            if (!ReportManager.Reports.ContainsKey(id))
                return;
            Report report = ReportManager.Reports[id];
            ExtPlayer target = Trigger.GetPlayerByUuid(report.AuthorUUID);
            if (target == null)
                return;
            switch (key)
            {
                case 0:
                    Admin.teleportToPlayer(player, target);
                    SafeTrigger.ClientEvent(player,"report:closemenu");
                    break;
                case 1:
                    AdminSP.Spectate(player, target);
                    SafeTrigger.ClientEvent(player,"report:closemenu");
                    break;
                case 3:
                    if (report.StateGet > 0)
                        return;
                    target.GetStats().ForEach(item => ReportManager.SendMessage(player, id, item, false));
                    report.StateGet++;
                    break;
                default:
                    break;
            }
        }

        [RemoteEvent("report:position")]
        public void SebbdReportPosition(ExtPlayer player, int id, float x, float y)
        {
            if (!player.IsLogged())
                return;
            if (!ReportManager.Reports.ContainsKey(id))
                return;
            Report report = ReportManager.Reports[id];
            ExtPlayer target = Trigger.GetPlayerByUuid(report.AuthorUUID);
            if (target == null)
                return;
            SafeTrigger.ClientEvent(target, "createWaypoint", x, y);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You handed over {target.Name} Data on your route!", 3000);
        }

        [RemoteEvent("report:player:send")]
        [Command("report", GreedyArg = true)]
        public static void PlayerSendReport(ExtPlayer player, string message)
        {
            ReportManager.SendReport(player, message);
        }

        [RemoteEvent("report:player:close")]
        public static void PlayerCloseReport(ExtPlayer player)
        {
            
            if (!player.IsLogged() || !ReportManager.Reports.ContainsKey(player.Character.OpenedReport))
                return;
            ReportManager.CloseReport(null, ReportManager.Reports[player.Character.OpenedReport]);
        }

        [RemoteEvent("report:player:raiting")]
        public static void PlayerReportRating(ExtPlayer player, int rating)
        {   
            if (!player.IsLogged())
                return;
            if (rating < 1)
                rating = 1;
            if (rating > 5)
                rating = 5;
            ReportManager.SetRatingLastReportFromPlayer(player, rating);
        }

        
        // === ASTRO AdminPanel Remote Events (Player Support / Tickets) ===

        [RemoteEvent("admin:tickets:request")]
        public void AdminTicketsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestTickets(player);
        }

        [RemoteEvent("admin:ticket:claim")]
        public void AdminTicketClaim(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                ReportManager.AdminPanel_ClaimTicket(player, id);
            }
            catch { }
        }

        [RemoteEvent("admin:ticket:close")]
        public void AdminTicketClose(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                ReportManager.AdminPanel_CloseTicket(player, id);
            }
            catch { }
        }

        [RemoteEvent("admin:ticket:note")]
        public void AdminTicketNote(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                string note = obj.Value<string>("note") ?? "";
                ReportManager.AdminPanel_SetNote(player, id, note);
            }
            catch { }
        }

        [RemoteEvent("admin:ticket:message")]
        public void AdminTicketMessage(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                string text = obj.Value<string>("text") ?? string.Empty;
                ReportManager.AdminPanel_SendMessage(player, id, text);
            }
            catch { }
        }

        [RemoteEvent("admin:ticket:teleport")]
        public void AdminTicketTeleport(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                ReportManager.AdminPanel_TeleportToPlayer(player, id);
            }
            catch { }
        }

        [RemoteEvent("admin:ticket:spectate")]
        public void AdminTicketSpectate(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int id = obj.Value<int>("id");
                ReportManager.AdminPanel_SpectatePlayer(player, id);
            }
            catch { }
        }

        [RemoteEvent("admin:penalties:search")]
        public void AdminPenaltiesSearch(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                string query = obj.Value<string>("query") ?? string.Empty;
                ReportManager.AdminPanel_SearchPenalties(player, query);
            }
            catch { }
        }

        [RemoteEvent("admin:orgs:request")]
        public void AdminOrganizationsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestOrganizations(player);
        }

        [RemoteEvent("admin:org:tp")]
        public void AdminOrganizationTeleport(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int playerId = obj.Value<int>("playerId");
                ReportManager.AdminPanel_OrganizationTeleport(player, playerId);
            }
            catch { }
        }

        [RemoteEvent("admin:org:stats")]
        public void AdminOrganizationStats(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int playerId = obj.Value<int>("playerId");
                ReportManager.AdminPanel_OrganizationPlayerStats(player, playerId);
            }
            catch { }
        }

        [RemoteEvent("admin:biz:request")]
        public void AdminBusinessesRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestBusinesses(player);
        }

        [RemoteEvent("admin:biz:tp")]
        public void AdminBusinessTeleport(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int businessId = obj.Value<int>("businessId");
                ReportManager.AdminPanel_BusinessTeleport(player, businessId);
            }
            catch { }
        }

        [RemoteEvent("admin:commands:request")]
        public void AdminCommandsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestCommands(player);
        }

        [RemoteEvent("admin:administrators:request")]
        public void AdminAdministratorsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestAdministrators(player);
        }

        [RemoteEvent("admin:administrator:rank")]
        public void AdminAdministratorRank(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int playerId = obj.Value<int>("playerId");
                int rank = obj.Value<int>("rank");
                ReportManager.AdminPanel_SetAdministratorRank(player, playerId, rank);
            }
            catch { }
        }

        [RemoteEvent("admin:teleports:request")]
        public void AdminTeleportsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestTeleports(player);
        }

        [RemoteEvent("admin:teleport:go")]
        public void AdminTeleportGo(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                float x = obj.Value<float>("x");
                float y = obj.Value<float>("y");
                float z = obj.Value<float>("z");
                uint dimension = (uint)(obj.Value<int?>("dimension") ?? 0);
                ReportManager.AdminPanel_GoToTeleport(player, x, y, z, dimension);
            }
            catch { }
        }

        [RemoteEvent("admin:families:request")]
        public void AdminFamiliesRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestFamilies(player);
        }

        [RemoteEvent("admin:family:join")]
        public void AdminFamilyJoin(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int familyId = obj.Value<int>("familyId");
                ReportManager.AdminPanel_JoinFamily(player, familyId);
            }
            catch { }
        }

        [RemoteEvent("admin:family:tp-house")]
        public void AdminFamilyTpHouse(ExtPlayer player, string json)
        {
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                int familyId = obj.Value<int>("familyId");
                ReportManager.AdminPanel_FamilyTeleportHouse(player, familyId);
            }
            catch { }
        }

        [RemoteEvent("admin:event:create")]
        public void AdminEventCreate(ExtPlayer player, string json)
        {
            try
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminEventDTO>(json ?? "{}");
                ReportManager.AdminPanel_CreateEvent(player, model);
            }
            catch { }
        }

        [RemoteEvent("admin:events:request")]
        public void AdminEventsRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestEvents(player);
        }

        [RemoteEvent("admin:murders:request")]
        public void AdminMurdersRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestMurders(player);
        }

        [RemoteEvent("admin:arena:request")]
        public void AdminArenaRequest(ExtPlayer player)
        {
            ReportManager.AdminPanel_RequestArena(player);
        }

        [RemoteEvent("transfer:success")]
        public static void TransferSuccess(ExtPlayer player, int id)
        {   
            if ((player.Character?.AdminLVL ?? 0) < ReportConfigs.adminLvLMoneyTransfer) return;
            ReportManager.SuccessTransfer(id, player.Name);
        }

        [RemoteEvent("transfer:cancel")]
        public static void TransferCancel(ExtPlayer player, int id)
        {
            if ((player.Character?.AdminLVL ?? 0) < ReportConfigs.adminLvLMoneyTransfer) return;
            ReportManager.CancelTransfer(id, player.Name);

        }
    }
}
