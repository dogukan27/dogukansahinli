using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCartRepository :
    EfCoreGenericRepository<Cart, ShopContext>, ICartRepository
    {
        public void DeleteFromCart(int cartId, int productId)
        {
             using (var context=new ShopContext())
            {
               var cmd=@"delete from CartItems where CartId=@p0 and ProductId=@p1";
               context.Database.ExecuteSqlRaw(cmd,cartId,productId);
               context.SaveChanges();
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context=new ShopContext())
            {
               return context.Carts.Where(i=>i.UserId==userId).Include(i=>i.CartItems).
               ThenInclude(i=>i.Product).FirstOrDefault();
            }
        }

        public void UpdateCart(Cart cart)
        {
            using (var context=new ShopContext())
            {
               context.Carts.Update(cart);
               context.SaveChanges();
            }
        }
    }
}