using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ICartRepository:IRepository<Cart>
    {
         Cart GetCartByUserId(string userId);
        void UpdateCart(Cart cart);
        
        void DeleteFromCart(int cartId, int productId);
    }
}