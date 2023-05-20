using multitracks.Core.Dtos;

namespace multitracks.Infrastructure.Repositories
{
    public interface IUploadImageToAzureRepository
    {
        Task<ResponseDto<string>> UploadImageRepository(UploadImageDto upload);
    }
}