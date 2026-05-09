using GTANetworkAPI;
using Whistler.SDK;

namespace Whistler.Phone.Finder
{
    internal class FinderSchema : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            MySQL.Query(@"CREATE TABLE IF NOT EXISTS `finder_profiles` (
                `character_uuid` INT NOT NULL,
                `display_name` VARCHAR(64) NOT NULL,
                `age` TINYINT UNSIGNED NULL,
                `gender` TINYINT NULL,
                `bio` VARCHAR(500) NULL,
                `headline` VARCHAR(80) NULL,
                `avatar_url` VARCHAR(512) NULL,
                `tags_json` TEXT NULL,
                `looking_for_gender` TINYINT NULL,
                `min_age` TINYINT UNSIGNED NULL,
                `max_age` TINYINT UNSIGNED NULL,
                `is_active` TINYINT(1) NOT NULL DEFAULT 1,
                `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                PRIMARY KEY (`character_uuid`),
                INDEX `idx_finder_profiles_active` (`is_active`),
                INDEX `idx_finder_profiles_gender_age` (`gender`, `age`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            MySQL.Query(@"CREATE TABLE IF NOT EXISTS `finder_likes` (
                `id` INT NOT NULL AUTO_INCREMENT,
                `from_character_uuid` INT NOT NULL,
                `to_character_uuid` INT NOT NULL,
                `action` ENUM('like', 'pass') NOT NULL,
                `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                PRIMARY KEY (`id`),
                UNIQUE KEY `uq_finder_like_pair` (`from_character_uuid`, `to_character_uuid`),
                INDEX `idx_finder_likes_to_action` (`to_character_uuid`, `action`),
                INDEX `idx_finder_likes_from_action` (`from_character_uuid`, `action`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            MySQL.Query(@"CREATE TABLE IF NOT EXISTS `finder_matches` (
                `id` INT NOT NULL AUTO_INCREMENT,
                `character_a_uuid` INT NOT NULL,
                `character_b_uuid` INT NOT NULL,
                `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                `last_message_at` DATETIME NULL,
                `is_active` TINYINT(1) NOT NULL DEFAULT 1,
                PRIMARY KEY (`id`),
                UNIQUE KEY `uq_finder_match_pair` (`character_a_uuid`, `character_b_uuid`),
                INDEX `idx_finder_matches_a` (`character_a_uuid`, `is_active`),
                INDEX `idx_finder_matches_b` (`character_b_uuid`, `is_active`),
                INDEX `idx_finder_matches_last_message` (`last_message_at`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            MySQL.Query(@"CREATE TABLE IF NOT EXISTS `finder_messages` (
                `id` INT NOT NULL AUTO_INCREMENT,
                `match_id` INT NOT NULL,
                `sender_character_uuid` INT NOT NULL,
                `message` VARCHAR(1000) NOT NULL,
                `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                `is_read` TINYINT(1) NOT NULL DEFAULT 0,
                PRIMARY KEY (`id`),
                INDEX `idx_finder_messages_match_created` (`match_id`, `created_at`),
                INDEX `idx_finder_messages_sender` (`sender_character_uuid`)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");
        }
    }
}
