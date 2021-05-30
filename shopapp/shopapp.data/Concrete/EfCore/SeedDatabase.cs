using System.Linq;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class SeedDatabase
    {
        public static void Seed(){
            var context=new ShopContext();
            if (context.Products.Count()==0)
            {
                context.Products.AddRange(Products);
            }

            if (context.Categories.Count()==0)
            {
                context.Categories.AddRange(Categories);
                context.AddRange(ProductCategories);
            }
            context.SaveChanges();
        }

        private static Product[] Products={
            new Product{Name="Samsung S3",Price=1500,ImageUrl="1.jpg",Description="Eski zamanların iyisi",IsApproved=true},
            new Product{Name="Samsung S4",Price=1800,ImageUrl="1.jpg",Description="Eski zamanların en iyisi",IsApproved=true},
            new Product{Name="Samsung S5",Price=2000,ImageUrl="1.jpg",Description="Kötünün iyisi",IsApproved=true},
            new Product{Name="Samsung S6",Price=2500,ImageUrl="1.jpg",Description="İyi",IsApproved=true},
        };

         private static Category[] Categories={
            new Category{Name="Telefon"},
            new Category{Name="Bilgisayar"},
            new Category{Name="Elektronik"}
        };

        private static ProductCategory[] ProductCategories={
            new ProductCategory{Product=Products[0],Category=Categories[0]},
            new ProductCategory{Product=Products[0],Category=Categories[2]},
            new ProductCategory{Product=Products[1],Category=Categories[0]},
            new ProductCategory{Product=Products[1],Category=Categories[2]},
            new ProductCategory{Product=Products[2],Category=Categories[0]},
            new ProductCategory{Product=Products[2],Category=Categories[2]},
            new ProductCategory{Product=Products[3],Category=Categories[0]},
            new ProductCategory{Product=Products[3],Category=Categories[2]}
        };

    }
}