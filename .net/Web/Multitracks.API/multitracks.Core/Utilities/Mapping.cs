using AutoMapper;
using multitracks.Core.Dtos;
using multitracks.Domain.Models;

namespace multitracks.Core.Utilities
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Artist, CreateArtistDto>().ReverseMap()
                .ForMember(dest => dest.DateCreation, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Artist, GetArtistDto>().ReverseMap();
            CreateMap<GetSongDto, Song>().ReverseMap();
        }
    }
}
