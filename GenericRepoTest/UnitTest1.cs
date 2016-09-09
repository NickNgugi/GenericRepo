using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace GenericRepoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanAddEntity()
        {
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer { Name = "Nick" });

            string name = null;
            using (var context = new GenericRepoDb()) {
                name = context.Customers.FirstOrDefault()?.Name;
            }

            Assert.AreEqual("Nick", name);
        }
        [TestMethod]
        public void CanAddParentChild()
        {
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer {
                Name = "Nick",
                Orders = new List<Order> {
                    new Order { Name = "Order 1"}
                }
            });

            Customer customer = new Customer();
            using (var context = new GenericRepoDb()) {
                customer = context.Customers.Include(x => x.Orders).FirstOrDefault();
            }

            Assert.AreEqual(1, customer.Orders.Count);
        }
    }
}
