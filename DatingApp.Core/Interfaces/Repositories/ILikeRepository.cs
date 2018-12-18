using System.Threading.Tasks;
using DatingApp.Core.Entities.Catalog;

namespace DatingApp.Core.Interfaces.Repositories
{
    public interface ILikeRepository : IRepository<Like>, IAsyncRepository<Like>
    {
        Task<Like> GetLike(int userId, int recipientId);
    }
}