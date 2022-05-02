namespace MovieZone.Service.AWS.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using MovieZone.Service.DTOs.AWS.Storage;

    public interface IStorageService
    {
        Task UploadFileAsync(Stream fileStream, string fileName, string contentType);

        Task<GetFileByKeyDTO> GetFileByKeyAsync(string key);

        Task DeleteFileAsync(string key);
    }
}
