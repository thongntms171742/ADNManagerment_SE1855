using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        private static ProductDAO? instance;
        private static readonly object instanceLock = new object();

        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new ProductDAO();
                    return instance;
                }
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products;
            try
            {
                var context = new LucyDbContext();
                products = context.Products.Include(p => p.Category).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            List<Product> products;
            try
            {
                var context = new LucyDbContext();
                products = context.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == categoryId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductById(int productId)
        {
            Product? product = null;
            try
            {
                var context = new LucyDbContext();
                product = context.Products
                    .Include(p => p.Category)
                    .SingleOrDefault(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddProduct(Product product)
        {
            try
            {
                var context = new LucyDbContext();
                context.Products.Add(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                var context = new LucyDbContext();
                var product = context.Products.SingleOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    context.Products.Remove(product);
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