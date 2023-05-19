using System.ComponentModel.DataAnnotations;

namespace multitracks.Core.Dtos
{
    public class CreateArtistDto
    {
        [Required]
        public string Title { get; set; }
        [Required]

        public string Biography { get; set; }
        [Required]

        public string ImageUrl { get; set; }
        [Required]

        public string HeroUrl { get; set; }
    }
}
