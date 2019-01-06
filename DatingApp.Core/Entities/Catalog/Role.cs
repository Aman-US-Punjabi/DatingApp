using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Entities.Catalog
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}