using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Entities.Catalog
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}