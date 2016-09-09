using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GenericRepoTest
{
    [TestClass]
    public class GraphEntitiesTest
    {
        [TestMethod]
        public void CanAddParentChild()
        {
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer
            {
                Name = "Mark",
                Orders = new List<Order> {
                    new Order { Name = "Order 1"},
                    new Order { Name = "Order 2"}
                }
            });

            Customer customer = new Customer();
            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == "Mark")
                    .Include(x => x.Orders).SingleOrDefault();
            }

            Assert.AreEqual(2, customer.Orders.Count);
        }
        [TestMethod]
        public void CanUpdateParentIndependentOfChild()
        {
            Assert.AreEqual(2, 1);
        }
    }
}
