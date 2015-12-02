using System;
using System.Collections.Generic;

namespace trex.Models
{
    /// <summary>
    /// The company is the purchaser of trexdesk and may have several products for which they provide support.
    /// </summary>
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string MailDomain { get; set; }
        public string TrexSubdomain { get; set; }
        public string MailBoxEmailAddress { get; set; }
        public string MailBoxServer { get; set; }
        public string MailBoxUsername { get; set; }
        public string MailBoxPassword { get; set; }
        public int MailboxPort { get; set; }
        public bool MailboxSsl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public User PrimaryUser { get; set; }
        public Product DefaultProduct { get; set; }
        public DateTime? Deleted { get; set; }

        // this collection is the security groups to which users belonging to this company can possibly be assigned. it does not control access to the record directly
        public virtual ICollection<SecurityGroup> SecurityGroups { get; set; }

        public virtual ICollection<PermissionControl> PermissionControls { get; set; }
    }
}