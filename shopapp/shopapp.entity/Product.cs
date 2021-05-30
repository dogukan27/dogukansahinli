using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shopapp.entity
{
    public class Product
    {
        public int ProductId { get; set; }  
        
        [Required(ErrorMessage="Name alanı zorunludur")]
        [StringLength(60,MinimumLength=5,ErrorMessage="Uzunluğu 5-60 arasında olmalı")]
        public string Name { get; set; }   
        [Required(ErrorMessage="Price alanı zorunludur")]
        [Range(0,13000,ErrorMessage="Fİyat 0 ile 13000 arasında olmalı.")]
        public double Price { get; set; } 
        [Required(ErrorMessage="Description alanı zorunludur")]
        [StringLength(60,MinimumLength=5,ErrorMessage="Uzunluğu 5-60 arasında olmalı")]
        public string Description { get; set; }     
        [Required(ErrorMessage="ImageUrl alanı zorunludur")]    
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}