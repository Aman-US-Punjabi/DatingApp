using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Interfaces.Services
{
    public interface IDatingService
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        // Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
    }
}