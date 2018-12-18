using DatingApp.Core.Entities.BaseEntities;

namespace DatingApp.Core.Entities.Catalog
{
    public class Like : BaseEntity
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}