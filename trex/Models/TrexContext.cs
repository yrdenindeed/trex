using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace trex.Models
{
    public class TrexContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Email> Emails { get; set; }          
        public DbSet<PermissionControl> PermissionControls { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<Ticket> Tickets { get; set; }      
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> Users { get; set; }


    }
}