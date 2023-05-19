using multitracks.Core.Dtos;

namespace multitracks.Core.Interfaces
{
    public interface ISongService
    {
        Task<ResponseDto<List<GetSongDto>>> GetAllSongsAsync(int pageNumber, int pageSize);
    }
}