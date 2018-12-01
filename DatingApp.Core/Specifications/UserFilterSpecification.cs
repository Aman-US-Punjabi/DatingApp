using System;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Specifications
{
    public class UserFilterSpecification : BaseSpecification<User>
    {
        public UserFilterSpecification(string gender, int? excludeUserId, DateTime? minDateOfBirth, DateTime? maxDateOfBirth, bool includePhotos = false )
            : base(user => ( 
                (String.IsNullOrEmpty(gender) || user.Gender.Equals(gender))&&
                (!minDateOfBirth.HasValue || user.DateOfBirth >= minDateOfBirth) &&
                (!maxDateOfBirth.HasValue || user.DateOfBirth <= maxDateOfBirth) &&
                (!excludeUserId.HasValue || user.Id != excludeUserId)
            ))
        {
            if (includePhotos)
                AddInclude(u => u.Photos);
        }
    }
}