using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whistler.Domain.Phone.Finder;
using Whistler.Entities;
using Whistler.Helpers;
using Whistler.Infrastructure.DataAccess;
using Whistler.Phone.Finder.Dtos;
using Whistler.SDK;

namespace Whistler.Phone.Finder
{
    internal class FinderHandler : Script
    {
        private static WhistlerLogger _logger = new WhistlerLogger(typeof(FinderHandler));
        private const int DiscoverLimit = 24;

        [RemoteEvent("phone:finder:loadProfile")]
        public async Task LoadProfile(ExtPlayer player)
        {
            try
            {
                if (!IsValidPlayer(player)) return;

                using (var context = DbManager.TemporaryContext)
                {
                    var profile = await context.FinderProfiles.FindAsync(player.Character.UUID);
                    SendProfile(player, profile);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"Unhandled exception catched on phone:finder:loadProfile ({player?.Name}) - " + e);
                SendError(player, "profile_load_failed");
            }
        }

        [RemoteEvent("phone:finder:saveProfile")]
        public async Task SaveProfile(ExtPlayer player, string profileJson)
        {
            try
            {
                if (!IsValidPlayer(player)) return;

                var dto = JsonConvert.DeserializeObject<FinderProfileSaveDto>(profileJson ?? "{}");
                if (dto == null)
                {
                    SendError(player, "invalid_profile_payload");
                    return;
                }

                using (var context = DbManager.TemporaryContext)
                {
                    var uuid = player.Character.UUID;
                    var profile = await context.FinderProfiles.FindAsync(uuid);
                    var now = DateTime.Now;

                    if (profile == null)
                    {
                        profile = new FinderProfile
                        {
                            CharacterUuid = uuid,
                            CreatedAt = now
                        };
                        context.FinderProfiles.Add(profile);
                    }

                    ApplyProfileData(player, profile, dto, now);
                    await context.SaveChangesAsync();

                    SendProfile(player, profile);
                    await SendDiscoverProfiles(player, context, uuid);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"Unhandled exception catched on phone:finder:saveProfile ({player?.Name}) - " + e);
                SendError(player, "profile_save_failed");
            }
        }

        [RemoteEvent("phone:finder:loadProfiles")]
        public async Task LoadProfiles(ExtPlayer player)
        {
            try
            {
                if (!IsValidPlayer(player)) return;

                var uuid = player.Character.UUID;

                using (var context = DbManager.TemporaryContext)
                {
                    var currentProfile = await context.FinderProfiles.FindAsync(uuid);
                    SendProfile(player, currentProfile);

                    await SendDiscoverProfiles(player, context, uuid, currentProfile);
                }
            }
            catch (Exception e)
            {
                _logger.WriteError($"Unhandled exception catched on phone:finder:loadProfiles ({player?.Name}) - " + e);
                SendError(player, "profiles_load_failed");
            }
        }

        private static bool IsValidPlayer(ExtPlayer player)
        {
            return player != null && player.IsLogged() && player.Character != null;
        }

        private static async Task SendDiscoverProfiles(ExtPlayer player, ServerContext context, int uuid, FinderProfile currentProfile = null)
        {
            currentProfile ??= await context.FinderProfiles.FindAsync(uuid);

            if (currentProfile == null || !currentProfile.IsActive)
            {
                SendProfiles(player, new List<FinderProfileDto>());
                return;
            }

            var excludedUuids = await context.FinderLikes
                .Where(l => l.FromCharacterUuid == uuid)
                .Select(l => l.ToCharacterUuid)
                .ToListAsync();

            excludedUuids.Add(uuid);

            var profiles = await context.FinderProfiles
                .Where(p => p.IsActive && !excludedUuids.Contains(p.CharacterUuid))
                .OrderByDescending(p => p.UpdatedAt)
                .Take(DiscoverLimit)
                .ToListAsync();

            SendProfiles(player, profiles.Select(ToDto).ToList());
        }

        private static void ApplyProfileData(ExtPlayer player, FinderProfile profile, FinderProfileSaveDto dto, DateTime now)
        {
            var fallbackName = $"{player.Character.FirstName} {player.Character.LastName}";

            profile.DisplayName = ClampText(string.IsNullOrWhiteSpace(dto.DisplayName) ? fallbackName : dto.DisplayName, 64);
            profile.Age = ClampAge(dto.Age);
            profile.Gender = dto.Gender;
            profile.Bio = ClampText(dto.Bio, 500);
            profile.Headline = ClampText(dto.Headline, 80);
            profile.AvatarUrl = ClampText(dto.AvatarUrl, 512);
            profile.TagsJson = JsonConvert.SerializeObject((dto.Tags ?? new List<string>()).Where(t => !string.IsNullOrWhiteSpace(t)).Take(8).Select(t => ClampText(t, 24)).ToList());
            profile.LookingForGender = dto.LookingForGender;
            profile.MinAge = ClampAge(dto.MinAge);
            profile.MaxAge = ClampAge(dto.MaxAge);
            profile.IsActive = dto.IsActive;
            profile.UpdatedAt = now;
        }

        private static byte? ClampAge(byte? value)
        {
            if (value == null) return null;
            if (value < 18) return 18;
            if (value > 99) return 99;
            return value;
        }

        private static string ClampText(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            value = value.Trim();
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        private static FinderProfileDto ToDto(FinderProfile profile)
        {
            if (profile == null) return null;

            List<string> tags = new List<string>();
            try
            {
                tags = JsonConvert.DeserializeObject<List<string>>(profile.TagsJson ?? "[]") ?? new List<string>();
            }
            catch
            {
                tags = new List<string>();
            }

            var onlinePlayer = Main.GetExtPlayerByPredicate(p => p.Character != null && p.Character.UUID == profile.CharacterUuid);
            var character = onlinePlayer?.Character;

            return new FinderProfileDto
            {
                CharacterUuid = profile.CharacterUuid,
                DisplayName = profile.DisplayName,
                Age = profile.Age,
                Gender = profile.Gender,
                Bio = profile.Bio,
                Headline = profile.Headline,
                AvatarUrl = profile.AvatarUrl,
                Tags = tags,
                LookingForGender = profile.LookingForGender,
                MinAge = profile.MinAge,
                MaxAge = profile.MaxAge,
                IsActive = profile.IsActive,
                Online = onlinePlayer != null,
                Level = character?.LVL,
                PhoneNumber = character?.PhoneTemporary?.Phone?.SimCard?.Number.ToString(),
                DistanceMeters = null
            };
        }

        private static void SendProfile(ExtPlayer player, FinderProfile profile)
        {
            player.TriggerCefEvent("smartphone/finderPage/setProfile", JsonConvert.SerializeObject(ToDto(profile)));
        }

        private static void SendProfiles(ExtPlayer player, List<FinderProfileDto> profiles)
        {
            player.TriggerCefEvent("smartphone/finderPage/setProfiles", JsonConvert.SerializeObject(profiles));
        }

        private static void SendError(ExtPlayer player, string message)
        {
            player?.TriggerCefEvent("smartphone/finderPage/setError", JsonConvert.SerializeObject(message));
        }
    }
}
