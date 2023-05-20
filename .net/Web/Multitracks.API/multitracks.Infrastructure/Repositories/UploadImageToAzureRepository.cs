using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using multitracks.Core.Dtos;
using multitracks.Domain.Enums;
using multitracks.Infrastructure.Settings;
using System.Net;

namespace multitracks.Infrastructure.Repositories
{
    public class UploadImageToAzureRepository : IUploadImageToAzureRepository
    {
        private readonly AzureOptions _azureOptions;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UploadImageToAzureRepository> _log;

        public UploadImageToAzureRepository(IOptions<AzureOptions> azureOptions, ApplicationDbContext dbContext, ILogger<UploadImageToAzureRepository> log)
        {
            _azureOptions = azureOptions.Value;
            _dbContext = dbContext;
            _log = log;
        }

        public async Task<ResponseDto<string>> UploadImageRepository(UploadImageDto upload)
        {
            _log.LogInformation("Successfull enter the image upload service");
            var artist = await _dbContext.Artist.FirstOrDefaultAsync(a => a.ArtistId == upload.ArtistId);
            if (artist is null)
            {
                _log.LogInformation("Artist is not found");
                return ResponseDto<string>.Fail("Artist is not found", (int)HttpStatusCode.NotFound);
            }
            var fileExtension = Path.GetExtension(upload.File.FileName);
            using MemoryStream fileUploadStream = new MemoryStream();
            upload.File.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;
            var blobcontainerClient = new BlobContainerClient(_azureOptions.ConnectionString, _azureOptions.Container);
            var uniqueName = $"{artist.ArtistId}{fileExtension}";
            var blobClient = blobcontainerClient.GetBlobClient(uniqueName);
            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/bitmap"
                }
            }, CancellationToken.None);
            _log.LogInformation("Successful upload the image to Azure storage");
            if (upload.ImageType == ImageType.ArtistImage)
            {
                artist.ImageUrl = blobClient.Uri.AbsoluteUri;
            }
            else if (upload.ImageType == ImageType.HeroImage)
            {
                artist.HeroUrl = blobClient.Uri.AbsoluteUri;
            }
            _log.LogInformation("Saving image url to database...............");
            _dbContext.Entry(artist).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            _log.LogInformation("Successful saved image url to database");
            return ResponseDto<string>.Success("Successful upload the image", blobClient.Uri.AbsoluteUri, (int)HttpStatusCode.OK);
        }
    }
}
