using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Song>> GetAllSongsAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var songs = await _dbContext.Song
                .Include(x => x.Artist)
                .Include(x => x.Album)
                .OrderBy(x => x.SongId)
                           .Skip(skip)
                           .Take(pageSize)
               .ToListAsync();
            return songs;
        }
    }
}
