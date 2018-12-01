using System;
using System.Linq.Expressions;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Specifications
{
    public class UserWithPhotosSpecification : BaseSpecification<User>
    {
        public UserWithPhotosSpecification(int? userId) : base(user => (!userId.HasValue) || user.Id == userId)
        {
            AddInclude(user => user.Photos);
        }
    }
}