using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whistler.Domain.Phone.Finder
{
    [Table("finder_messages")]
    public class FinderMessage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("match_id")]
        public int MatchId { get; set; }

        [Column("sender_character_uuid")]
        public int SenderCharacterUuid { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;
    }
}
