namespace MOBY_API_Core6.Models
{
    public class ReturnMessage
    {
        public string? message { get; set; }

        public static ReturnMessage create(string message)
        {
            return new ReturnMessage { message = message };
        }
    }
}
