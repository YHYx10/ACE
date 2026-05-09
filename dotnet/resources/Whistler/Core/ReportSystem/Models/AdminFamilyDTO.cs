namespace Whistler.Core.ReportSystem.Models
{
    class AdminFamilyDTO
    {
        public int id { get; set; }
        public string familyName { get; set; }
        public string leaderName { get; set; }
        public int leaderId { get; set; }
        public long bank { get; set; }
        public int houseNumber { get; set; }
        public string status { get; set; }
    }
}
