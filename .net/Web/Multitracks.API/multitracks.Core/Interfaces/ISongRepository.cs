using multitracks.Core.Dtos;
using multitracks.Domain.Models;

namespace multitracks.Infrastructure.Repositories
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllSongsAsync(RequestParam requestParam);
    }
}