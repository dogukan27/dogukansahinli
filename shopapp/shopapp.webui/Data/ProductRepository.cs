using System.Collections.Generic;
using System.Linq;
using shopapp.entity;


namespace shopapp.webui.Data
{
    public static class ProductRepository
    {
        private static List<Product> _products = null;


        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public static Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

       

        public static void DeleteProduct(int id)
        {
            var product = GetProductById(id);

            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}