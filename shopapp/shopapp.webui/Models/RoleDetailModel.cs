using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using shopapp.webui.Identity;

namespace shopapp.webui.Models
{
    public class RoleDetailModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}