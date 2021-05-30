using System.Collections.Generic;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
       List<Product> GetProductsByCategory(int id,int page,int pagesize);
       Product GetProductDetails(int id); 
       List<Product> GetPopularProducts();
        int GetPageCountByCategory(int id);
        List<Product> GetProductHome();
        List<Product> DeleteEmpty();
        void ChangesHome(int id);
        void ChangesOnay(int id);
    }
}