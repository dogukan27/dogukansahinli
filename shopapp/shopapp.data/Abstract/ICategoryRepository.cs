using System.Collections.Generic;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        List<Category> GetPopularCategories();
        Category GetProductsWithCategory(int id);
        void DeleteFromCategory(int productId, int categoryId);
        void AddProductCategory(int productId, int categoryId);
    }
}