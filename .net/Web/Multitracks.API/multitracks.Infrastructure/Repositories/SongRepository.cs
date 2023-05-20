using Microsoft.EntityFrameworkCore;
using multitracks.Core.Dtos;
using multitracks.Domain.Models;

namespace multitracks.Infrastructure.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SongRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Song>> GetAllSongsAsync(RequestParam requestParam)
        {
            int skip = (requestParam.PageNumber - 1) * requestParam.PageSize;
            var songs = await _dbContext.Song
                .Include(x => x.Artist)
                .Include(x => x.Album)
                .OrderBy(x => x.SongId)
                           .Skip(skip)
                           .Take(requestParam.PageSize)
               .ToListAsync();
            return songs;
        }
    }
}
