using AutoMapper;
using Microsoft.Extensions.Logging;
using multitracks.Core.Dtos;
using multitracks.Core.Interfaces;
using multitracks.Domain.Models;
using multitracks.Infrastructure.Repositories;
using System.Net;

namespace multitracks.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository artistRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ArtistService> _log;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper, ILogger<ArtistService> log)
        {
            this.artistRepository = artistRepository;
            this.mapper = mapper;
            _log = log;
        }

        public async Task<ResponseDto<List<GetArtistDto>>> SearchArtistByName(string name)
        {
            _log.LogInformation("Calling the search artist method.");
            var artists = await artistRepository.SearchArtistByNameAsync(name);
            if (artists is null)
            {
                return ResponseDto<List<GetArtistDto>>.Fail("No artist found that matches the search result", (int)HttpStatusCode.NotFound);
            }
            var mappedArtists = mapper.Map<List<GetArtistDto>>(artists);
            return ResponseDto<List<GetArtistDto>>.Success("Success", mappedArtists, (int)HttpStatusCode.OK);
        }
        public async Task<ResponseDto<GetArtistDto>> CreateArtistAsync(CreateArtistDto request)
        {
            _log.LogInformation("Calling the create artist method.");
            var artist = mapper.Map<Artist>(request);
            var artistCreated = await artistRepository.CreateArtistAsync(artist);
            var mappedArtists = mapper.Map<GetArtistDto>(artistCreated);
            return ResponseDto<GetArtistDto>.Success("Artist has been created successfully", mappedArtists, (int)HttpStatusCode.Created);
        }
    }
}
