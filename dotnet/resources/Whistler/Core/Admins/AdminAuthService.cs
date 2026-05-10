using System;
using System.Data;
using System.Security.Cryptography;
using GTANetworkAPI;
using Whistler.Core.ReportSystem;
using Whistler.Entities;
using Whistler.GUI;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Core.Admins
{
    public static class AdminAuthService
    {
        private const string AuthDataName = "ADMIN_AUTHENTICATED";
        private const int Pbkdf2Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const string CreateAdminUsersTableSql = @"
CREATE TABLE IF NOT EXISTS `admin_users` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(64) NOT NULL UNIQUE,
    `password_hash` TEXT NOT NULL,
    `role` VARCHAR(32) NOT NULL,
    `is_active` TINYINT(1) DEFAULT 1,
    `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP,
    `last_login` DATETIME NULL
);";
        private static readonly WhistlerLogger _logger = new WhistlerLogger(typeof(AdminAuthService));

        public static bool IsAdminCandidate(ExtPlayer player)
        {
            return player != null && player.IsLogged() && (player.Character?.AdminLVL ?? 0) >= 1;
        }

        public static bool IsAuthenticated(ExtPlayer player)
        {
            try
            {
                return IsAdminCandidate(player) &&
                       player.HasData(AuthDataName) &&
                       player.GetData<bool>(AuthDataName);
            }
            catch
            {
                return false;
            }
        }

        public static void ResetSession(ExtPlayer player, bool syncClient = true)
        {
            try
            {
                if (player == null) return;
                SafeTrigger.SetData(player, AuthDataName, false);
                if (syncClient)
                {
                    SafeTrigger.ClientEvent(player, "setadminlvl", 0);
                    player.SetSharedData("ALVL", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError($"ResetSession: {ex}");
            }
        }

        public static void Register(ExtPlayer player, string password)
        {
            try
            {
                if (!IsAdminCandidate(player))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not an administrator.", 3000);
                    return;
                }

                if (!IsPasswordValid(password))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Password must be at least 6 characters.", 4000);
                    return;
                }

                EnsureAdminUsersTable();

                string username = GetUsername(player);
                DataTable exists = MySQL.QueryRead("SELECT `id` FROM `admin_users` WHERE `username` = @prop0 LIMIT 1", username);
                if (exists != null && exists.Rows.Count > 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Already registered", 4000);
                    return;
                }

                string passwordHash = HashPassword(password);
                MySQL.Query(
                    "INSERT INTO `admin_users` (`username`, `password_hash`, `role`, `is_active`, `created_at`, `last_login`) VALUES (@prop0, @prop1, @prop2, 1, @prop3, NULL)",
                    username,
                    passwordHash,
                    player.Character.AdminLVL.ToString(),
                    MySQL.ConvertTime(DateTime.Now)
                );

                ResetSession(player);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Admin account registered. Use /alogin to unlock admin tools.", 5000);
            }
            catch (Exception ex)
            {
                _logger.WriteError($"Register: {ex}");
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Admin system initialized, please try again", 5000);
            }
        }

        public static void Login(ExtPlayer player, string password)
        {
            try
            {
                if (!IsAdminCandidate(player))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "You are not an administrator.", 3000);
                    return;
                }

                EnsureAdminUsersTable();

                string username = GetUsername(player);
                DataTable table = MySQL.QueryRead("SELECT `password_hash`, `is_active` FROM `admin_users` WHERE `username` = @prop0 LIMIT 1", username);
                if (table == null || table.Rows.Count == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Admin account is not registered. Use /aregister first.", 5000);
                    return;
                }

                DataRow row = table.Rows[0];
                bool isActive = Convert.ToBoolean(row["is_active"]);
                if (!isActive)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Admin account is disabled.", 4000);
                    return;
                }

                string passwordHash = Convert.ToString(row["password_hash"]);
                if (!VerifyPassword(password, passwordHash))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid admin password.", 3000);
                    return;
                }

                SafeTrigger.SetData(player, AuthDataName, true);
                SafeTrigger.ClientEvent(player, "setadminlvl", player.Character.AdminLVL);
                player.SetSharedData("ALVL", player.Character.AdminLVL);
                MySQL.Query("UPDATE `admin_users` SET `last_login` = @prop0, `role` = @prop1 WHERE `username` = @prop2", MySQL.ConvertTime(DateTime.Now), player.Character.AdminLVL.ToString(), username);

                ReportManager.OnAdminLoad(player);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Admin login successful.", 3000);
            }
            catch (Exception ex)
            {
                _logger.WriteError($"Login: {ex}");
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Admin login failed.", 4000);
            }
        }

        public static string GetUsername(ExtPlayer player)
        {
            return $"char:{player.Character.UUID}";
        }

        private static void EnsureAdminUsersTable()
        {
            DataTable table = MySQL.QueryRead("SHOW TABLES LIKE 'admin_users'");
            if (table != null && table.Rows.Count > 0)
                return;

            MySQL.QuerySync(CreateAdminUsersTableSql);
        }

        private static bool IsPasswordValid(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        private static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashSize);
            return $"pbkdf2${Pbkdf2Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
                return false;

            string[] parts = storedHash.Split('$');
            if (parts.Length != 4 || parts[0] != "pbkdf2")
                return false;

            if (!int.TryParse(parts[1], out int iterations))
                return false;

            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] expectedHash = Convert.FromBase64String(parts[3]);
            byte[] actualHash = Pbkdf2(password, salt, iterations, expectedHash.Length);
            return FixedTimeEquals(actualHash, expectedHash);
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int size)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return deriveBytes.GetBytes(size);
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < left.Length; i++)
                diff |= left[i] ^ right[i];
            return diff == 0;
        }
    }
}
