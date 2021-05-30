using System.Collections.Generic;
using shopapp.entity;
namespace shopapp.business.Abstract
{
    public interface IProductService
    {
        Product GetProductDetails(int id);
        Product GetById(int id);

        List<Product> GetProductsByCategory(int id,int page, int pagesize);


        List<Product> GetAll();

        void Create(Product entity);

        void Update(Product entity);
        void Delete(Product entity);
        int GetPageCountByCategory(int id);
        List<Product> GetProductHome();
        List<Product> DeleteEmpty();
        void ChangesHome(int id);
        void ChangesOnay(int id);
    }
}