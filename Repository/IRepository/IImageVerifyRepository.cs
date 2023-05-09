namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IImageVerifyRepository
    {
        public Task<bool> Verify(string url);
    }
}
