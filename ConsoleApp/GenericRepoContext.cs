using System.Data.Entity;

namespace ConsoleApp
{
    public class GenericRepoContext : DbContext
    {
        public GenericRepoContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<GenericRepoContext>());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
