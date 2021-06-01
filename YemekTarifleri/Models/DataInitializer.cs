using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YemekTarifleri.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<Food> foods = new List<Food>()
            {
                new Food(){Name="Makarna",FoodLocation="Italya",Specification="Suyu kaynatın sonra makarnayı atın ve 5 dakika pişirin",IsApproved=true,IsHome=true ,CategoryId=2},
                new Food(){Name="Mangal",FoodLocation="Türkiye",Specification="Mangalı yakın etleri soslayın ve ateşte keyfinize göre pişirin",IsApproved=true,IsHome=true ,CategoryId=1}
            };
            foreach (var item in foods)
            {
                context.Foods.Add(item);
            }

            context.SaveChanges();

            List<Category> categories = new List<Category>() {
                new Category(){Name="Et Yemeği"},
                new Category(){Name="Hamur İşi"},

            };


            foreach (var item in categories)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}