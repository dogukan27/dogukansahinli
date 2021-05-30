using System;
using System.Collections.Generic;

namespace shopapp.entity
{
    public class Order
    {
        //Kullanıcı ile ilgili bilgilerin bulunduğu tablo
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; } //Sipariş eden ile Alıcı farklı olabileceği için yazdık
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public EnumOrderState OrderState { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }


    public enum EnumOrderState
    {
        //Sipariş durumunun hangi aşamada olduğunu öğrenmek için
        onayBekliyor=0,
        odenmemis=1,
        tamamlanmis=2
    }
}