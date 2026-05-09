using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whistler.Domain.Phone.Finder
{
    [Table("finder_likes")]
    public class FinderLike
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("from_character_uuid")]
        public int FromCharacterUuid { get; set; }

        [Column("to_character_uuid")]
        public int ToCharacterUuid { get; set; }

        [Column("action")]
        public string Action { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
