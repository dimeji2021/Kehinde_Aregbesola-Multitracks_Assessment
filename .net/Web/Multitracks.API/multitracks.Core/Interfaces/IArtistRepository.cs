using multitracks.Domain.Models;

namespace multitracks.Infrastructure.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> CreateArtistAsync(Artist artist);
        Task<List<Artist>> SearchArtistByNameAsync(string name);
    }
}