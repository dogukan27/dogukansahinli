using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Identity;
using shopapp.webui.Models;
using shopapp.webui.ViewModels;

namespace shopapp.webui.Controllers
{
    [Authorize(Roles="admin")]

    public class AdminController:Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AdminController(IProductService productService,ICategoryService categoryService,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            this._productService=productService;
            this._categoryService=categoryService;
            this._roleManager=roleManager;
            this._userManager=userManager;
        }

        public IActionResult UserList(){
            return View(_userManager.Users);
        }

        public async Task<IActionResult> UserEdit(string id){
            var user=await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var selectedroles=await _userManager.GetRolesAsync(user);
                var allroles=_roleManager.Roles.Select(i=>i.Name);
                ViewBag.AllRoles=allroles;
                var model=new UserDetailModel(){
                    UserId=user.Id,
                    UserName=user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    EmailConfirmed=user.EmailConfirmed,
                    SelectedRoles=selectedroles
                };
                return View(model);
            }
            return RedirectToAction("UserList");

        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model,string[] seciliroller){
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByIdAsync(model.UserId);
                if (user!=null)
                {
                    user.FirstName=model.FirstName;
                    user.LastName=model.LastName;
                    user.UserName=model.UserName;
                    user.Email=model.Email;
                    user.EmailConfirmed=model.EmailConfirmed;
                }
                var result=await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles=await _userManager.GetRolesAsync(user);
                    if (seciliroller==null)
                    {
                        seciliroller=new string[]{};
                    }
                    await _userManager.AddToRolesAsync(user,seciliroller.Except(userRoles).ToArray<string>());
                    await _userManager.RemoveFromRolesAsync(user,userRoles.Except(seciliroller).ToArray<string>());
                    return RedirectToAction("UserList");
                }
                return View(model);
                
            }
            return View(model);
        }

        public async Task<IActionResult> RoleEdit(string id){
            var role=await _roleManager.FindByIdAsync(id);
            var members=new List<User>();
            var nonmembers=new List<User>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    members.Add(user);
                }
                else
                {
                    nonmembers.Add(user);
                }
            }
            var model=new RoleDetailModel(){
                Role=role,
                Members=members,
                NonMembers=nonmembers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model){
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[]{})
                {
                    var user=await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
                        var result=await _userManager.AddToRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[]{})
                {
                    var user=await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
                        var result=await _userManager.RemoveFromRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("RoleEdit");
        }

        public IActionResult RoleList(){
            return View(_roleManager.Roles);
        }

        public IActionResult RoleCreate(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model){
            if (ModelState.IsValid)
            {
                //Sayfadan girilen modeli ekleme
                var result=await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
            }
            return View(model);
        }

        public IActionResult ProductList(){
            var urunler=_productService.GetAll();
            var products=new ProductViewModel(){
                Products=urunler
            };
            return View(products);
        }

         [HttpGet]
        public IActionResult Create()
        {           
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {                       
            if (ModelState.IsValid)
            {
                    _productService.Create(p);
                    TempData["message"]=p.Name+" ürünü eklendi";
                    return RedirectToAction("ProductList"); 
            }

            return View(p);            
        }

         [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.procateg=_productService.GetProductDetails(id).ProductCategories.Select(i=>i.Category).ToList();
            ViewBag.Categories = _categoryService.GetAll();
            return View(_productService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p,IFormFile file)
        {
            if (file!=null)
            {
                var resimturu=Path.GetExtension(file.FileName);
                var randomName=string.Format(Guid.NewGuid()+resimturu);
                p.ImageUrl=randomName;
                var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot//img",randomName);
                 using (var stream=new FileStream(path,FileMode.Create))
                {
                await file.CopyToAsync(stream); 
                }
            }
             _productService.Update(p);
            TempData["message"]=p.Name+" ürünü güncellendi";
            return RedirectToAction("ProductList");
            
        }

        public IActionResult Delete(int id){
            var product=_productService.GetById(id);
            _productService.Delete(product);
            TempData["message"]=product.Name+" ürünü silindi";
            return RedirectToAction("ProductList");
        }


         [HttpGet]
        public IActionResult CategoryCreate()
        {           
            return View(new Category());
        }

        [HttpPost]
        public IActionResult CategoryCreate(Category c)
        {                       
            if (ModelState.IsValid)
            {
                    _categoryService.Create(c);
                    TempData["message"]=c.Name+" kategorisi eklendi";
                    return RedirectToAction("CategoryList");
            }

            return View(c);            
        }

         public IActionResult CategoryList(){
            var kategoriler=_categoryService.GetAll();
            var categories=new CategoryViewModel(){
                Categories=kategoriler
            };
            return View(categories);
        }

        public IActionResult CategoryDelete(int id){
             var category=_categoryService.GetById(id);
            _categoryService.Delete(category);
            TempData["message"]=category.Name+" kategorisi silindi";
            return RedirectToAction("CategoryList");
        }


         [HttpGet]
        public IActionResult CategoryEdit(int id)
        {
            ViewBag.productlist=_categoryService.GetProductsWithCategory(id).ProductCategories.Select(p=>p.Product).ToList();
            return View(_categoryService.GetById(id));
        }

        [HttpPost]
        public IActionResult CategoryEdit(Category c)
        {
            _categoryService.Update(c);
            TempData["message"]=c.Name+" kategorisi güncellendi";
            

            
            
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult CategoryProductDelete(int productId,int categoryId){
            _categoryService.DeleteFromCategory(productId,categoryId);
            return Redirect("/admin/categories/edit/"+categoryId);
        }

         [HttpPost]
        public IActionResult ProductCategoryDelete(int productId,int categoryId){
            _categoryService.DeleteFromCategory(productId,categoryId);
            return Redirect("/admin/edit/"+productId);
        }

        [HttpPost]
        public IActionResult AddProductCategory(int productId,int categoryId){

            _categoryService.AddProductCategory(productId,categoryId);
            return Redirect("/admin/edit/"+productId);
        }

        public IActionResult ChangesHome(int id){
            _productService.ChangesHome(id);
            return RedirectToAction("ProductList");
        }

        public IActionResult ChangesOnay(int id){
            _productService.ChangesOnay(id);
            return RedirectToAction("ProductList");
        }



    }
}