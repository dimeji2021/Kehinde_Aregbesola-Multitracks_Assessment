using Microsoft.AspNetCore.Mvc;
using multitracks.Core.Dtos;
using multitracks.Core.Interfaces;

namespace multitracks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly ILogger<SongController> _log;

        public SongController(ISongService songService, ILogger<SongController> log)
        {
            _songService = songService;
            _log = log;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAllSong([FromQuery]RequestParam requestParam)
        {
            _log.LogInformation("Executing list all songs endpoint");
            var songs = await _songService.GetAllSongsAsync(requestParam);
            return StatusCode(songs.StatusCode, songs);
        }
    }
}
