using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace trex.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Email is the username
        /// </summary>
        public string Email { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsHelpdesk { get; set; }

        public Company Company { get; set; }
        public ICollection<Ticket> Tickets{ get; set; }

        /// <summary>
        /// membership
        /// </summary>
        public ICollection<SecurityGroup> SecurityGroups{ get; set; }

        /// <summary>
        /// permission to edit this user. this user does not necessarily have full permission to his own record
        /// </summary>
        public virtual ICollection<PermissionControl> PermissionControls { get; set; }
        
    }
}