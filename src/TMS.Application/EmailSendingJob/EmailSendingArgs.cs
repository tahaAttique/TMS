namespace TMS.EmailSendingJob;

public class EmailSendingArgs
{
    public required string EmailAddress { get; set; }
    public required string Name { get; set; }
    public required string Subject { get; set; }
     public required string Body { get; set; }
}
