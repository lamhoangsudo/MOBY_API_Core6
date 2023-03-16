namespace MOBY_API_Core6.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestDetails = new HashSet<RequestDetail>();
        }

        public int RequestId { get; set; }
        public int UserId { get; set; }

        public virtual UserAccount? User { get; set; }
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}
