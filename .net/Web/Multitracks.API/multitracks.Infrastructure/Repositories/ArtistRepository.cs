using Microsoft.EntityFrameworkCore;
using multitracks.Domain.Models;

namespace multitracks.Infrastructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ArtistRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Artist>> SearchArtistByNameAsync(string name)
        {
            return await _dbContext.Artist.Where(x => x.Title.ToLower().Contains(name.ToLower())).ToListAsync();
        }
        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            await _dbContext.Artist.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return artist;
        }
    }
}
