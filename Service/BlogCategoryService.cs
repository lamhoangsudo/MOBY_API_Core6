using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository blogCategoryRepository;
        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository)
        {
            this.blogCategoryRepository = blogCategoryRepository;
        }
        public async Task<List<BlogCategoryOnlyVM>> GetAllBlogCategory(int status)
        {
            List<BlogCategoryOnlyVM> blogCategories = await blogCategoryRepository.GetAllBlogCategory(status);
            return blogCategories;
        }
        public async Task<bool> CreateBlogCategory(string name)
        {
            int check = await blogCategoryRepository.CreateBlogCategory(name);
            if (check <= 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM)
        {
            int check = await blogCategoryRepository.UpdateBlogCategory(updateBlogCategoryVM);
            if (check <= 0)
            {
                return false;
            }
            return true;

        }
        public async Task<bool> DeleteBlogCategory(BlogCateGetVM blogCateGetVM)
        {
            int check = await blogCategoryRepository.DeleteBlogCategory(blogCateGetVM);
            if (check <= 0)
            {
                return false;
            }
            return true;
        }
        public async Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId)
        {
            BlogCategoryVM? foundBlogCate = await blogCategoryRepository.GetBlogCateByID(blogCateId);
            if (foundBlogCate != null)
            {
                return foundBlogCate;
            }
            else
            {
                return null;
            }
        }
    }
}
