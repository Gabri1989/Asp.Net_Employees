using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }             // Unique identifier for the ticket
        public string Title { get; set; }       // Title or brief description of the issue/request
        public string Description { get; set; } // Detailed description of the issue/request
        public string RequesterName { get; set; } // Name of the person who submitted the ticket
        public string RequesterEmail { get; set; } // Email of the person who submitted the ticket
        public TicketStatus Status { get; set; } // Current status of the ticket (e.g., Open, In Progress, Closed)
        public DateTime CreatedDate { get; set; } // Date and time when the ticket was created
        public DateTime? ResolvedDate { get; set; } // Date and time when the ticket was resolved (optional)
    }
    public enum TicketStatus
    {
        [Display(Name = "Open")]
        Open,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Closed")]
        Closed
    }

}
