using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace GenericRepoTest
{
    [TestClass]
    public class SingleEntityTest
    {
        [TestMethod]
        public void CanAddEntity()
        {
            string name = null;
            GenericRepo<Customer> customerRepo = new GenericRepo<Customer>();
            customerRepo.Add(new Customer { Name = "Aaron" });

            using (var context = new GenericRepoContext()) {
                name = context.Customers.SingleOrDefault(x => x.Name == "Aaron")?.Name;
            }

            Assert.AreEqual("Aaron", name);
        }
    }
}
