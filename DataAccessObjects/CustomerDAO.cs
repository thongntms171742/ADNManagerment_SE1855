using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private static CustomerDAO? instance;
        private static readonly object instanceLock = new object();

        private CustomerDAO() { }

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new CustomerDAO();
                    return instance;
                }
            }
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers;
            try
            {
                var context = new LucyDbContext();
                customers = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer? customer = null;
            try
            {
                var context = new LucyDbContext();
                customer = context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                var context = new LucyDbContext();
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                var context = new LucyDbContext();
                var customer = context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
                if (customer != null)
                {
                    context.Customers.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
} 