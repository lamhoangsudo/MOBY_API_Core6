using Item.Models;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IImageRepository
    {
        bool CreateImage (string image1, string image2, string image3, string image4, string image5);
        bool UpdateImagesItem (int imageID, string image1, string image2, string image3, string image4, string image5);
    }
}
