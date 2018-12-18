using System;
using System.Collections.Generic;
using System.Linq;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Specifications
{
    public class UserFilterSpecification : BaseSpecification<User>
    {
        public UserFilterSpecification(string gender, int? excludeUserId,
            DateTime? minDateOfBirth, DateTime? maxDateOfBirth, bool includePhotos = false,
            IEnumerable<int> userLikers = null, IEnumerable<int> userLikees = null )
            : base(user => ( 
                (String.IsNullOrEmpty(gender) || user.Gender.Equals(gender)) &&
                (!minDateOfBirth.HasValue || user.DateOfBirth >= minDateOfBirth) &&
                (!maxDateOfBirth.HasValue || user.DateOfBirth <= maxDateOfBirth) &&
                (!excludeUserId.HasValue || user.Id != excludeUserId) &&
                (userLikers == null || userLikers.Contains(user.Id)) &&
                (userLikees == null || userLikees.Contains(user.Id))
            ))
        {
            if (includePhotos)
                AddInclude(u => u.Photos);
        }
    }
}