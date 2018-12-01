using System;

namespace DatingApp.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 0;
        private int pageSize  = 10;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize  = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int TotalPages(int totalItemsIncludingSkipResult)
        {
            return (int)Math.Ceiling((totalItemsIncludingSkipResult / (double)pageSize));
        }

        public int TotalResultsToSkipForPagination
        {
            get { return (PageNumber - 1) * PageSize; }
        }

        public int UserId { get; set; }
        public string Gender { get; set; }
        public int minAge { get; set; } = 18;
        public int maxAge { get; set; } = 99;
        public string OrderBy { get; set; }
    }
}