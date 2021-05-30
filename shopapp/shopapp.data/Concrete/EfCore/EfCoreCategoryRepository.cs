using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        private ShopContext db=new ShopContext();

        public void AddProductCategory(int productId, int categoryId)
        {
            using (ShopContext context=new ShopContext())
            {
                try
                {
                     var sql="insert into productcategory (CategoryId,ProductId) values(@p0,@p1)";
                    context.Database.ExecuteSqlRaw(sql,categoryId,productId);
                    context.SaveChanges();
                }
                catch (SqliteException)
                {
                }
                
            }
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
             using (ShopContext context=new ShopContext())
            {
                 var sql="delete from productcategory where CategoryId=@p0 and ProductId=@p1";
                 context.Database.ExecuteSqlRaw(sql,categoryId,productId);
                 context.SaveChanges();
            }
        }

        public List<Category> GetPopularCategories()
        {
             return db.Categories.ToList();
        }

        public Category GetProductsWithCategory(int id)
        {
            using (ShopContext context=new ShopContext())
            {
                 return context.Categories.Where(i=>i.CategoryId==id).
                Include(c=>c.ProductCategories).ThenInclude(b=>b.Product).FirstOrDefault();
            }
           
        }
    }
}