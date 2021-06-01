using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekTarifleri.Identity;
using YemekTarifleri.Models;

namespace YemekTarifleri.Controllers
{
    [Authorize(Roles ="admin")]

    public class RoleController : Controller
    {
        private RoleManager<ApplicationRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RoleController()
        {
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new IdentityDataContext()));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }
        // GET: Role
        [HttpGet]
        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new ApplicationRole { Name = model.Name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View();
        }


        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }

        public ActionResult Delete(string id)
        {
            var role = roleManager.FindById(id);
            if (role != null)
            {
                var result = roleManager.Delete(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var role = roleManager.FindById(id);
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();
            foreach (var user in userManager.Users.ToList())
            {

                var list = userManager.IsInRole(user.Id, role.Name) ?
                    members : nonmembers;

                list.Add(user);

            }
            var model = new RoleDetailModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };


            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(RoleEditModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {

                    result = userManager.AddToRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {

                        return View("Error", result.Errors);

                    }


                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {

                    result = userManager.RemoveFromRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }



                }
                return RedirectToAction("Index", "Role");



            }
            return View("Error", new string[] { "aranılan rol yok." });

        }


    }
}