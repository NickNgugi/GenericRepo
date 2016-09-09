using System;
using System.Data.Entity;

namespace ConsoleApp
{
    public class GenericRepoDb : DbContext
    {
        public GenericRepoDb()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<GenericRepoDb>());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}