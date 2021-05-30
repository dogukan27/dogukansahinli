using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Data;

namespace shopapp.webui.Controllers
{
    public class ProductController:Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            this._productService=productService;
            this._categoryService=categoryService;
        }

         [Authorize(Roles="müşteri")]
        public IActionResult Index()
        {
            var product = new Product {Name="Iphone X",Price=6000,Description="güzel telefon"};
            ViewBag.Category = "Telefonlar";

            return View(product);
        }
        public IActionResult list(int id,int page=1) 
        {
            const int pagesize=3;
            var product=_productService.GetAll();
            //Kategori filtreleme
            if (id==0 || id==1 || id==2 || id==3)
            {
                product=_productService.GetProductsByCategory(id,page,pagesize);
                int kategoriyegoreurunsayisi=_productService.GetPageCountByCategory(id);
                ViewBag.sayfasayisi=Math.Ceiling((decimal)kategoriyegoreurunsayisi/pagesize);
                ViewBag.kategoriid=id;
            }
           var products=new ProductViewModel(){
               Products=product
           };
           
           
           


            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.procateg=_productService.GetProductDetails(id).ProductCategories.Select(i=>i.Category).ToList();
            return View(_productService.GetById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {           
            ViewBag.Categories = new SelectList(_categoryService.GetAll(),"CategoryId","Name");
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {                       
            if (ModelState.IsValid)
            {
                    _productService.Create(p);
                    return RedirectToAction("list"); 
            }

            return View(p);            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(),"CategoryId","Name");
            return View(_productService.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            _productService.Update(p);
            return RedirectToAction("list");
        }
  
        [HttpPost]
        public IActionResult Delete(int ProductId)
        {
            var product=_productService.GetById(ProductId);
            _productService.Delete(product);
            return RedirectToAction("list");
        }

        public IActionResult Search(string q){
            var product=_productService.GetAll();
             // search butonu
             if (q!=null)
            {
                 product=_productService.GetAll().Where(p => p.Name.ToLower().Contains(q.ToLower())).ToList();
            }
            var products=new ProductViewModel(){
               Products=product
           };
            return View(products);
        }
        
    }
}