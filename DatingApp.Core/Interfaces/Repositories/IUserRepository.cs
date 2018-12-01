using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>, IAsyncRepository<User>
    {
        User GetByIdWithPhotos(int id);
        Task<User> GetByIdWithPhotosAsync(int id);

        Photo GetMainPhotoByUserId(int id);
        Task<Photo> GetMainPhotoByUserIdAsync(int id);
    }
}