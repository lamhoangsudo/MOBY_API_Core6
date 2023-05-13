using MOBY_API_Core6.Models;
namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IEmailRepository
    {
        Task SendEmai(Email emailTo);
    }
}
