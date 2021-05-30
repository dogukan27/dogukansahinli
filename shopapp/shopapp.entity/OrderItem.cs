namespace shopapp.entity
{
    public class OrderItem
    {
        //Sipariş edilen ürünlerler ilgili ürünlerin bulunduğu tablo
        public int Id { get; set; }
        public int OrderId { get; set; } //Order tablosundan gelen Id foreign key
        public Order Order { get; set; }
        public int ProductId { get; set; } //Product tablosundan gelen id foreign key
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}