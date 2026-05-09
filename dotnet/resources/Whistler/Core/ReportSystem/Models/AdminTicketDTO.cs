using System;
using System.Collections.Generic;
using System.Linq;
using Whistler.Entities;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Core.ReportSystem.Models
{
    // DTO for ASTRO AdminPanel (Player Support tab)
    class AdminTicketDTO
    {
        public int id { get; set; }
        public int playerId { get; set; }
        public string playerName { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string status { get; set; } // open / assigned / closed
        public int? assignedToId { get; set; } // not always available
        public string assignedToName { get; set; }
        public long createdAt { get; set; }
        public string note { get; set; }
        public int playerLevel { get; set; }
        public List<AdminTicketMessageDTO> messages { get; set; }

        public AdminTicketDTO(Report report)
        {
            ExtPlayer target = Trigger.GetPlayerByUuid(report.AuthorUUID);
            id = report.ID;
            playerId = report.AuthorApiID;
            playerName = (report.Author ?? "Unknown").Replace('_', ' ');
            subject = "Contact Administration";
            message = (report.Answers != null && report.Answers.Count > 0) ? (report.Answers[0].Message ?? "") : "";
            status = report.Closed ? "closed" : (string.IsNullOrEmpty(report.AdminName) ? "open" : "assigned");
            assignedToId = null; // we store UUID in report.AdminUUID; remoteId is not available reliably
            assignedToName = string.IsNullOrEmpty(report.AdminName) ? null : report.AdminName.Replace('_', ' ');
            createdAt = new DateTimeOffset(report.OpenedDate).ToUnixTimeMilliseconds();
            note = report.InternalNote ?? "";
            playerLevel = target?.Character?.LVL ?? (Main.PlayerSlotsInfo.ContainsKey(report.AuthorUUID) ? Main.PlayerSlotsInfo[report.AuthorUUID].Lvl : 0);
            messages = report.Answers?
                .Select(answer => new AdminTicketMessageDTO(answer, report.Author, report.OpenedDate))
                .ToList() ?? new List<AdminTicketMessageDTO>();
        }
    }

    class AdminTicketMessageDTO
    {
        public int id { get; set; }
        public string sender { get; set; }
        public string text { get; set; }
        public long createdAt { get; set; }
        public bool isPlayer { get; set; }

        public AdminTicketMessageDTO(ReportAnswer answer, string reportAuthor, DateTime fallbackDate)
        {
            id = answer.ID;
            sender = (answer.Sender ?? "Unknown").Replace('_', ' ');
            text = answer.Message ?? string.Empty;
            createdAt = new DateTimeOffset(fallbackDate).ToUnixTimeMilliseconds();
            isPlayer = string.Equals(answer.Sender, reportAuthor, StringComparison.OrdinalIgnoreCase);
        }
    }
}
