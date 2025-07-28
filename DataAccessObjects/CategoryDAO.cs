using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        private static CategoryDAO? instance;
        private static readonly object instanceLock = new object();

        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new CategoryDAO();
                    return instance;
                }
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories;
            try
            {
                var context = new LucyDbContext();
                categories = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category? category = null;
            try
            {
                var context = new LucyDbContext();
                category = context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public void AddCategory(Category category)
        {
            try
            {
                var context = new LucyDbContext();
                context.Categories.Add(category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                var context = new LucyDbContext();
                var category = context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (category != null)
                {
                    context.Categories.Remove(category);
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