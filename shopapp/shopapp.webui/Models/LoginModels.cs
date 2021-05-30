using System.ComponentModel.DataAnnotations;

namespace shopapp.webui.Models
{
    public class LoginModels
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}