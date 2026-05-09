using System;

namespace Whistler.Core.ReportSystem.Models
{
    class AdminPenaltyDTO
    {
        public string date { get; set; }
        public string playerName { get; set; }
        public int playerId { get; set; }
        public string reason { get; set; }
        public string punishment { get; set; }
        public string adminName { get; set; }
        public long sortTime { get; set; }

        public AdminPenaltyDTO()
        {
            date = "DATE.DATE.DATE";
            playerName = "PLAYER_NAME";
            playerId = 0;
            reason = "REASON:";
            punishment = "WHAT WAS THE PUNISHMENT";
            adminName = "ADMIN_NAME";
            sortTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
