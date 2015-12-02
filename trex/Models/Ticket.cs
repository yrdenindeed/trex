using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trex.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public string InternalNotes { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Due { get; set; }
        public string Submitter { get; set; }
        public User AssigneeUser { get; set; }
        public string Status { get; set; }
        public TicketType TicketType { get; set; }
        public int Priority { get; set; }
        public DateTime? FirstOpened { get; set; }
        public DateTime? Deleted { get; set; }

        public Ticket MasterTicket { get; set; }
        public Organisation Organisation { get; set; }
        public Customer Customer { get; set; }

        public virtual ICollection<PermissionControl> PermissionControls { get; set; }
    }
}