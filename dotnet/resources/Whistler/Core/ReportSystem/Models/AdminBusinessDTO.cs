namespace Whistler.Core.ReportSystem.Models
{
    class AdminBusinessDTO
    {
        public int id { get; set; }
        public string businessName { get; set; }
        public string ownerName { get; set; }
        public int ownerId { get; set; }
        public int businessNumber { get; set; }
        public long balance { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public uint dimension { get; set; }
    }
}
