using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GenericRepoTest
{
    [TestClass]
    public class DisconnectedGraphEntityTests
    {
        [TestMethod]
        public void CanAddParentAndChild()
        {
            string name = "Ben";
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer
            {
                Name = name,
                Orders = new List<Order> {
                    new Order { Name = "Order 1"},
                    new Order { Name = "Order 2"}
                }
            });

            Customer customer = new Customer();
            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == name)
                    .Include(x => x.Orders).SingleOrDefault();
            }

            Assert.AreEqual(2, customer.Orders.Count);
        }
        [TestMethod]
        public void CanUpdateParentIndependentOfChild()
        {
            string oldName = "Carol";
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer
            {
                Name = oldName,
                Orders = new List<Order> {
                    new Order { Name = "Order 5"},
                    new Order { Name = "Order 6"}
                }
            });

            string newName = "Carol1";
            Customer customer = new Customer();
            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == oldName)
                    .Include(x => x.Orders).SingleOrDefault();
            }

            customer.Name = newName;
            customerRepo.Update(customer);

            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == newName)
                    .Include(x => x.Orders).SingleOrDefault();
            }

            Assert.AreEqual(newName, customer.Name);
            Assert.AreEqual(2, customer.Orders.Count);
        }
        [TestMethod]
        public void CanUpdateParentAndUpdateChild()
        {
            string oldName = "Dan";
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer
            {
                Name = oldName,
                Orders = new List<Order> {
                    new Order { Name = "Order 10"},
                    new Order { Name = "Order 11"}
                }
            });

            string newName = "Dan1";
            Customer customer = new Customer();
            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == oldName)
                    .Include(x => x.Orders).SingleOrDefault();
            }

            customer.Name = newName;
            customer.Orders[0].Name = "Order 15";

            CustomerRepo diffRepo = new CustomerRepo();
            diffRepo.Update(customer);

            using (var context = new GenericRepoContext())
            {
                customer = context.Customers.Where(x => x.Name == newName)
                    .Include(x => x.Orders).SingleOrDefault();
            }

            Assert.AreEqual(newName, customer.Name);
            Assert.AreEqual(2, customer.Orders.Count);
            Assert.AreEqual(1, customer.Orders.Where(x => x.Name == "Order 15").ToList().Count);
        }
    }
}
