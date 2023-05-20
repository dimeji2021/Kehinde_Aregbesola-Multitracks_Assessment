using AutoMapper;
using Microsoft.Extensions.Logging;
using multitracks.Core.Dtos;
using multitracks.Core.Interfaces;
using multitracks.Infrastructure.Repositories;
using System.Net;

namespace multitracks.Core.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository songRepository;
        private readonly IMapper mapper;
        private readonly ILogger<SongService> _log;

        public SongService(ISongRepository songRepository, IMapper mapper, ILogger<SongService> log)
        {
            this.songRepository = songRepository;
            this.mapper = mapper;
            _log = log;
        }

        public async Task<ResponseDto<List<GetSongDto>>> GetAllSongsAsync(RequestParam requestParam)
        {
            _log.LogInformation("Calling the get all songs  method.");
            var songs = await songRepository.GetAllSongsAsync(requestParam);
            if (songs.Count == 0)
            {
                return ResponseDto<List<GetSongDto>>.Fail("No Song found", (int)HttpStatusCode.NotFound);
            }
            var mappedSongs = mapper.Map<List<GetSongDto>>(songs);
            return ResponseDto<List<GetSongDto>>.Success("Success", mappedSongs, (int)HttpStatusCode.OK);
        }
    }
}
