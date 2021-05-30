using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICartService
    {
        //Sepeti oluşturma
         void InitializeCart(string userId);
        //Kullanıcı id sine göre sepeti dönderme string id int olsa zaten hazırı var
        Cart GetCartByUserId(string userId);

        void AddToCart(string userId,int productId,int quantity);
        void DeleteFromCart(string userId, int productId);
    }
}