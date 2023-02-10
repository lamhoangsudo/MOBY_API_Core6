using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly MOBYContext _context;
        public ImageRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateImage(string image1, string image2, string image3, string image4, string image5)
        {
            if (image1 == null || image1.Equals(""))
            {
                return false;
            }
            if (image2 == null || image2.Equals(""))
            {
                return false;
            }
            if (image3 == null || image3.Equals(""))
            {
                return false;
            }
            else
            {
                try
                {
                    var checkCreate = _context.Database.ExecuteSqlInterpolated($"EXEC create_Imange {image1},{image2},{image3},{image4},{image5}");
                    if (checkCreate != 0)
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateImagesItem(int imageID, string image1, string image2, string image3, string image4, string image5)
        {
            if (image1 == null || image1.Equals(""))
            {
                return false;
            }
            if (image2 == null || image2.Equals(""))
            {
                return false;
            }
            if (image3 == null || image3.Equals(""))
            {
                return false;
            }
            try
            {
                var checkUpdate = _context.Database.ExecuteSqlInterpolated($"");
                if (checkUpdate > 0)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
