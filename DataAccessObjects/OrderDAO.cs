using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        private static OrderDAO? instance;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new OrderDAO();
                    return instance;
                }
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> orders;
            try
            {
                var context = new LucyDbContext();
                orders = context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> orders;
            try
            {
                var context = new LucyDbContext();
                orders = context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .Where(o => o.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            Order? order = null;
            try
            {
                var context = new LucyDbContext();
                order = context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                            .ThenInclude(p => p.Category)
                    .SingleOrDefault(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            try
            {
                var context = new LucyDbContext();
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                var context = new LucyDbContext();
                
                // First delete all order details
                var orderDetails = context.OrderDetails.Where(od => od.OrderId == orderId);
                context.OrderDetails.RemoveRange(orderDetails);
                
                // Then delete the order
                var order = context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    context.Orders.Remove(order);
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