using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.business.Concreate
{
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            this._cartRepository=cartRepository;
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart=_cartRepository.GetCartByUserId(userId);
            if (cart!=null)
            {
                var index=cart.CartItems.FindIndex(i=>i.ProductId==productId);
                if (index<0)
                {
                    cart.CartItems.Add(new CartItem(){
                        ProductId=productId,
                        Quantity=quantity,
                        CartId=cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity+=quantity;
                }

                _cartRepository.UpdateCart(cart);
            }
        }

        public void DeleteFromCart(string userId, int productId)
        {
             var cart=_cartRepository.GetCartByUserId(userId);
             if (cart!=null)
             {
                 _cartRepository.DeleteFromCart(cart.Id,productId);
             }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        //Yeni sepet oluşturma
        public void InitializeCart(string userId)
        {
            _cartRepository.Create(new Cart(){UserId=userId});
        }
    }
}