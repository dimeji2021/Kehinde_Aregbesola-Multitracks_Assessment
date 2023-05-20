using Microsoft.AspNetCore.Http;
using multitracks.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace multitracks.Core.Dtos
{
    public class UploadImageDto
    {
        [Required]
        public int ArtistId { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public ImageType ImageType { get; set; }

    }
}
