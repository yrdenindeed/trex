using System;
using System.ComponentModel.DataAnnotations;

namespace trex.Models
{
    public class Email
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsSent { get; set; }

        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Sent { get; set; }
    }
}