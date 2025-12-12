using Volo.Abp.BlobStoring;

namespace TMS.LocalStorage.Ticket;

[BlobContainerName(TicketFileContainerName)]
public class TicketFileContainer
{
    public const string TicketFileContainerName = "ticket-file-container";
}
