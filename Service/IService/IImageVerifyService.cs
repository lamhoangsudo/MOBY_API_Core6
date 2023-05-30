namespace MOBY_API_Core6.Service.IService
{
    public interface IImageVerifyService
    {
        public Task<string> Verify(string url);
    }
}
