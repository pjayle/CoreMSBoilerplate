using Microsoft.AspNetCore.Identity;

namespace gumfa.services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string EmpID { get; set; }
    }
}
