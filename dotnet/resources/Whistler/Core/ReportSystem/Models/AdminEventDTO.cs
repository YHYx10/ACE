namespace Whistler.Core.ReportSystem.Models
{
    class AdminEventDTO
    {
        public string name { get; set; }
        public int level { get; set; }
        public int health { get; set; }
        public int armor { get; set; }
        public int maxPlayers { get; set; }
        public int reward { get; set; }
        public string portalStart { get; set; }
        public string portalEnd { get; set; }
        public string description { get; set; }
        public long createdAt { get; set; }
        public int createdBy { get; set; }
    }
}
