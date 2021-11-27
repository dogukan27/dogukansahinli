using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace shopapp.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
         CategoryManager cm = new CategoryManager(new EfCategoryDal());

        [HttpGet("[action]")]
        public IActionResult GetCategories()
        {
            return Ok(cm.List());
        }



        [HttpGet("[action]/{id}")]

        public IActionResult GetById(int id)
        {
            return Ok(cm.GetById(id));
        }



        [HttpPut("{id}")]
        //api/values/1
        public IActionResult UpdateCategory(int id,Category category)
        {
            var cat = cm.GetById(id);
            cat.Name = category.Name;
            cat.Description = category.Description;
            cat.CategoryStatus = category.CategoryStatus;
            cm.Update(cat);
            return NoContent();
        }


        [HttpDelete("{id}")]
        //api/values/1
        public IActionResult DeleteCategory(int id)
        {
            var deletedcat = cm.GetById(id);
            cm.Delete(deletedcat);
            return NoContent();
        }


        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
            cm.Insert(category);
            return Created("",category);
        }
    }
}
