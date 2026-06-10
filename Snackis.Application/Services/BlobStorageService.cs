using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Snackis.Application.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorage:ConnectionString"];
            var containerName = configuration["AzureStorage:ContainerName"];
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var blobClient = _containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(imageStream, new BlobHttpHeaders
            {
                ContentType = GetContentType(fileName)
            });

            return blobClient.Uri.ToString();
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var blobName = Path.GetFileName(uri.LocalPath);
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        private static string GetContentType(string fileName) =>
            Path.GetExtension(fileName).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
    }
}