using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitracks.Core.Dtos;
using multitracks.Infrastructure.Repositories;

namespace multitracks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImageUploadController : ControllerBase
    {
        private readonly IUploadImageToAzureRepository _uploadImageToAzure;

        public ImageUploadController(IUploadImageToAzureRepository uploadImageToAzure)
        {
            _uploadImageToAzure = uploadImageToAzure;
        }

        [HttpPost("upload-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UploadImage([FromQuery] UploadImageDto upload)
        {
            var result = await _uploadImageToAzure.UploadImageRepository(upload);
            return StatusCode(result.StatusCode, result);
        }
    }
}