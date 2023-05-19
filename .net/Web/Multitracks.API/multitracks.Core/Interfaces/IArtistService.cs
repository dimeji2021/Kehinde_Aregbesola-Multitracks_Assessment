using multitracks.Core.Dtos;

namespace multitracks.Core.Interfaces
{
    public interface IArtistService
    {
        Task<ResponseDto<GetArtistDto>> CreateArtistAsync(CreateArtistDto request);
        Task<ResponseDto<List<GetArtistDto>>> SearchArtistByName(string name);
    }
}