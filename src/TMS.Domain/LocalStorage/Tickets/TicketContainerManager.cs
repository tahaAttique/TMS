using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;

namespace TMS.LocalStorage.Ticket;

public class TicketContainerManager(IBlobContainer<TicketFileContainer> ticketFileContainer, IConfiguration configuration) : DomainService
{
    public async Task<string> SaveImageAsync(string imageName, byte[] imageBytes)
    {
        await ticketFileContainer.SaveAsync(imageName, imageBytes);
        return GetStorageUrl(imageName);
    }

    public async Task<byte[]> GetImageAsync(string imageName)
    {
        return await ticketFileContainer.GetAllBytesAsync(imageName);
    }

    public async Task<bool> DeleteAsync(string fileName)
    {
        return await ticketFileContainer.DeleteAsync(fileName);
    }

    public string GetStorageUrl(string fileName)
    {
        return $"{configuration["LocalStorageSetting:BaseUrl"]}{configuration["LocalStorageSetting:StoragePath"]}{Path.Combine(TicketFileContainer.TicketFileContainerName, fileName)}";
    }
}
