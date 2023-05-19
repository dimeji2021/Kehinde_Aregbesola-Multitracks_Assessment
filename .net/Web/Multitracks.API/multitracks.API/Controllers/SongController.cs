using Microsoft.AspNetCore.Mvc;
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

        [HttpGet, Route("list")]
        public async Task<IActionResult> ListAllSong(int pageNumber, int pageSize)
        {
            _log.LogInformation("Executing list all songs endpoint");
            var songs = await _songService.GetAllSongsAsync(pageNumber, pageSize);
            return StatusCode(songs.StatusCode, songs);
        }
    }
}
