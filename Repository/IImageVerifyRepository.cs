namespace MOBY_API_Core6.Repository
{
    public interface IImageVerifyRepository
    {
        public Task<bool> verify(String url);
    }
}
