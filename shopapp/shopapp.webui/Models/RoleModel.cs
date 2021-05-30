using System.ComponentModel.DataAnnotations;

namespace shopapp.webui.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}