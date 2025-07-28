using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO? instance;
        private static readonly object instanceLock = new object();

        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new OrderDetailDAO();
                    return instance;
                }
            }
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            List<OrderDetail> orderDetails;
            try
            {
                var context = new LucyDbContext();
                orderDetails = context.OrderDetails
                    .Include(od => od.Product)
                        .ThenInclude(p => p.Category)
                    .Where(od => od.OrderId == orderId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            OrderDetail? orderDetail = null;
            try
            {
                var context = new LucyDbContext();
                orderDetail = context.OrderDetails
                    .Include(od => od.Product)
                    .Include(od => od.Order)
                    .SingleOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var context = new LucyDbContext();
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(orderDetail).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(int orderId, int productId)
        {
            try
            {
                var context = new LucyDbContext();
                var orderDetail = context.OrderDetails.SingleOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
                if (orderDetail != null)
                {
                    context.OrderDetails.Remove(orderDetail);
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