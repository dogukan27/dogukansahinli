using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.webui.Data;

namespace shopapp.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {      
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            this._productService=productService;
        }
        public IActionResult Index()
        {
           var BosVerileriSil=_productService.DeleteEmpty();
           foreach (var item in BosVerileriSil)
           {
                _productService.Delete(item);
           }
           var products=new ProductViewModel(){
               Products=_productService.GetProductHome()
           };

            return View(products);
        }

         // localhost:5000/home/about
        public IActionResult About()
        {
            return View();
        }

         public IActionResult Contact()
        {
            return View("MyView");
        }
    }
}