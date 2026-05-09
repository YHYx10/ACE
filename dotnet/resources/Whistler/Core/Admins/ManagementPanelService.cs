using GTANetworkAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Whistler.Businesses;
using Whistler.Common;
using Whistler.Core.ReportSystem;
using Whistler.Entities;
using Whistler.Families;
using Whistler.Fractions;
using Whistler.Helpers;
using Whistler.Houses;
using Whistler.MoneySystem;
using Whistler.SDK;
using Whistler.VehicleSystem;
using Whistler.VehicleSystem.Models.VehiclesData;

namespace Whistler.Core.Admins
{
    class ManagementPanelService : Script
    {
        private static readonly WhistlerLogger _logger = new WhistlerLogger(typeof(ManagementPanelService));

        [RemoteEvent("management:requestData")]
        public static void RequestData(ExtPlayer player)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                var data = BuildPayload(player);
                SafeTrigger.ClientEvent(player, "management:setData", JsonConvert.SerializeObject(data));
            }
            catch (Exception e)
            {
                _logger.WriteError($"RequestData:\n{e}");
                SafeTrigger.ClientEvent(player, "management:setError", "Failed to load management data.");
            }
        }

        [RemoteEvent("management:playerAction")]
        public static void PlayerAction(ExtPlayer player, string action, int targetUuid)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                var target = Trigger.GetPlayerByUuid(targetUuid);
                if (target == null || !target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Target player is not online.", 3000);
                    return;
                }

                if (!TryRunPlayerAction(player, target, action))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Unknown or disabled management action.", 3000);
                    return;
                }

                LogPlayerAction(player, target, action);
                RequestData(player);
            }
            catch (Exception e)
            {
                _logger.WriteError($"PlayerAction({action}, {targetUuid}):\n{e}");
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Management action failed.", 3000);
            }
        }

        [RemoteEvent("management:punishment:requestHistory")]
        public static void RequestPunishmentHistory(ExtPlayer player, int targetUuid)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                var target = Trigger.GetPlayerByUuid(targetUuid);
                string targetName = target?.Name ?? GetCharacterName(targetUuid);

                var payload = new
                {
                    targetUuid,
                    targetName = string.IsNullOrWhiteSpace(targetName) ? $"UUID {targetUuid}" : targetName,
                    history = LoadPunishmentHistory(targetUuid, targetName),
                    message = "Existing history source: whistlerlogs.adminlog and whistlerlogs.banlog."
                };

                SafeTrigger.ClientEvent(player, "management:punishment:setHistory", JsonConvert.SerializeObject(payload));
            }
            catch (Exception e)
            {
                _logger.WriteError($"RequestPunishmentHistory({targetUuid}):\n{e}");
                SafeTrigger.ClientEvent(player, "management:punishment:setHistoryError", "Failed to load punishment history.");
            }
        }

        [RemoteEvent("management:punishment:execute")]
        public static void ExecutePunishment(ExtPlayer player, string action, int targetUuid, int duration, string reason)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                reason = (reason ?? string.Empty).Trim();
                action = (action ?? string.Empty).Trim().ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(reason))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Punishment reason is required.", 3000);
                    return;
                }

                var target = Trigger.GetPlayerByUuid(targetUuid);
                if (target == null || !target.IsLogged())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Target player is not online.", 3000);
                    return;
                }

                if (RequiresDuration(action) && duration <= 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Duration must be greater than 0.", 3000);
                    return;
                }

                if (!TryRunPunishmentAction(player, target, action, duration, reason))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Unknown or disabled punishment action.", 3000);
                    return;
                }

                LogPunishmentAction(player, target, action, duration, reason);
                RequestPunishmentHistory(player, targetUuid);
                RequestData(player);
            }
            catch (Exception e)
            {
                _logger.WriteError($"ExecutePunishment({action}, {targetUuid}):\n{e}");
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Punishment action failed.", 3000);
            }
        }

        [RemoteEvent("management:database:searchCharacters")]
        public static void SearchCharacters(ExtPlayer player, string query)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                var payload = new
                {
                    query = (query ?? string.Empty).Trim(),
                    results = LoadCharacterSearchResults(query),
                    message = "Read-only character search. No database writes are performed."
                };

                SafeTrigger.ClientEvent(player, "management:database:setSearchResults", JsonConvert.SerializeObject(payload));
            }
            catch (Exception e)
            {
                _logger.WriteError($"SearchCharacters({query}):\n{e}");
                SafeTrigger.ClientEvent(player, "management:database:setError", "Failed to search characters.");
            }
        }

        [RemoteEvent("management:database:getCharacterProfile")]
        public static void GetCharacterProfile(ExtPlayer player, int targetUuid)
        {
            if (!CanUseManagement(player))
                return;

            try
            {
                var profile = LoadCharacterProfile(targetUuid);
                if (profile == null)
                {
                    SafeTrigger.ClientEvent(player, "management:database:setError", "Character profile was not found.");
                    return;
                }

                SafeTrigger.ClientEvent(player, "management:database:setProfile", JsonConvert.SerializeObject(profile));
            }
            catch (Exception e)
            {
                _logger.WriteError($"GetCharacterProfile({targetUuid}):\n{e}");
                SafeTrigger.ClientEvent(player, "management:database:setError", "Failed to load character profile.");
            }
        }

        private static bool CanUseManagement(ExtPlayer player)
        {
            try
            {
                return player != null &&
                       player.IsLogged() &&
                       (player.Character?.AdminLVL ?? 0) >= 8 &&
                       AdminAuthService.IsAuthenticated(player);
            }
            catch
            {
                return false;
            }
        }

        private static object BuildPayload(ExtPlayer requester)
        {
            var onlinePlayers = Trigger.GetAllPlayers().Where(p => p != null && p.IsLogged()).ToList();
            int authenticatedAdmins = onlinePlayers.Count(p => (p.Character?.AdminLVL ?? 0) >= 1 && AdminAuthService.IsAuthenticated(p));
            int authenticatedSenior = onlinePlayers.Count(p => (p.Character?.AdminLVL ?? 0) >= 8 && AdminAuthService.IsAuthenticated(p));

            int openReports = ReportManager.Reports.Values.Count(report => report != null && !report.Closed);
            int unassignedReports = ReportManager.Reports.Values.Count(report =>
                report != null &&
                !report.Closed &&
                report.AdminUUID <= 0 &&
                string.IsNullOrWhiteSpace(report.AdminName)
            );

            var seniorStaff = LoadSeniorStaff(onlinePlayers);
            int totalAdmins = ReadCount("SELECT COUNT(*) FROM `characters` WHERE `adminlvl` >= 1");

            return new
            {
                loadedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                dashboard = new
                {
                    onlinePlayers = onlinePlayers.Count,
                    authenticatedAdmins,
                    authenticatedSenior,
                    openReports,
                    unassignedReports,
                    totalAdmins,
                    totalSeniorStaff = seniorStaff.Count,
                    managementAccess = "AUTHORIZED",
                    requester = new
                    {
                        name = requester.Name,
                        uuid = requester.Character.UUID,
                        adminLevel = requester.Character.AdminLVL,
                        authenticated = true
                    }
                },
                players = LoadOnlinePlayers(onlinePlayers),
                seniorStaff,
                server = new
                {
                    resource = "Whistler",
                    status = "ONLINE",
                    onlinePlayers = onlinePlayers.Count,
                    openReports,
                    unassignedReports,
                    dangerousControls = "DISABLED",
                    notes = new[]
                    {
                        "Read-only management phase is active.",
                        "Restart, stop, console and resource actions are intentionally disabled.",
                        "All management requests are gated by adminlvl >= 8 and /alogin."
                    }
                }
            };
        }

        private static bool TryRunPlayerAction(ExtPlayer player, ExtPlayer target, string action)
        {
            switch ((action ?? string.Empty).ToLowerInvariant())
            {
                case "goto":
                    Admin.teleportToPlayer(player, target);
                    return true;
                case "bring":
                    Admin.teleportTargetToPlayer(player, target, false);
                    return true;
                case "freeze":
                    Admin.freezeTarget(player, target);
                    return true;
                case "unfreeze":
                    Admin.unFreezeTarget(player, target);
                    return true;
                case "spectate":
                    if (!Group.CanUseAdminCommand(player, "sp"))
                        return true;
                    Whistler.Core.AdminSP.Spectate(player, target);
                    return true;
                default:
                    return false;
            }
        }

        private static bool TryRunPunishmentAction(ExtPlayer player, ExtPlayer target, string action, int duration, string reason)
        {
            switch (action)
            {
                case "kick":
                    Admin.kickPlayer(player, target, reason, false);
                    return true;
                case "warn":
                    Admin.warnPlayer(player, target, reason);
                    return true;
                case "mute":
                    Admin.mutePlayer(player, target, duration, reason);
                    return true;
                case "unmute":
                    Admin.unmutePlayer(player, target);
                    return true;
                case "jail":
                    Admin.sendPlayerToDemorgan(player, target, duration, reason);
                    return true;
                case "ban":
                    Admin.BanPlayer(player, target, duration, reason, false);
                    return true;
                default:
                    return false;
            }
        }

        private static bool RequiresDuration(string action)
        {
            return action == "mute" || action == "jail" || action == "ban";
        }

        private static void LogPlayerAction(ExtPlayer admin, ExtPlayer target, string action)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string normalizedAction = (action ?? "unknown").ToLowerInvariant();
            string message = $"{timestamp} | {admin.Name} -> {target.Name} [{target.Character.UUID}]";

            GameLog.Admin(admin.Name, $"management:{normalizedAction}", message);
            _logger.WriteInfo($"Management action: admin={admin.Name}, target={target.Name}, action={normalizedAction}, timestamp={timestamp}");
        }

        private static void LogPunishmentAction(ExtPlayer admin, ExtPlayer target, string action, int duration, string reason)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string normalizedAction = (action ?? "unknown").ToLowerInvariant();
            string durationText = duration > 0 ? $", duration={duration}" : string.Empty;
            string actionText = $"management:punishment:{normalizedAction}({reason}{durationText})";
            string targetText = $"{target.Name} [{target.Character.UUID}]";
            string details = $"{timestamp} | admin={admin.Name} [{admin.Character.UUID}] | target={targetText} | action={normalizedAction}{durationText} | reason={reason}";

            GameLog.Admin(admin.Name, actionText, targetText);
            _logger.WriteInfo($"Management punishment: {details}");
        }

        private static List<object> LoadOnlinePlayers(List<ExtPlayer> onlinePlayers)
        {
            return onlinePlayers
                .Where(p => p?.Character != null)
                .OrderBy(p => p.Character.UUID)
                .Select(p => new
                {
                    name = p.Name,
                    uuid = p.Character.UUID,
                    adminLevel = p.Character.AdminLVL,
                    factionId = p.Character.FractionID,
                    faction = GetFractionName(p.Character.FractionID),
                    ping = p.Ping,
                    status = "ONLINE"
                })
                .Cast<object>()
                .ToList();
        }

        private static List<object> LoadSeniorStaff(List<ExtPlayer> onlinePlayers)
        {
            var rows = new List<object>();
            var seen = new HashSet<int>();
            DataTable table = MySQL.QueryRead(@"
                SELECT 
                    c.`uuid`,
                    c.`firstname`,
                    c.`lastname`,
                    c.`adminlvl`,
                    c.`createdate`,
                    au.`last_login`,
                    au.`is_active`
                FROM `characters` c
                LEFT JOIN `admin_users` au ON au.`username` = CONCAT('char:', c.`uuid`)
                WHERE c.`adminlvl` >= 8
                ORDER BY c.`adminlvl` DESC, c.`uuid` ASC
                LIMIT 500
            ");

            if (table == null)
                return rows;

            foreach (DataRow row in table.Rows)
            {
                int uuid = ReadInt(row, "uuid");
                if (uuid <= 0 || seen.Contains(uuid))
                    continue;
                seen.Add(uuid);

                var online = onlinePlayers.FirstOrDefault(p => p.Character?.UUID == uuid);
                bool authenticated = online != null && AdminAuthService.IsAuthenticated(online);
                string firstName = ReadString(row, "firstname");
                string lastName = ReadString(row, "lastname");
                string lastLogin = ReadString(row, "last_login");
                string createdAt = ReadString(row, "createdate");

                rows.Add(new
                {
                    uuid,
                    name = string.IsNullOrWhiteSpace($"{firstName}{lastName}") ? $"Senior #{uuid}" : $"{firstName}_{lastName}",
                    adminLevel = ReadInt(row, "adminlvl"),
                    rankName = GetRankName(ReadInt(row, "adminlvl")),
                    online = online != null,
                    authenticated,
                    sessionStatus = authenticated ? "AUTHENTICATED" : (online != null ? "ONLINE / LOCKED" : "OFFLINE"),
                    lastLogin = string.IsNullOrWhiteSpace(lastLogin) ? "-" : lastLogin,
                    lastSeen = online != null ? "ONLINE NOW" : (!string.IsNullOrWhiteSpace(lastLogin) ? lastLogin : createdAt),
                    adminAccountActive = ReadBool(row, "is_active")
                });
            }

            return rows;
        }

        private static List<object> LoadPunishmentHistory(int targetUuid, string targetName)
        {
            var rows = new List<object>();

            try
            {
                DataTable adminLog = MySQL.QueryRead($@"
                    SELECT `time`, `admin`, `action`, `player`
                    FROM {GameLog.DB}.adminlog
                    WHERE `player` = @prop0
                       OR `player` = @prop1
                       OR `player` LIKE @prop2
                       OR `action` LIKE @prop2
                    ORDER BY `time` DESC
                    LIMIT 80",
                    targetName ?? string.Empty,
                    $"UUID {targetUuid}",
                    $"%{targetUuid}%"
                );

                if (adminLog != null)
                {
                    foreach (DataRow row in adminLog.Rows)
                    {
                        string action = ReadString(row, "action");
                        if (!IsPunishmentLogAction(action))
                            continue;

                        rows.Add(new
                        {
                            time = ReadString(row, "time"),
                            admin = ReadString(row, "admin"),
                            action,
                            player = ReadString(row, "player"),
                            source = "adminlog"
                        });
                    }
                }

                DataTable banLog = MySQL.QueryRead($@"
                    SELECT `time`, `admin`, `player`, `until`, `reason`, `ishard`
                    FROM {GameLog.DB}.banlog
                    WHERE `player` = @prop0
                    ORDER BY `time` DESC
                    LIMIT 40",
                    targetUuid
                );

                if (banLog != null)
                {
                    foreach (DataRow row in banLog.Rows)
                    {
                        rows.Add(new
                        {
                            time = ReadString(row, "time"),
                            admin = ReadString(row, "admin"),
                            action = $"ban until {ReadString(row, "until")} / {ReadString(row, "reason")}",
                            player = targetName,
                            source = ReadBool(row, "ishard") ? "hard-banlog" : "banlog"
                        });
                    }
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"LoadPunishmentHistory({targetUuid}):\n{e}");
            }

            return rows
                .OrderByDescending(row => row.GetType().GetProperty("time")?.GetValue(row, null)?.ToString())
                .Take(80)
                .ToList();
        }

        private static List<object> LoadCharacterSearchResults(string query)
        {
            var rows = new List<object>();
            string search = (query ?? string.Empty).Trim();
            bool hasSearch = !string.IsNullOrWhiteSpace(search);
            bool numeric = int.TryParse(search, out int numericValue);
            string like = $"%{search}%";

            string sql = @"
                SELECT
                    c.`uuid`,
                    c.`firstname`,
                    c.`lastname`,
                    c.`sim`,
                    c.`lvl`,
                    c.`adminlvl`,
                    c.`fraction`,
                    c.`fractionlvl`,
                    c.`family`,
                    c.`familylvl`,
                    c.`money`,
                    c.`banknew`,
                    c.`warns`,
                    c.`owner`,
                    c.`createdate`,
                    ac.`login`
                FROM `characters` c
                LEFT JOIN `accounts` ac ON ac.`idkey` = c.`owner`
                WHERE c.`deleted` = 0";

            DataTable table;
            if (hasSearch)
            {
                sql += @"
                    AND (
                        c.`firstname` LIKE @prop0
                        OR c.`lastname` LIKE @prop0
                        OR CONCAT(c.`firstname`, '_', c.`lastname`) LIKE @prop0
                        OR c.`sim` LIKE @prop0
                        OR ac.`login` LIKE @prop0";
                if (numeric)
                    sql += " OR c.`uuid` = @prop1 OR c.`owner` = @prop1";
                sql += @")
                    ORDER BY c.`uuid` ASC
                    LIMIT 80";

                table = numeric ? MySQL.QueryRead(sql, like, numericValue) : MySQL.QueryRead(sql, like);
            }
            else
            {
                sql += " ORDER BY c.`uuid` DESC LIMIT 50";
                table = MySQL.QueryRead(sql);
            }

            if (table == null)
                return rows;

            foreach (DataRow row in table.Rows)
            {
                int uuid = ReadInt(row, "uuid");
                rows.Add(new
                {
                    uuid,
                    name = BuildCharacterName(row),
                    firstName = ReadString(row, "firstname"),
                    lastName = ReadString(row, "lastname"),
                    phone = ReadString(row, "sim"),
                    accountId = ReadInt(row, "owner"),
                    accountLogin = ReadString(row, "login"),
                    level = ReadInt(row, "lvl"),
                    adminLevel = ReadInt(row, "adminlvl"),
                    factionId = ReadInt(row, "fraction"),
                    faction = GetFractionName(ReadInt(row, "fraction")),
                    familyId = ReadInt(row, "family"),
                    family = GetFamilyNameSafe(ReadInt(row, "family")),
                    warnings = ReadInt(row, "warns"),
                    money = ReadLong(row, "money"),
                    bank = GetBankBalance(ReadInt(row, "banknew")),
                    online = Trigger.GetPlayerByUuid(uuid) != null,
                    createdAt = ReadString(row, "createdate")
                });
            }

            return rows;
        }

        private static object LoadCharacterProfile(int targetUuid)
        {
            DataTable table = MySQL.QueryRead(@"
                SELECT
                    c.`uuid`,
                    c.`firstname`,
                    c.`lastname`,
                    c.`sim`,
                    c.`lvl`,
                    c.`exp`,
                    c.`adminlvl`,
                    c.`fraction`,
                    c.`fractionlvl`,
                    c.`family`,
                    c.`familylvl`,
                    c.`money`,
                    c.`banknew`,
                    c.`warns`,
                    c.`unwarn`,
                    c.`unmutedate`,
                    c.`demorgan`,
                    c.`arrest`,
                    c.`wanted`,
                    c.`licenses`,
                    c.`mulct`,
                    c.`owner`,
                    c.`createdate`,
                    c.`imagelink`,
                    ac.`login`
                FROM `characters` c
                LEFT JOIN `accounts` ac ON ac.`idkey` = c.`owner`
                WHERE c.`deleted` = 0 AND c.`uuid` = @prop0
                LIMIT 1",
                targetUuid
            );

            if (table?.Rows == null || table.Rows.Count == 0)
                return null;

            DataRow row = table.Rows[0];
            int uuid = ReadInt(row, "uuid");
            int bankAccountId = ReadInt(row, "banknew");
            int fractionId = ReadInt(row, "fraction");
            int familyId = ReadInt(row, "family");
            var online = Trigger.GetPlayerByUuid(uuid);
            string targetName = BuildCharacterName(row);

            return new
            {
                profile = new
                {
                    uuid,
                    name = targetName,
                    firstName = ReadString(row, "firstname"),
                    lastName = ReadString(row, "lastname"),
                    accountId = ReadInt(row, "owner"),
                    accountLogin = ReadString(row, "login"),
                    phone = ReadString(row, "sim"),
                    level = ReadInt(row, "lvl"),
                    exp = ReadInt(row, "exp"),
                    adminLevel = ReadInt(row, "adminlvl"),
                    money = ReadLong(row, "money"),
                    bankAccountId,
                    bank = GetBankBalance(bankAccountId),
                    factionId = fractionId,
                    faction = GetFractionName(fractionId),
                    factionLevel = ReadInt(row, "fractionlvl"),
                    familyId,
                    family = GetFamilyNameSafe(familyId),
                    familyLevel = ReadInt(row, "familylvl"),
                    warnings = ReadInt(row, "warns"),
                    unwarn = ReadString(row, "unwarn"),
                    unmuteDate = ReadString(row, "unmutedate"),
                    demorgan = ReadInt(row, "demorgan"),
                    arrest = ReadInt(row, "arrest"),
                    wanted = ParseWantedLevel(ReadString(row, "wanted")),
                    licenses = ParseLicenses(ReadString(row, "licenses")),
                    fines = ReadInt(row, "mulct"),
                    image = ReadString(row, "imagelink"),
                    createdAt = ReadString(row, "createdate"),
                    online = online != null,
                    status = online != null ? "ONLINE" : "OFFLINE"
                },
                related = new
                {
                    vehicles = LoadCharacterVehicles(uuid),
                    houses = LoadCharacterHouses(uuid),
                    businesses = LoadCharacterBusinesses(uuid),
                    punishmentHistory = LoadPunishmentHistory(uuid, targetName),
                    activeBan = LoadActiveBan(uuid),
                    notes = new[]
                    {
                        "Profile data is read from existing characters/accounts records.",
                        "Vehicles, houses and businesses are read from existing loaded server state.",
                        "Punishment history is read from existing adminlog/banlog sources."
                    }
                }
            };
        }

        private static List<object> LoadCharacterVehicles(int uuid)
        {
            return VehicleManager.Vehicles.Values
                .Where(vehicle => vehicle != null && vehicle.OwnerType == OwnerType.Personal && vehicle.OwnerID == uuid)
                .OrderBy(vehicle => vehicle.ID)
                .Select(vehicle =>
                {
                    var personal = vehicle as PersonalBaseVehicle;
                    return new
                    {
                        id = vehicle.ID,
                        model = vehicle.ModelName,
                        name = vehicle.DisplayName,
                        number = vehicle.Number,
                        fuel = vehicle.Fuel,
                        price = vehicle.Price,
                        engineHealth = personal?.EngineHealth ?? 0,
                        mileage = personal?.Mileage ?? 0,
                        status = vehicle.Vehicle != null ? "SPAWNED" : "STORED",
                        wanted = personal?.WantedLVL?.Level ?? 0
                    };
                })
                .Cast<object>()
                .Take(80)
                .ToList();
        }

        private static List<object> LoadCharacterHouses(int uuid)
        {
            return HouseManager.Houses
                .Where(house => house != null && house.OwnerType == OwnerType.Personal && house.OwnerID == uuid)
                .OrderBy(house => house.ID)
                .Select(house => new
                {
                    id = house.ID,
                    name = house.PropertyName,
                    type = house.Type.ToString(),
                    price = house.CurrentPrice,
                    garage = house.GarageID,
                    locked = house.Locked,
                    taxBalance = house.BankModel?.Balance ?? 0
                })
                .Cast<object>()
                .Take(30)
                .ToList();
        }

        private static List<object> LoadCharacterBusinesses(int uuid)
        {
            return BusinessManager.BizList.Values
                .Where(business => business != null && business.OwnerID == uuid)
                .OrderBy(business => business.ID)
                .Select(business => new
                {
                    id = business.ID,
                    name = business.Name,
                    type = business.TypeModel?.TypeName ?? $"Type #{business.Type}",
                    address = business.Address,
                    price = business.SellPrice,
                    balance = business.BankAccountModel?.Balance ?? 0,
                    taxBalance = business.BankNalogModel?.Balance ?? 0,
                    familyPatronage = business.FamilyPatronage
                })
                .Cast<object>()
                .Take(30)
                .ToList();
        }

        private static object LoadActiveBan(int uuid)
        {
            try
            {
                var ban = Ban.Get2(uuid);
                if (ban == null || !ban.CheckDate())
                    return null;

                return new
                {
                    admin = ban.ByAdmin,
                    until = ban.Until.ToString("yyyy-MM-dd HH:mm:ss"),
                    reason = ban.Reason,
                    active = true
                };
            }
            catch
            {
                return null;
            }
        }

        private static List<object> ParseLicenses(string raw)
        {
            var licenses = new List<object>();
            if (string.IsNullOrWhiteSpace(raw))
                return licenses;

            try
            {
                JToken token = JToken.Parse(raw);
                if (token is JArray array)
                {
                    for (int index = 0; index < array.Count; index++)
                    {
                        JToken item = array[index];
                        if (item.Type == JTokenType.Boolean)
                        {
                            if (item.Value<bool>())
                                licenses.Add(new { name = $"License #{index}", status = "ACTIVE" });
                            continue;
                        }

                        string name = item["Name"]?.ToString() ?? item["name"]?.ToString() ?? item["LicenseName"]?.ToString() ?? $"License #{index}";
                        bool isActive = item["IsActive"]?.Value<bool?>() ?? item["isActive"]?.Value<bool?>() ?? true;
                        string expires = item["Date"]?.ToString() ?? item["date"]?.ToString() ?? item["EndDate"]?.ToString() ?? string.Empty;
                        licenses.Add(new { name, status = isActive ? "ACTIVE" : "INACTIVE", expires });
                    }
                }
            }
            catch
            {
                licenses.Add(new { name = "Stored license data", status = "UNPARSED", raw });
            }

            return licenses.Take(40).ToList();
        }

        private static object ParseWantedLevel(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw) || raw == "null")
                return new { level = 0, reason = "", whoGive = "", date = "" };

            try
            {
                JToken token = JToken.Parse(raw);
                return new
                {
                    level = token["Level"]?.Value<int?>() ?? token["level"]?.Value<int?>() ?? 0,
                    reason = token["Reason"]?.ToString() ?? token["reason"]?.ToString() ?? "",
                    whoGive = token["WhoGive"]?.ToString() ?? token["whoGive"]?.ToString() ?? "",
                    date = token["Date"]?.ToString() ?? token["date"]?.ToString() ?? ""
                };
            }
            catch
            {
                return new { level = 0, reason = "", whoGive = "", date = "" };
            }
        }

        private static string BuildCharacterName(DataRow row)
        {
            string firstName = ReadString(row, "firstname");
            string lastName = ReadString(row, "lastname");
            return string.IsNullOrWhiteSpace($"{firstName}{lastName}") ? $"Character #{ReadInt(row, "uuid")}" : $"{firstName}_{lastName}";
        }

        private static long GetBankBalance(int bankAccountId)
        {
            if (bankAccountId <= 0)
                return 0;

            try
            {
                return BankManager.GetAccount(bankAccountId)?.Balance ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private static string GetFamilyNameSafe(int familyId)
        {
            if (familyId <= 0)
                return "-";

            try
            {
                return FamilyManager.GetFamilyName(familyId);
            }
            catch
            {
                return $"Family #{familyId}";
            }
        }

        private static bool IsPunishmentLogAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                return false;

            string value = action.ToLowerInvariant();
            return value.Contains("kick") ||
                   value.Contains("warn") ||
                   value.Contains("mute") ||
                   value.Contains("demorgan") ||
                   value.Contains("jail") ||
                   value.Contains("ban") ||
                   value.Contains("management:punishment");
        }

        private static string GetCharacterName(int uuid)
        {
            try
            {
                DataTable table = MySQL.QueryRead("SELECT `firstname`, `lastname` FROM `characters` WHERE `uuid` = @prop0 LIMIT 1", uuid);
                if (table?.Rows == null || table.Rows.Count == 0)
                    return string.Empty;

                string firstName = ReadString(table.Rows[0], "firstname");
                string lastName = ReadString(table.Rows[0], "lastname");
                return string.IsNullOrWhiteSpace($"{firstName}{lastName}") ? string.Empty : $"{firstName}_{lastName}";
            }
            catch (Exception e)
            {
                _logger.WriteError($"GetCharacterName({uuid}):\n{e}");
                return string.Empty;
            }
        }

        private static int ReadCount(string query)
        {
            try
            {
                var table = MySQL.QueryRead(query);
                if (table?.Rows == null || table.Rows.Count == 0) return 0;
                return Convert.ToInt32(table.Rows[0][0]);
            }
            catch (Exception e)
            {
                _logger.WriteError($"ReadCount:\n{e}");
                return 0;
            }
        }

        private static int ReadInt(DataRow row, string columnName)
        {
            if (row?.Table?.Columns == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
                return 0;
            int.TryParse(row[columnName].ToString(), out int value);
            return value;
        }

        private static long ReadLong(DataRow row, string columnName)
        {
            if (row?.Table?.Columns == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
                return 0;
            long.TryParse(row[columnName].ToString(), out long value);
            return value;
        }

        private static string ReadString(DataRow row, string columnName)
        {
            if (row?.Table?.Columns == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
                return string.Empty;
            return row[columnName]?.ToString() ?? string.Empty;
        }

        private static bool ReadBool(DataRow row, string columnName)
        {
            if (row?.Table?.Columns == null || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
                return false;
            try { return Convert.ToBoolean(row[columnName]); }
            catch { return false; }
        }

        private static string GetRankName(int adminLevel)
        {
            return Group.GroupNames.Count > adminLevel ? Group.GroupNames[adminLevel] : $"Admin Level {adminLevel}";
        }

        private static string GetFractionName(int fractionId)
        {
            if (fractionId <= 0)
                return "-";

            try
            {
                return Manager.getName(fractionId);
            }
            catch
            {
                return $"Fraction #{fractionId}";
            }
        }
    }
}
