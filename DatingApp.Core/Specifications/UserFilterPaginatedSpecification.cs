using System;
using System.Linq.Expressions;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Specifications
{
    public class UserFilterPaginatedSpecification : BaseSpecification<User>
    {
        public UserFilterPaginatedSpecification(int skip, int take, string gender, int? excludeUserId, DateTime? minDateOfBirth, DateTime? maxDateOfBirth,  bool includePhotos = false)
            : base(user => (
                (String.IsNullOrEmpty(gender) || user.Gender.Equals(gender))&&
                (!minDateOfBirth.HasValue || user.DateOfBirth >= minDateOfBirth) &&
                (!maxDateOfBirth.HasValue || user.DateOfBirth <= maxDateOfBirth) &&
                (!excludeUserId.HasValue || user.Id != excludeUserId)

            ))
        {
            ApplyPaging(skip, take);
            
            if (includePhotos)
                AddInclude(u => u.Photos);
        }
    }
}