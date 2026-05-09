using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whistler.Domain.Phone.Finder
{
    [Table("finder_matches")]
    public class FinderMatch
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("character_a_uuid")]
        public int CharacterAUuid { get; set; }

        [Column("character_b_uuid")]
        public int CharacterBUuid { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("last_message_at")]
        public DateTime? LastMessageAt { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }
}
