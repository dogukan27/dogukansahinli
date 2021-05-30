using System.Collections.Generic;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.business.Concreate
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this._productRepository=productRepository;
        }
        public void Create(Product entity)
        {
            //iş kuralları uygula
            _productRepository.Create(entity);
        }
        public List<Product> GetAll()
        {
            //iş kuralları uygula
            return _productRepository.GetAll();
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public Product GetProductDetails(int id)
        {
            return _productRepository.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(int id,int page, int pagesize)
        {
            return _productRepository.GetProductsByCategory(id,page,pagesize);
        }

        public int GetPageCountByCategory(int id)
        {
            return _productRepository.GetPageCountByCategory(id);
        }

        public List<Product> GetProductHome()
        {
            return _productRepository.GetProductHome();
        }

        public List<Product> DeleteEmpty()
        {
            return _productRepository.DeleteEmpty();
        }

        public void ChangesHome(int id)
        {
             _productRepository.ChangesHome(id);
        }

        public void ChangesOnay(int id)
        {
            _productRepository.ChangesOnay(id);
        }
    }
}