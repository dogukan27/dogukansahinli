using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YemekTarifleri.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}