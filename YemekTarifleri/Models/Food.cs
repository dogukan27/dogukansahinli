using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YemekTarifleri.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FoodLocation { get; set; }
        public string Specification { get; set; }
        public int CategoryId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public Category Category { get; set; }
    }
}