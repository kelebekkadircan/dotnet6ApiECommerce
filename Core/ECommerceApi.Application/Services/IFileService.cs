using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path , IFormFileCollection formFiles);

        //Task<string> FileRenameAsync(string fileName); // private olması için interfaceden silindi

        Task<bool> CopyFileAsync(string path, IFormFile file);

    }
}
