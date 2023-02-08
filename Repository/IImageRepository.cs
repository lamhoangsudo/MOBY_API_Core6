using Item.Models;
using MOBY_API_Core6.Models;

namespace Item.Repository
{
    public interface IImageRepository
    {
        bool CreateImage (string image1, string image2, string image3, string image4, string image5);
        Image GetImagesItemDetail (int itemID);
    }
}
