using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Core.Dtos
{
    public class GetArtistDto
    {
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public string ImageUrl { get; set; }
        public string HeroUrl { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
