using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IEmailService
    {
        Task SendEmai(Email emailTo);
    }
}
