using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YemekTarifleri.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Food> Food { get; set; }
    }
}