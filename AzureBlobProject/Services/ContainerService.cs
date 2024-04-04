
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainer
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerName = new();

            await foreach (BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerName.Add(blobContainerItem.Name);
            }
            return containerName;
        }

        public async Task<List<string>> GetAllContainerAndBlobs()
        {
            List<string> containerandBlobNames = new();
            containerandBlobNames.Add("Account name:" + _blobServiceClient.AccountName);
            containerandBlobNames.Add("---------------------------------------------------------------------------");
            await foreach (BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerandBlobNames.Add("--" + blobContainerItem.Name);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach (BlobItem blobItem in _blobContainerClient.GetBlobsAsync())
                {
                    containerandBlobNames.Add("------" + blobItem.Name);
                }
                containerandBlobNames.Add("----------------------------------------------------------------------------------");
            }
            return containerandBlobNames;
        }
    }
}
