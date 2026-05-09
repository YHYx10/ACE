namespace Whistler.Core.ReportSystem.Models
{
    class AdminTeleportDTO
    {
        public string placeName { get; set; }
        public string coords { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public uint dimension { get; set; }
    }
}
