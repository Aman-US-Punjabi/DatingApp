using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;
using DatingApp.Core.Interfaces.Repositories;
using DatingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        private readonly CatalogContext _dbContext;

        public UserRepository(CatalogContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetByIdWithPhotos(int id)
        {
        //  _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id ==id);   
            return _dbContext.Users
                .Include(u => u.Photos).FirstOrDefault(u => u.Id ==id);
        }

        public Task<User> GetByIdWithPhotosAsync(int id)
        {
            return _dbContext.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => u.Id ==id);
        }

        public Photo GetMainPhotoByUserId(int id)
        {
            var photo = _dbContext.Photos.Where(p => p.UserId == id)
                .FirstOrDefault(p => p.IsMain);

            return photo; 
        }

        public Task<Photo> GetMainPhotoByUserIdAsync(int id)
        {
            return _dbContext.Photos.Where(p => p.UserId == id)
                .FirstOrDefaultAsync(p => p.IsMain);
        }
    }
}