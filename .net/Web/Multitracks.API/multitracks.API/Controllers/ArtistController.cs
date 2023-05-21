using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitracks.Core.Dtos;
using multitracks.Core.Interfaces;

namespace multitracks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ILogger<ArtistController> _log;

        public ArtistController(IArtistService artistService, ILogger<ArtistController> log)
        {
            _artistService = artistService;
            _log = log;
        }

        [HttpGet("search/{name}", Name = "search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(string name)
        {
            _log.LogInformation("Executing search artist endpoint");
            var artist = await _artistService.SearchArtistByName(name);
            return StatusCode(artist.StatusCode, artist);
        }
        [HttpPost("create-artist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateArtist(CreateArtistDto request)
        {
            _log.LogInformation("Executing create artist endpoint");
            var artist = await _artistService.CreateArtistAsync(request);
            return StatusCode(artist.StatusCode, artist);
        }
    }
}
