using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whistler.Domain.Phone.Finder
{
    [Table("finder_profiles")]
    public class FinderProfile
    {
        [Key]
        [Column("character_uuid")]
        public int CharacterUuid { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }

        [Column("age")]
        public byte? Age { get; set; }

        [Column("gender")]
        public byte? Gender { get; set; }

        [Column("bio")]
        public string Bio { get; set; }

        [Column("headline")]
        public string Headline { get; set; }

        [Column("avatar_url")]
        public string AvatarUrl { get; set; }

        [Column("tags_json")]
        public string TagsJson { get; set; }

        [Column("looking_for_gender")]
        public byte? LookingForGender { get; set; }

        [Column("min_age")]
        public byte? MinAge { get; set; }

        [Column("max_age")]
        public byte? MaxAge { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
