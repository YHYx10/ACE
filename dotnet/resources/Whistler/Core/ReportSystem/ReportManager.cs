using System;
using System.Data;
using GTANetworkAPI;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Whistler.SDK;
using System.Collections.Generic;
using Whistler.GUI;
using System.Linq;
using Newtonsoft.Json.Linq;
using Whistler.Core.ReportSystem.Models;
using Whistler.Helpers;
using Whistler.MoneySystem.Models;
using Whistler.Entities;
using System.Text.RegularExpressions;
using Whistler.Families;
using Whistler.Houses;
using Whistler.Common;
using Whistler.MP.Arena.Lobbies;
using Whistler.MP.Arena.Enums;
using Whistler.Core.Admins;

namespace Whistler.Core.ReportSystem
{
    class ReportManager : Script
    {
        public static Dictionary<int, Report> Reports = new Dictionary<int, Report>();
        public static Dictionary<int, MoneyTransfer> MoneyTransfers = new Dictionary<int, MoneyTransfer>();
        private static int _moneyTransfersID = 1;
        public static List<ExtPlayer> Admins = new List<ExtPlayer>();
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(ReportManager));

        public ReportManager()
        {
            Main.PlayerPreDisconnect += HandlePlayerDisconnect;
            Admin.SetPlayerToAdminGroup += OnAdminLoad;
            Admin.DeletePlayerFromAdminGroup += OnAdminUnload;
        }

        public static void HandlePlayerDisconnect(ExtPlayer player)
        {
            if (Admins.Contains(player))
                Admins.Remove(player);
            int openReport = player.Character.OpenedReport;
            if (openReport != -1 && Reports.ContainsKey(openReport))
            {
                CloseReport(null, Reports[player.Character.OpenedReport]);
            }
            if (player.Character.AdminLVL >= ReportConfigs.adminLvL)
                foreach (var report in Reports.Where(item => item.Value.AdminUUID == player.Character.UUID && !item.Value.Closed))
                    SetAdminToReport(null, report.Value);
        }

        public static void OnAdminLoad(ExtPlayer player)
        {
            if (!AdminAuthService.IsAuthenticated(player)) return;

            if (!Admins.Contains(player))
            {
                Admins.Add(player);
                if (player.Character.AdminLVL >= ReportConfigs.adminLvL)
                {
                    bool viewAllReports = Group.CanUseAdminCommand(player, "viewallreports", false);
                    player.TriggerEventWithLargeList("report:loadreports", Reports.Select(item => item.Value.GetReportDTO()), viewAllReports);
                    // AdminPanel tickets list
                    SendTicketsListToAdminPanel(player);
                }
                if (player.Character.AdminLVL >= ReportConfigs.adminLvLMoneyTransfer)
                {
                    player.TriggerCefEvent("transfersConfirmation/setTransfersList", JsonConvert.SerializeObject(MoneyTransfers.Values));
                }
            }
        }

        public static void OnAdminUnload(ExtPlayer player)
        {
            if (Admins.Contains(player))
                Admins.Remove(player);
        }

        
        // === ASTRO AdminPanel bridge (Player Support / Tickets) ===
        private static bool CanUseAdminPanel(ExtPlayer player)
        {
            try { return player != null && player.IsLogged() && (player.Character?.AdminLVL ?? 0) >= ReportConfigs.adminLvL && AdminAuthService.IsAuthenticated(player); }
            catch { return false; }
        }

        private static void PushAdminPanelData(ExtPlayer admin, string clientEventName, string cefEventName, string json)
        {
            SafeTrigger.ClientEvent(admin, clientEventName, json);
            admin.TriggerCefEvent(cefEventName, json);
        }

        private static void PushAdminPanelError(ExtPlayer admin, string clientEventName, string cefEventName, string message)
        {
            SafeTrigger.ClientEvent(admin, clientEventName, JsonConvert.SerializeObject(message));
            admin.TriggerCefEvent(cefEventName, JsonConvert.SerializeObject(message));
        }

        private static bool HasColumn(DataRow row, string columnName)
        {
            return row?.Table?.Columns != null && row.Table.Columns.Contains(columnName);
        }

        private static int ReadInt(DataRow row, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (!HasColumn(row, columnName) || row[columnName] == DBNull.Value) continue;
                if (int.TryParse(row[columnName].ToString(), out int value)) return value;
            }
            return 0;
        }

        private static long ReadLong(DataRow row, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (!HasColumn(row, columnName) || row[columnName] == DBNull.Value) continue;
                if (long.TryParse(row[columnName].ToString(), out long value)) return value;
            }
            return 0;
        }

        private static string ReadString(DataRow row, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (!HasColumn(row, columnName) || row[columnName] == DBNull.Value) continue;
                var value = row[columnName]?.ToString();
                if (!string.IsNullOrWhiteSpace(value)) return value;
            }
            return string.Empty;
        }

        private static string SerializeTicketsForAdminPanel()
        {
            var list = Reports.Values
                .OrderByDescending(r => r.OpenedDate)
                .Select(r => new AdminTicketDTO(r))
                .ToList();
            return JsonConvert.SerializeObject(list);
        }

        private static void SendTicketsListToAdminPanel(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;
            var json = SerializeTicketsForAdminPanel();
            SafeTrigger.ClientEvent(admin, "admin:tickets:set", json);
        }

        private static void PushTicketUpsertToAdmins(Report report)
        {
            if (report == null) return;
            var json = JsonConvert.SerializeObject(new AdminTicketDTO(report));
            foreach (var admin in Admins.ToArray())
            {
                if (!CanUseAdminPanel(admin)) continue;
                SafeTrigger.ClientEvent(admin, "admin:ticket:upsert", json);
            }
        }

        private static Report GetReportSafe(int id)
        {
            if (!Reports.ContainsKey(id)) return null;
            return Reports[id];
        }

        private static string GetPenaltyPlayerName(int uuid)
        {
            if (uuid <= 0) return "PLAYER_NAME";
            if (Main.PlayerNames.ContainsKey(uuid)) return Main.PlayerNames[uuid];

            var data = MySQL.QueryRead("SELECT `firstname`, `lastname` FROM `characters` WHERE `uuid` = @prop0 LIMIT 1", uuid);
            if (data?.Rows == null || data.Rows.Count == 0) return $"PLAYER_{uuid}";

            var firstName = data.Rows[0]["firstname"]?.ToString() ?? "PLAYER";
            var lastName = data.Rows[0]["lastname"]?.ToString() ?? uuid.ToString();
            var fullName = $"{firstName}_{lastName}";

            if (!Main.PlayerNames.ContainsKey(uuid))
                Main.PlayerNames.Add(uuid, fullName);
            if (!Main.PlayerUUIDs.ContainsKey(fullName))
                Main.PlayerUUIDs.Add(fullName, uuid);

            return fullName;
        }

        private static string FormatPenaltyAdminName(string adminName, int adminUuid = -1)
        {
            string safeName = string.IsNullOrWhiteSpace(adminName) ? "ADMIN" : adminName.Trim().Replace(' ', '_');
            int uuid = adminUuid;

            if (uuid <= 0 && Main.PlayerUUIDs.ContainsKey(safeName))
                uuid = Main.PlayerUUIDs[safeName];

            if (uuid > 0)
                return $"{safeName}_{uuid}";

            return safeName;
        }

        private static string FormatPenaltyAdminName(int adminUuid)
        {
            if (adminUuid <= 0) return "ADMIN";
            return FormatPenaltyAdminName(GetPenaltyPlayerName(adminUuid), adminUuid);
        }

        private static string NormalizePenaltyReason(string value, string fallback = "REASON:")
        {
            var text = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(text))
                return fallback;
            return $"REASON: {text}";
        }

        private static string NormalizePenaltyUnit(string unit)
        {
            var value = (unit ?? string.Empty).Trim().ToUpper();
            switch (value)
            {
                case "MINUTE":
                case "MINUTES":
                    return "MINUTES";
                case "HOUR":
                case "HOURS":
                case "TIME":
                    return "HOURS";
                case "DAY":
                case "DAYS":
                    return "DAYS";
                default:
                    return string.IsNullOrWhiteSpace(value) ? "MINUTES" : value;
            }
        }

        private static bool TryParseAdminLogPenalty(DataRow row, int playerUuid, string playerName, out AdminPenaltyDTO penalty)
        {
            penalty = null;

            var action = row["action"]?.ToString() ?? string.Empty;
            var adminName = row["admin"]?.ToString() ?? "ADMIN";
            var createdAt = Convert.ToDateTime(row["time"]);
            var safePlayerName = string.IsNullOrWhiteSpace(playerName) ? GetPenaltyPlayerName(playerUuid) : playerName;

            string punishment = null;
            string reason = null;

            var muteMatch = Regex.Match(action, @"^mutePlayer\((?<time>\d+),\s*(?<reason>.*)\)$", RegexOptions.IgnoreCase);
            if (muteMatch.Success)
            {
                punishment = $"MUTED FOR {muteMatch.Groups["time"].Value} MINUTES";
                reason = muteMatch.Groups["reason"].Value;
            }

            var demorganMatch = Regex.Match(action, @"^demorgan\((?<time>\d+)(?<unit>[A-Za-z]+),\s*(?<reason>.*)\)$", RegexOptions.IgnoreCase);
            if (punishment == null && demorganMatch.Success)
            {
                punishment = $"JAILED FOR {demorganMatch.Groups["time"].Value} {NormalizePenaltyUnit(demorganMatch.Groups["unit"].Value)}";
                reason = demorganMatch.Groups["reason"].Value;
            }

            var warnJailMatch = Regex.Match(action, @"^Demorgan\s*\((?<time>\d+)\s*(?<unit>[A-Za-z]+),\s*(?<reason>.*)\)$", RegexOptions.IgnoreCase);
            if (punishment == null && warnJailMatch.Success)
            {
                punishment = $"JAILED FOR {warnJailMatch.Groups["time"].Value} {NormalizePenaltyUnit(warnJailMatch.Groups["unit"].Value)}";
                reason = warnJailMatch.Groups["reason"].Value;
            }

            if (punishment == null && action.StartsWith("warnPlayer", StringComparison.OrdinalIgnoreCase))
            {
                punishment = "WARNING";
                reason = action.Substring("warnPlayer".Length);
            }

            if (punishment == null && action.StartsWith("kickPlayer", StringComparison.OrdinalIgnoreCase))
            {
                punishment = "KICKED";
                reason = action.Substring("kickPlayer".Length);
            }

            if (punishment == null)
                return false;

            penalty = new AdminPenaltyDTO
            {
                date = createdAt.ToString("dd.MM.yyyy"),
                playerName = safePlayerName,
                playerId = playerUuid,
                reason = NormalizePenaltyReason(reason),
                punishment = punishment,
                adminName = FormatPenaltyAdminName(adminName),
                sortTime = new DateTimeOffset(createdAt).ToUnixTimeMilliseconds()
            };
            return true;
        }

        private static AdminPenaltyDTO CreateBanPenalty(DataRow row, int playerUuid, string playerName)
        {
            var createdAt = Convert.ToDateTime(row["time"]);
            var until = Convert.ToDateTime(row["until"]);
            bool isHard = false;
            try { isHard = Convert.ToBoolean(row["ishard"]); } catch { }

            return new AdminPenaltyDTO
            {
                date = createdAt.ToString("dd.MM.yyyy"),
                playerName = string.IsNullOrWhiteSpace(playerName) ? GetPenaltyPlayerName(playerUuid) : playerName,
                playerId = playerUuid,
                reason = NormalizePenaltyReason(row["reason"]?.ToString()),
                punishment = isHard ? "HARDBANNED" : $"BANNED UNTIL {until:dd.MM.yyyy}",
                adminName = FormatPenaltyAdminName(Convert.ToInt32(row["admin"])),
                sortTime = new DateTimeOffset(createdAt).ToUnixTimeMilliseconds()
            };
        }

        private static List<int> FindPenaltySearchTargets(string query)
        {
            var result = new List<int>();
            var text = (query ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(text))
                return result;

            if (int.TryParse(text, out int uuid) && uuid > 0)
                result.Add(uuid);

            string directName = text.Replace(' ', '_');
            if (Main.PlayerUUIDs.ContainsKey(directName))
                result.Add(Main.PlayerUUIDs[directName]);

            string[] tokens = text.Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            DataTable data = null;

            if (tokens.Length >= 2)
            {
                data = MySQL.QueryRead(
                    "SELECT `uuid` FROM `characters` WHERE LOWER(`firstname`) LIKE @prop0 AND LOWER(`lastname`) LIKE @prop1 LIMIT 25",
                    $"%{tokens[0].ToLower()}%",
                    $"%{tokens[1].ToLower()}%"
                );
            }
            else if (!int.TryParse(text, out _))
            {
                var like = $"%{text.ToLower().Replace(" ", "%")}%";
                data = MySQL.QueryRead(
                    "SELECT `uuid` FROM `characters` WHERE CONCAT(LOWER(`firstname`), '_', LOWER(`lastname`)) LIKE @prop0 OR CONCAT(LOWER(`firstname`), ' ', LOWER(`lastname`)) LIKE @prop1 LIMIT 25",
                    like,
                    like
                );
            }

            if (data?.Rows != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    int foundUuid = Convert.ToInt32(row["uuid"]);
                    if (!result.Contains(foundUuid))
                        result.Add(foundUuid);
                }
            }

            return result;
        }

        public static void AdminPanel_SearchPenalties(ExtPlayer admin, string query)
        {
            if (!CanUseAdminPanel(admin)) return;

            var targets = FindPenaltySearchTargets(query);
            var penalties = new List<AdminPenaltyDTO>();

            foreach (var uuid in targets)
            {
                var playerName = GetPenaltyPlayerName(uuid);

                var adminLogs = MySQL.QueryRead(
                    $"SELECT `time`, `admin`, `action`, `player` FROM `{GameLog.DB}`.`adminlog` WHERE `player` = @prop0 ORDER BY `time` DESC LIMIT 80",
                    playerName
                );

                if (adminLogs?.Rows != null)
                {
                    foreach (DataRow row in adminLogs.Rows)
                    {
                        if (TryParseAdminLogPenalty(row, uuid, playerName, out var penalty))
                            penalties.Add(penalty);
                    }
                }

                var banLogs = MySQL.QueryRead(
                    $"SELECT `time`, `admin`, `player`, `until`, `reason`, `ishard` FROM `{GameLog.DB}`.`banlog` WHERE `player` = @prop0 ORDER BY `time` DESC LIMIT 40",
                    uuid
                );

                if (banLogs?.Rows != null)
                {
                    foreach (DataRow row in banLogs.Rows)
                        penalties.Add(CreateBanPenalty(row, uuid, playerName));
                }
            }

            var json = JsonConvert.SerializeObject(
                penalties
                    .OrderByDescending(item => item.sortTime)
                    .Take(100)
                    .ToList()
            );

            PushAdminPanelData(admin, "admin:penalties:set", "reportMenu/setPenalties", json);
        }

public static void SendReport(ExtPlayer player, string message)
        {
            if (!player.IsLogged())
                return;
            //if (player.Character.AdminLVL > 0)
            //{
            //    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"ReportMenu_29", 3000);
            //    return;
            //}
            if (message.Length > 150)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The message is too long", 3000);
                return;
            }
            int currentReport = -1;
            if (player.Character.OpenedReport != -1)
            {
                if (Reports.ContainsKey(player.Character.OpenedReport))
                {
                    Report report = Reports[player.Character.OpenedReport];
                    if (!report.Closed)
                        currentReport = player.Character.OpenedReport;
                }
            }
            if (currentReport == -1 && player.HasData("NEXT_REPORT"))
            {
                DateTime nextReport = player.GetData<DateTime>("NEXT_REPORT");
                if (DateTime.Now < nextReport)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Try it out in a few minutes", 3000);
                    return;
                }
            }
            if (player.IsMuted)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You cannot send a complaint if you have a mute.", 3000);
                return;
            }
            SafeTrigger.SetData(player, "NEXT_REPORT", DateTime.Now.AddMinutes(2));
            if (currentReport != -1)
                SendMessage(player, currentReport, message);
            else
                CreateReport(player, message);
        }

        public static void CreateReport(ExtPlayer player, string question)
        {
            Report report = new Report(player.Character, player.Value, player.Account);
            report.AddMessage(player.Character, question);
            Reports.Add(report.ID, report);
            report.Send("report:addreport", false, report.GetSerializeReportDTO());
            // AdminPanel realtime update
            PushTicketUpsertToAdmins(report);
            player.Character.OpenedReport = report.ID;
        }

        public static void SendMessage(ExtPlayer player, int id, string message, bool saveToDB = true)
        {
            if (!player.IsLogged())
                return;
            if (!Reports.ContainsKey(id))
                return;
            var character = player.Character;
            Report report = Reports[id];
            var answer = report.AddMessage(character, message, saveToDB);

            report.Send("report:addmessage", false, answer.GetSerializeReportAnswerDTO());
            PushTicketUpsertToAdmins(report);

            if (character.UUID != report.AuthorUUID && saveToDB)
            {
                var target = Trigger.GetPlayerByUuid(report.AuthorUUID);
                if (target != null)
                {
                    SafeTrigger.ClientEvent(target, "report:player:answer", player.Value, player.Name.Replace("_", " "), message);
                    foreach (var adm in Admins)
                    {
                        Chat.AdmiAnswer(adm, target, player, message);
                    }
                    Admin.adminSMS(player, target, message);
                    // Chat.SendTo(target, message);
                }
            }
        }

        public static void SetAdminToReport(Character.Character character, Report report)
        {
            if (report.Closed)
                return;
            if (character != null)
            {
                report.AdminName = character.FullName;
                report.AdminUUID = character.UUID;
            }
            else
            {
                report.AdminName = null;
                report.AdminUUID = -1;
            }
            report.Send("report:updatetakereport", true, report.ID, report.AdminName?.Replace('_', ' '));
            // AdminPanel realtime update
            PushTicketUpsertToAdmins(report);
        }

        public static void CloseReport(Character.Character admin, Report report)
        {
            report.Closed = true;
            if (admin != null)
            {
                report.AdminName = admin.FullName;
                report.AdminUUID = admin.UUID;
            }

            report.ClosedDate = DateTime.Now;
            MySQL.Query("UPDATE `reports` SET `closedate` = @prop0, `adminuuid` = @prop1 WHERE `id` = @prop2", MySQL.ConvertTime(report.ClosedDate), report.AdminUUID, report.ID);
            report.Send("report:closereport", true, report.ID, report.AdminName?.Replace('_', ' '));
            // AdminPanel realtime update
            PushTicketUpsertToAdmins(report);
        }

        
        // === AdminPanel actions ===
        public static void AdminPanel_RequestTickets(ExtPlayer admin)
        {
            SendTicketsListToAdminPanel(admin);
        }

        public static void AdminPanel_ClaimTicket(ExtPlayer admin, int id)
        {
            if (!CanUseAdminPanel(admin)) return;
            var report = GetReportSafe(id);
            if (report == null || report.Closed) return;
            if (!string.IsNullOrEmpty(report.AdminName)) return; // already assigned
            SetAdminToReport(admin.Character, report);
        }

        public static void AdminPanel_CloseTicket(ExtPlayer admin, int id)
        {
            if (!CanUseAdminPanel(admin)) return;
            var report = GetReportSafe(id);
            if (report == null || report.Closed) return;
            CloseReport(admin.Character, report);
        }

        public static void AdminPanel_SetNote(ExtPlayer admin, int id, string note)
        {
            if (!CanUseAdminPanel(admin)) return;
            var report = GetReportSafe(id);
            if (report == null) return;
            report.InternalNote = note ?? string.Empty;
            PushTicketUpsertToAdmins(report);
        }

        public static void AdminPanel_SendMessage(ExtPlayer admin, int id, string text)
        {
            if (!CanUseAdminPanel(admin)) return;
            if (string.IsNullOrWhiteSpace(text)) return;
            SendMessage(admin, id, text.Trim());
        }

        public static void AdminPanel_TeleportToPlayer(ExtPlayer admin, int id)
        {
            if (!CanUseAdminPanel(admin)) return;
            var report = GetReportSafe(id);
            if (report == null) return;
            var target = Trigger.GetPlayerByUuid(report.AuthorUUID);
            if (target == null) return;
            Admin.teleportToPlayer(admin, target);
        }

        public static void AdminPanel_SpectatePlayer(ExtPlayer admin, int id)
        {
            if (!CanUseAdminPanel(admin)) return;
            var report = GetReportSafe(id);
            if (report == null) return;
            var target = Trigger.GetPlayerByUuid(report.AuthorUUID);
            if (target == null) return;
            AdminSP.Spectate(admin, target);
        }

        public static void AdminPanel_RequestOrganizations(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            try
            {
                var rows = new List<AdminOrganizationDTO>();
                var table = MySQL.QueryRead("SELECT `id`,`money`,`fuellimit`,`fuelleft` FROM `fractions` ORDER BY `id` ASC");
                if (table == null)
                {
                    PushAdminPanelError(admin, "admin:orgs:error", "reportMenu/setOrganizationsError", "Failed to load organizations from fractions.");
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    int fractionId = ReadInt(row, "id");
                    rows.Add(new AdminOrganizationDTO
                    {
                        id = fractionId,
                        orgName = Whistler.Fractions.Manager.getName(fractionId) ?? $"Fraction {fractionId}",
                        balance = ReadLong(row, "money"),
                        fuelLimit = ReadInt(row, "fuellimit"),
                        fuelLeft = ReadInt(row, "fuelleft")
                    });
                }

                PushAdminPanelData(admin, "admin:orgs:set", "reportMenu/setOrganizations", JsonConvert.SerializeObject(rows));
            }
            catch (Exception ex)
            {
                _logger.WriteError($"AdminPanel_RequestOrganizations: {ex}");
                PushAdminPanelError(admin, "admin:orgs:error", "reportMenu/setOrganizationsError", "Failed to load organizations from fractions.");
            }
        }

        public static void AdminPanel_OrganizationTeleport(ExtPlayer admin, int playerId)
        {
            if (!CanUseAdminPanel(admin)) return;
            var target = Trigger.GetPlayerByUuid(playerId);
            if (target == null) return;
            Admin.teleportToPlayer(admin, target);
        }

        public static void AdminPanel_OrganizationPlayerStats(ExtPlayer admin, int playerId)
        {
            if (!CanUseAdminPanel(admin)) return;
            var target = Trigger.GetPlayerByUuid(playerId);
            if (target == null)
            {
                Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter, "Player is offline.", 3000);
                return;
            }

            var stats = $"[{target.Name}] ID:{target.Character.UUID} LVL:{target.Character.LVL} CASH:{target.Character.Money} FRACTION:{target.Character.FractionID}";
            Chat.SendTo(admin, stats);
            Notify.Send(admin, NotifyType.Info, NotifyPosition.BottomCenter, "Player stats sent to chat.", 3000);
        }

        public static void AdminPanel_RequestBusinesses(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            var rows = BusinessManager.BizList.Values
                .OrderBy(item => item.ID)
                .Select(item => new AdminBusinessDTO
                {
                    id = item.ID,
                    businessName = string.IsNullOrWhiteSpace(item.Name) ? (item.TypeModel?.TypeName ?? "BUSINESS") : item.Name,
                    ownerName = item.OwnerID > 0 ? (Main.PlayerNames.ContainsKey(item.OwnerID) ? Main.PlayerNames[item.OwnerID] : "Unknown") : "STATE",
                    ownerId = item.OwnerID,
                    businessNumber = item.ID,
                    balance = item.BankAccountModel?.Balance ?? 0,
                    x = item.EnterPoint.X,
                    y = item.EnterPoint.Y,
                    z = item.EnterPoint.Z,
                    dimension = item.Dimension
                })
                .ToList();

            PushAdminPanelData(admin, "admin:biz:set", "reportMenu/setBusinesses", JsonConvert.SerializeObject(rows));
        }

        public static void AdminPanel_BusinessTeleport(ExtPlayer admin, int businessId)
        {
            if (!CanUseAdminPanel(admin)) return;
            if (!BusinessManager.BizList.ContainsKey(businessId)) return;
            var business = BusinessManager.BizList[businessId];
            admin.ChangePosition(business.EnterPoint + new Vector3(0, 0, 1.2));
            SafeTrigger.UpdateDimension(admin, business.Dimension);
        }

        public static void AdminPanel_RequestCommands(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            try
            {
                var table = MySQL.QueryRead("SELECT `minrank`,`command`,`isadmin`,`idkey`,`istech` FROM `adminaccess` WHERE `isadmin` = 1 ORDER BY `minrank` ASC, `command` ASC");
                if (table == null)
                {
                    PushAdminPanelError(admin, "admin:commands:error", "reportMenu/setCommandsError", "Failed to load commands from adminaccess.");
                    return;
                }

                var rows = new List<(int minrank, string command)>();
                foreach (DataRow row in table.Rows)
                {
                    rows.Add((ReadInt(row, "minrank"), ReadString(row, "command")));
                }

                var groups = rows
                    .GroupBy(item => item.minrank)
                    .OrderBy(group => group.Key)
                    .Select(group => new
                    {
                        level = group.Key,
                        title = group.Key == 1 ? "ASSISTANT ADMINISTRATOR" : group.Key == 2 ? "SERVER ADMINISTRATOR" : group.Key == 3 ? "CURATOR ADMINISTRATOR" : group.Key == 4 ? "CURATOR ADMINISTRATOR" : "ADMINISTRATOR",
                        commands = group
                            .Select(item => item.command)
                            .Where(command => !string.IsNullOrWhiteSpace(command))
                            .Distinct()
                            .OrderBy(command => command)
                            .ToList()
                    })
                    .ToList();

                PushAdminPanelData(admin, "admin:commands:set", "reportMenu/setCommands", JsonConvert.SerializeObject(groups));
            }
            catch (Exception ex)
            {
                _logger.WriteError($"AdminPanel_RequestCommands: {ex}");
                PushAdminPanelError(admin, "admin:commands:error", "reportMenu/setCommandsError", "Failed to load commands from adminaccess.");
            }
        }

        public static void AdminPanel_RequestAdministrators(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            try
            {
                var rows = new List<AdminAdministratorDTO>();
                var table = MySQL.QueryRead("SELECT * FROM `admin_users` LIMIT 500");
                if (table == null)
                {
                    PushAdminPanelError(admin, "admin:administrators:error", "reportMenu/setAdministratorsError", "Failed to load administrators from admin_users.");
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    int uuid = ReadInt(row, "uuid", "playerId", "player_id", "player_uuid", "characterId", "character_id", "id");
                    string fullName = ReadString(row, "playerName", "player_name", "name", "username", "login");
                    if (string.IsNullOrWhiteSpace(fullName) && HasColumn(row, "firstname") && HasColumn(row, "lastname"))
                        fullName = $"{row["firstname"]}_{row["lastname"]}";
                    if (string.IsNullOrWhiteSpace(fullName))
                        fullName = uuid > 0 ? $"Admin #{uuid}" : "Administrator";

                    int adminLevel = ReadInt(row, "rank", "adminlvl", "admin_level", "adminLevel", "level");
                    var online = Trigger.GetPlayerByUuid(uuid) != null;
                    string lastSeen = ReadString(row, "lastLogin", "last_login", "lastlogin", "lastSeen", "last_seen", "updated_at", "created_at");

                    rows.Add(new AdminAdministratorDTO
                    {
                        status = online ? "ONLINE" : "OFFLINE",
                        playerName = fullName,
                        playerId = uuid,
                        adminLevel = adminLevel,
                        adminRankName = Group.GroupNames.Count > adminLevel ? Group.GroupNames[adminLevel] : $"Admin Level {adminLevel}",
                        lastSeen = online ? "ONLINE NOW" : (string.IsNullOrWhiteSpace(lastSeen) ? "-" : lastSeen)
                    });
                }

                PushAdminPanelData(admin, "admin:administrators:set", "reportMenu/setAdministrators", JsonConvert.SerializeObject(rows));
            }
            catch (Exception ex)
            {
                _logger.WriteError($"AdminPanel_RequestAdministrators: {ex}");
                PushAdminPanelError(admin, "admin:administrators:error", "reportMenu/setAdministratorsError", "Failed to load administrators from admin_users.");
            }
        }

        public static void AdminPanel_SetAdministratorRank(ExtPlayer admin, int playerId, int rank)
        {
            if (!CanUseAdminPanel(admin)) return;
            if (rank < 1 || rank >= admin.Character.AdminLVL) return;

            var target = Trigger.GetPlayerByUuid(playerId);
            if (target != null)
            {
                Admin.setPlayerAdminRank(admin, target, rank);
            }
            else
            {
                var result = MySQL.QueryRead("SELECT `adminlvl` FROM `characters` WHERE `uuid` = @prop0", playerId);
                if (result?.Rows == null || result.Rows.Count == 0) return;
                int currentLevel = Convert.ToInt32(result.Rows[0]["adminlvl"]);
                if (currentLevel >= admin.Character.AdminLVL) return;
                MySQL.Query("UPDATE `characters` SET `adminlvl` = @prop0 WHERE `uuid` = @prop1", rank, playerId);
                string targetName = Main.PlayerNames.ContainsKey(playerId) ? Main.PlayerNames[playerId] : playerId.ToString();
                GameLog.Admin(admin.Name, $"setAdminRank({rank})", targetName);
            }

            AdminPanel_RequestAdministrators(admin);
        }

        public static void AdminPanel_RequestTeleports(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            var rows = new List<AdminTeleportDTO>
            {
                new AdminTeleportDTO { placeName = "NG HQ", x = 3.650193f, y = -29.05571f, z = 70.52247f, dimension = 0 },
                new AdminTeleportDTO { placeName = "LSPD HQ", x = 596.0463f, y = -13.40078f, z = 82.74032f, dimension = 0 },
                new AdminTeleportDTO { placeName = "EMS HQ", x = 322.2866f, y = -595.14f, z = 43.09046f, dimension = 0 },
                new AdminTeleportDTO { placeName = "LIFE INVADER HQ", x = -1063.046f, y = -249.463f, z = 44.0211f, dimension = 0 },
                new AdminTeleportDTO { placeName = "SAHP HQ", x = -2357.276f, y = 3251.052f, z = 32.81071f, dimension = 0 },
                new AdminTeleportDTO { placeName = "FIB HQ", x = 109.8975f, y = -753.0673f, z = 242.1521f, dimension = 0 },
                new AdminTeleportDTO { placeName = "BEACH MARKET", x = -1171.188f, y = -1571.089f, z = 4.663622f, dimension = 0 },
                new AdminTeleportDTO { placeName = "ADMIN HQ", x = 2456.746f, y = 4979.63f, z = 45.6903f, dimension = 0 },
                new AdminTeleportDTO { placeName = "BLACK MARKET", x = 412.7089f, y = 315.3876f, z = 103.1326f, dimension = 0 }
            };

            rows.ForEach(item => item.coords = $"{item.x:0.###}, {item.y:0.###}, {item.z:0.###}");
            PushAdminPanelData(admin, "admin:teleports:set", "reportMenu/setTeleports", JsonConvert.SerializeObject(rows));
        }

        public static void AdminPanel_GoToTeleport(ExtPlayer admin, float x, float y, float z, uint dimension)
        {
            if (!CanUseAdminPanel(admin)) return;
            admin.ChangePosition(new Vector3(x, y, z + 1.0f));
            SafeTrigger.UpdateDimension(admin, dimension);
        }

        public static void AdminPanel_RequestFamilies(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            try
            {
                var rows = new List<AdminFamilyDTO>();
                var table = MySQL.QueryRead("SELECT `f_id`,`f_name`,`f_owner`,`f_money`,`f_typefam` FROM `families` ORDER BY `f_id` ASC");
                if (table == null)
                {
                    PushAdminPanelError(admin, "admin:families:error", "reportMenu/setFamiliesError", "Failed to load families from families.");
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    int familyId = ReadInt(row, "f_id");
                    int leaderId = ReadInt(row, "f_owner");
                    var house = HouseManager.GetHouse(familyId, OwnerType.Family, true);

                    rows.Add(new AdminFamilyDTO
                    {
                        id = familyId,
                        familyName = ReadString(row, "f_name"),
                        leaderName = Main.PlayerNames.ContainsKey(leaderId) ? Main.PlayerNames[leaderId] : leaderId.ToString(),
                        leaderId = leaderId,
                        bank = ReadLong(row, "f_money"),
                        houseNumber = house?.ID ?? 0,
                        status = ReadInt(row, "f_typefam") > 0 ? "OFFICIAL" : "UNOFFICIAL"
                    });
                }

                PushAdminPanelData(admin, "admin:families:set", "reportMenu/setFamilies", JsonConvert.SerializeObject(rows));
            }
            catch (Exception ex)
            {
                _logger.WriteError($"AdminPanel_RequestFamilies: {ex}");
                PushAdminPanelError(admin, "admin:families:error", "reportMenu/setFamiliesError", "Failed to load families from families.");
            }
        }

        public static void AdminPanel_JoinFamily(ExtPlayer admin, int familyId)
        {
            if (!CanUseAdminPanel(admin)) return;
            var family = FamilyManager.GetFamily(familyId);
            if (family == null) return;

            if (admin.Character.FamilyID > 0)
            {
                var currentFamily = FamilyManager.GetFamily(admin.Character.FamilyID);
                currentFamily?.DeleteMember(admin.Character.UUID);
            }

            family.NewMember(admin.Character.UUID, 1);
            admin.Character.FamilyID = family.Id;
            admin.Character.FamilyLVL = 1;
            SafeTrigger.SetSharedData(admin, "family", family.Id);
            Notify.Send(admin, NotifyType.Success, NotifyPosition.BottomCenter, $"You joined family {family.Name}.", 3000);
        }

        public static void AdminPanel_FamilyTeleportHouse(ExtPlayer admin, int familyId)
        {
            if (!CanUseAdminPanel(admin)) return;
            var house = HouseManager.GetHouse(familyId, OwnerType.Family, true);
            if (house == null)
            {
                Notify.Send(admin, NotifyType.Error, NotifyPosition.BottomCenter, "Family house not found.", 3000);
                return;
            }

            admin.ChangePosition(house.Position + new Vector3(0, 0, 1.2));
            SafeTrigger.UpdateDimension(admin, house.Dimension);
        }

        public static void AdminPanel_CreateEvent(ExtPlayer admin, AdminEventDTO model)
        {
            if (!CanUseAdminPanel(admin) || model == null) return;
            if (string.IsNullOrWhiteSpace(model.name)) return;

            MySQL.Query(
                "CREATE TABLE IF NOT EXISTS `adminpanel_events` (`id` INT NOT NULL AUTO_INCREMENT, `name` VARCHAR(128) NOT NULL, `lvl` INT NOT NULL, `health` INT NOT NULL, `armor` INT NOT NULL, `reward` INT NOT NULL, `maxplayers` INT NOT NULL, `portalStart` VARCHAR(64) NULL, `portalEnd` VARCHAR(64) NULL, `description` TEXT NULL, `createdBy` INT NOT NULL, `createdAt` DATETIME NOT NULL, PRIMARY KEY (`id`));"
            );

            MySQL.Query(
                "INSERT INTO `adminpanel_events` (`name`,`lvl`,`health`,`armor`,`reward`,`maxplayers`,`portalStart`,`portalEnd`,`description`,`createdBy`,`createdAt`) VALUES (@prop0,@prop1,@prop2,@prop3,@prop4,@prop5,@prop6,@prop7,@prop8,@prop9,@prop10)",
                model.name,
                model.level,
                model.health,
                model.armor,
                model.reward,
                model.maxPlayers,
                model.portalStart ?? "",
                model.portalEnd ?? "",
                model.description ?? "",
                admin.Character.UUID,
                MySQL.ConvertTime(DateTime.Now)
            );

            Chat.SendToAdmins(1, $"[EVENT] {admin.Name} created event {model.name} (lvl {model.level}, hp {model.health}, armor {model.armor}, reward {model.reward}).");
            Notify.Send(admin, NotifyType.Success, NotifyPosition.BottomCenter, "Event created.", 3000);
            SafeTrigger.ClientEventForAll("admin:event:created", JsonConvert.SerializeObject(model));
        }

        public static void AdminPanel_RequestEvents(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            MySQL.Query(
                "CREATE TABLE IF NOT EXISTS `adminpanel_events` (`id` INT NOT NULL AUTO_INCREMENT, `name` VARCHAR(128) NOT NULL, `lvl` INT NOT NULL, `health` INT NOT NULL, `armor` INT NOT NULL, `reward` INT NOT NULL, `maxplayers` INT NOT NULL, `portalStart` VARCHAR(64) NULL, `portalEnd` VARCHAR(64) NULL, `description` TEXT NULL, `createdBy` INT NOT NULL, `createdAt` DATETIME NOT NULL, PRIMARY KEY (`id`));"
            );

            var rows = new List<object>();
            var table = MySQL.QueryRead("SELECT `id`,`name`,`lvl`,`health`,`armor`,`reward`,`maxplayers`,`portalStart`,`portalEnd`,`description`,`createdBy`,`createdAt` FROM `adminpanel_events` ORDER BY `createdAt` DESC LIMIT 50");
            if (table?.Rows != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    rows.Add(new
                    {
                        id = Convert.ToInt32(row["id"]),
                        name = row["name"]?.ToString() ?? "EVENT",
                        level = Convert.ToInt32(row["lvl"]),
                        health = Convert.ToInt32(row["health"]),
                        armor = Convert.ToInt32(row["armor"]),
                        reward = Convert.ToInt32(row["reward"]),
                        maxPlayers = Convert.ToInt32(row["maxplayers"]),
                        portalStart = row["portalStart"]?.ToString() ?? string.Empty,
                        portalEnd = row["portalEnd"]?.ToString() ?? string.Empty,
                        description = row["description"]?.ToString() ?? string.Empty,
                        createdBy = Convert.ToInt32(row["createdBy"]),
                        createdAt = new DateTimeOffset(Convert.ToDateTime(row["createdAt"])).ToUnixTimeMilliseconds()
                    });
                }
            }

            PushAdminPanelData(admin, "admin:events:set", "reportMenu/setEventEntries", JsonConvert.SerializeObject(rows));
        }

        public static void AdminPanel_RequestMurders(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            var rows = new List<object>();
            var table = MySQL.QueryRead(
                $"SELECT `id`,`killer`,`target`,`clientweapon`,`serverweapon`,`date`,`distance` FROM `{GameLog.DB}`.`killog` ORDER BY `id` DESC LIMIT 100"
            );
            if (table?.Rows != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string serverWeapon = row["serverweapon"]?.ToString() ?? "0";
                    string clientWeapon = row["clientweapon"]?.ToString() ?? "0";
                    rows.Add(new
                    {
                        id = Convert.ToInt32(row["id"]),
                        killer = row["killer"]?.ToString() ?? "UNKNOWN",
                        target = row["target"]?.ToString() ?? "UNKNOWN",
                        weapon = serverWeapon != "0" ? serverWeapon : clientWeapon,
                        distance = Convert.ToInt32(row["distance"]),
                        date = new DateTimeOffset(Convert.ToDateTime(row["date"])).ToUnixTimeMilliseconds()
                    });
                }
            }

            PushAdminPanelData(admin, "admin:murders:set", "reportMenu/setMurders", JsonConvert.SerializeObject(rows));
        }

        public static void AdminPanel_RequestArena(ExtPlayer admin)
        {
            if (!CanUseAdminPanel(admin)) return;

            var rows = LobbyManager.Lobbies.Values
                .OrderBy(lobby => lobby.Id)
                .Select(lobby =>
                {
                    var strikeLobby = lobby as StrikeLobby;
                    var maxPlayers = strikeLobby?.Settings?.MaxPlayers ?? 0;
                    var battleMode = strikeLobby?.Settings?.BattleMode.ToString() ?? "RACING";
                    var rounds = strikeLobby?.Settings?.TotalRounds ?? 0;
                    var entryBet = strikeLobby?.Settings?.EntryBet ?? 0;

                    return new
                    {
                        id = lobby.Id,
                        title = lobby.LocationName.ToString().Replace("_", " ").ToUpper(),
                        type = battleMode.ToUpper(),
                        owner = lobby.Leader?.Player?.Name ?? (lobby.AutoStarted ? "AUTO" : "UNKNOWN"),
                        players = lobby.Members?.Count ?? 0,
                        maxPlayers = maxPlayers,
                        status = lobby.CurrentState.ToString().ToUpper(),
                        rounds = rounds,
                        bet = entryBet
                    };
                })
                .ToList();

            PushAdminPanelData(admin, "admin:arena:set", "reportMenu/setArenaSessions", JsonConvert.SerializeObject(rows));
        }

public static void SetRatingLastReportFromPlayer(ExtPlayer player, int rating)
        {
            var report = Reports.LastOrDefault(item => item.Value.AuthorUUID == player.Character.UUID && item.Value.Rating == -1).Value;
            if (report == null)
                return;
            report.Rating = rating;
            if (report.AdminUUID != -1)
            {
                var Admin = Trigger.GetPlayerByUuid(report.AdminUUID);
                if (Admin != null)
                {
                    Admin.Character.NumberOfAdminRatings++;
                    Admin.Character.AmountOfAdminRatings += rating;
                }
                else
                {
                    MySQL.Query("UPDATE `characters` SET `numberofadminratings` = `numberofadminratings` + 1, `amountofadminratings` = `amountofadminratings` + @prop0 WHERE `uuid` = @prop1;", rating, report.AdminUUID);
                }
            }
            MySQL.Query("UPDATE `reports` SET `rating` = @prop0 WHERE `id` = @prop1", report.Rating, report.ID);
            report.Send("report:sendrating", false, report.ID, report.Rating);
        }


        public static bool CreateTransfer(ulong socialClubFrom, string fromName, string fromTo, CheckingAccount from, CheckingAccount to, int amount, string comment, string reason)
        {
            if (MoneyTransfers.FirstOrDefault(item => item.Value.SocialClubFrom == socialClubFrom).Value != null) return false;
            int id = _moneyTransfersID++;
            MoneyTransfer transfer = new MoneyTransfer(id, socialClubFrom, fromName, fromTo, from, to, amount, comment, reason);
            MoneyTransfers.Add(transfer.ID, transfer);
            transfer.Send("transfersConfirmation/updateTransfersList", JsonConvert.SerializeObject(transfer));
            return true;
        }


        public static void SuccessTransfer(int id, string adminName)
        {
            if (!MoneyTransfers.ContainsKey(id))
                return;
            MoneyTransfers[id].Success(adminName);
            MoneyTransfers.Remove(id);
        }

        public static void CancelTransfer(int id, string adminName)
        {
            if (!MoneyTransfers.ContainsKey(id))
                return;
            MoneyTransfers[id].Canceled(adminName);
            MoneyTransfers.Remove(id);
        }


    }
}
