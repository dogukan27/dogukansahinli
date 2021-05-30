using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Data;

namespace shopapp.webui.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
         private ICategoryService _categoryService;
         public CategoriesViewComponent(ICategoryService categoryService)
         {
            this._categoryService= categoryService;
         }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString()=="list")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_categoryService.GetAll());
        }
    }
}