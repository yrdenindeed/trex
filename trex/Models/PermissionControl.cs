using System;
using System.ComponentModel.DataAnnotations;

namespace trex.Models
{
    public class PermissionControl
    {
        [Key]
        public Guid Id { get; set; }
        public SecurityGroup SecurityGroup  { get; set; }

        public bool AllowRead { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }

        public bool DenyRead { get; set; }
        public bool DenyWrite { get; set; }
        public bool DenyDelete { get; set; }
    }
}