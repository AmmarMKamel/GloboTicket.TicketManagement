namespace GloboTicket.TicketManagement.Api.Utility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FileResultContentTypeAttribute : Attribute
    {
        public string ContentType { get; }

        public FileResultContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }
    }
}
