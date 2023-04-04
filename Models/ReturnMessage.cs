namespace MOBY_API_Core6.Models
{
    public class ReturnMessage
    {
        public string? Message { get; set; }

        public static ReturnMessage Create(string message)
        {
            return new ReturnMessage { Message = message };
        }
    }
}
