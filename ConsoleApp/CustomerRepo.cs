﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class CustomerRepo : GenericRepo<Customer>, IGenericRepo<Customer>
    {
        private GenericRepo<Order> _orderRepo;

        public CustomerRepo()
        {
            _orderRepo = new GenericRepo<Order>();
        }
        public override void Update(params Customer[] items)
        {
            base.Update(items);

            List<Order> orders = new List<Order>();
            using (var context = new GenericRepoContext())
            {
                foreach (var item in items)
                    orders.AddRange(context.Orders.Where(x => x.CustomerId == item.Id));
            }

            foreach (var item in orders)
                _orderRepo.Remove(item);

            using (var context = new GenericRepoContext())
            {
                foreach (var item in items)
                {
                    context.Customers.Add(item);
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}
