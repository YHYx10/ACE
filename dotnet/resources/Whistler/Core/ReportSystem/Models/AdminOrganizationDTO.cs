namespace Whistler.Core.ReportSystem.Models
{
    class AdminOrganizationDTO
    {
        public int id { get; set; }
        public string orgName { get; set; }
        public string playerName { get; set; }
        public int playerId { get; set; }
        public string rankName { get; set; }
        public long balance { get; set; }
        public int fuelLimit { get; set; }
        public int fuelLeft { get; set; }
    }
}
