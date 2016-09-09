using System.Collections.Generic;

namespace ConsoleApp
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<Address> Addresses { get; set; }
    }
}