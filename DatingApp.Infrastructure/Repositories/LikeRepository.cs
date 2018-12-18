using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;
using DatingApp.Core.Interfaces.Repositories;
using DatingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories
{
    public class LikeRepository : EfRepository<Like>, ILikeRepository
    {
        private readonly CatalogContext _dbContext;

        public LikeRepository(CatalogContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _dbContext.Likes.FirstOrDefaultAsync(u => 
                u.LikerId == userId && u.LikeeId == recipientId);
        }
    }
}