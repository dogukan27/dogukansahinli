using System.Collections.Generic;
using System.Linq;
using shopapp.entity;

namespace shopapp.webui.Data
{
    public class CategoryRepository
    {
        private static List<Category> _categories=null;


        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public static void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public static Category GetCategorybyId(int id)
        {
            return _categories.FirstOrDefault(c=>c.CategoryId==id);
        }



    }
}