using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [Authorize]
    public class CartController:Controller
    {
        private ICartService _cartService;  
        private UserManager<User> _userManager;

        public CartController(ICartService cartService,UserManager<User> userManager)
        {
            this._cartService=cartService;
            this._userManager=userManager;
        }
        public IActionResult Index(){
            var cart=_cartService.GetCartByUserId(_userManager.GetUserId(User));
            var model=new CartModel(){
                CartId=cart.Id,
                CartItems=cart.CartItems.Select(i=>new CartItemModel(){
                    CartItemId=i.Id,
                    ProductId=i.ProductId,
                    Name=i.Product.Name,
                    Price=i.Product.Price,
                    ImageUrl=i.Product.ImageUrl,
                    Quantity=i.Quantity

                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId,int quantity){
            var userId=_userManager.GetUserId(User);
            if (userId!=null)
            {
                _cartService.AddToCart(userId,productId,quantity);
            }
            return RedirectToAction("Index","Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId=_userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId,productId);
            return RedirectToAction("Index","Cart");
        }
        
        [HttpGet]
        public IActionResult Checkout(){
              var cart=_cartService.GetCartByUserId(_userManager.GetUserId(User));
              var orderModel=new OrderModel();
              orderModel.CartModel=new CartModel(){
                    CartId=cart.Id,
                    CartItems=cart.CartItems.Select(i=>new CartItemModel(){
                    CartItemId=i.Id,
                    ProductId=i.ProductId,
                    Name=i.Product.Name,
                    Price=i.Product.Price,
                    ImageUrl=i.Product.ImageUrl,
                    Quantity=i.Quantity

                }).ToList()
            };
            return View(orderModel);
        }

        [HttpPost]

        public IActionResult Checkout(OrderModel model)
        {
            return View();
        }
    }

}