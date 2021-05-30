namespace shopapp.entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } //Ürünle ilgili tüm özelliklere Product tablosundan erişmek için
        public int CartId { get; set; } //Kullanıcıya erişir buradan Cart tablosundan
        public Cart Cart { get; set; } //Sepet ile ilgili tüm özelliklere bu tablodan erişmek için
        public int Quantity { get; set; } //İlgili Üründen kaç tane aldığı kaydı

    }
}