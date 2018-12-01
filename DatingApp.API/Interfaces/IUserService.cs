using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.API.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUsers(int pageIndex, int itemsPage, string gender);
    }
}