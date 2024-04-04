namespace AzureBlobProject.Services
{
    public interface IContainer
    {
        Task<List<string>> GetAllContainer();

       Task<List<string>> GetAllContainerAndBlobs();

        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }

}
