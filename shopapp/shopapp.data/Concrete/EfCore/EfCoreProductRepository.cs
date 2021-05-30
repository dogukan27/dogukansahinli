using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreProductRepository : 
        EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public void ChangesHome(int id)
        {
            using (var context=new ShopContext())
            {
               var urun=context.Products.Where(i=>i.ProductId==id).FirstOrDefault();
               if (urun.IsHome)
               {
                   urun.IsHome=false;
                   context.SaveChanges();
               } 
               else
               {
                   urun.IsHome=true;
                   context.SaveChanges();
               }
               
            }
        }

        public void ChangesOnay(int id)
        {
            using (var context=new ShopContext())
            {
               var urun=context.Products.Where(i=>i.ProductId==id).FirstOrDefault();
               if (urun.IsApproved)
               {
                   urun.IsApproved=false;
                   context.SaveChanges();
               } 
               else
               {
                   urun.IsApproved=true;
                   context.SaveChanges();
               }
               
            }
        }

        public List<Product> DeleteEmpty()
        {
            using(var context = new ShopContext())
            {
               var liste=context.Products.Where(p=>p.Name==null).ToList();
               return liste;
               
            }
        }

        public int GetPageCountByCategory(int id)
        {
            using(var context = new ShopContext())
            {
                var products=context.Products.AsQueryable();
            if (id==0)
            {
                
                products=context.Products;
                
            }
             if (id==1 || id==2 || id==3)
            {
                    products=products.Include(c=>c.ProductCategories).ThenInclude(i=>i.Category)
                    .Where(i=>i.ProductCategories.Any(a=>a.Category.CategoryId==id));
            }
            return products.Where(p=>p.Name!=null).Count();
            }
        }

        public List<Product> GetPopularProducts()
        {
            using(var context = new ShopContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductDetails(int id)
        {
            using(var context = new ShopContext())
            {
                return context.Products.Where(i=>i.ProductId==id).
                Include(c=>c.ProductCategories).ThenInclude(b=>b.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProductHome()
        {
             using(var context = new ShopContext())
            {
                var products=context.Products.Where(i=>i.IsHome==true).ToList();
                return products;
            }
        }

        public List<Product> GetProductsByCategory(int id,int page, int pagesize)
        {
            using(var context = new ShopContext())
            {
                var products=context.Products.AsQueryable();
            if (id==0)
            {
                
                products=context.Products;
                
            }
             if (id==1 || id==2 || id==3)
            {
                    products=products.Include(c=>c.ProductCategories).ThenInclude(i=>i.Category)
                    .Where(i=>i.ProductCategories.Any(a=>a.Category.CategoryId==id));
            }
            return products.Skip((page-1)*pagesize).Take(pagesize).ToList();
            }
        }
    }
}        

    