using System.Collections.Generic;

namespace Whistler.Phone.Finder.Dtos
{
    public class FinderProfileDto
    {
        public int CharacterUuid { get; set; }
        public string DisplayName { get; set; }
        public byte? Age { get; set; }
        public byte? Gender { get; set; }
        public string Bio { get; set; }
        public string Headline { get; set; }
        public string AvatarUrl { get; set; }
        public List<string> Tags { get; set; }
        public byte? LookingForGender { get; set; }
        public byte? MinAge { get; set; }
        public byte? MaxAge { get; set; }
        public bool IsActive { get; set; }
        public bool Online { get; set; }
        public int? Level { get; set; }
        public string PhoneNumber { get; set; }
        public int? DistanceMeters { get; set; }
    }
}
