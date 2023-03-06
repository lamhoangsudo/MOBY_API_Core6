﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogCategoryRepository BlogCateDAO;
        private readonly IBlogRepository BlogDAO;
        private readonly IUserRepository UserDAO;

        public BlogController(IBlogRepository BlogDAO, IBlogCategoryRepository BlogCateDAO, IUserRepository userDAO)
        {
            this.BlogDAO = BlogDAO;
            this.BlogCateDAO = BlogCateDAO;
            UserDAO = userDAO;
        }

        [HttpGet]
        [Route("api/blog/all")]
        public async Task<IActionResult> getAllBlog([FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getAllBlog(pagging);

                return Ok(ListBlog);

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getAllBlog"));
            }
        }

        [HttpGet]
        [Route("api/blog/new")]
        public async Task<IActionResult> getNewBlog()
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getNewBlog();

                return Ok(ListBlog);

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getNewBlog"));
            }
        }



        [HttpGet]
        [Route("api/blog")]
        public async Task<IActionResult> getBlogByQuery([FromQuery] BlogGetVM blogGetVM, PaggingVM pagging)
        {
            try
            {
                List<BlogVM> ListBlog = new List<BlogVM>();
                if (blogGetVM.categoryId != null)
                {
                    ListBlog = await BlogDAO.getBlogByBlogCateID(blogGetVM.categoryId.Value, pagging);

                    return Ok(ListBlog);

                }
                else if (blogGetVM.userId != null)
                {
                    ListBlog = await BlogDAO.getBlogByUserID(blogGetVM.userId.Value, pagging);

                    return Ok(ListBlog);
                }
                else if (blogGetVM.BlogId != null)
                {
                    Blog? foundBlog = await BlogDAO.getBlogByBlogID(blogGetVM.BlogId.Value);
                    if (foundBlog != null)
                    {
                        return Ok(BlogVM.BlogToVewModel(foundBlog));
                    }
                    return Ok(foundBlog);
                }
                return Ok(ReturnMessage.create("there no field so no blog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getBlogByQuery"));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/blog")]
        public async Task<IActionResult> getBlogByToken([FromQuery] PaggingVM pagging)
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<BlogVM> ListBlog = await BlogDAO.getBlogBySelf(UserID, pagging);
                return Ok(ListBlog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ReturnMessage.create("error at getBlogByToken"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/blog/create")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogVM createdBlog)
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (await BlogDAO.CreateBlog(createdBlog, UserID))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create("error at CreateBlog"));
                }
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at CreateBlog"));
            }

        }

        [Authorize]
        [HttpPut]
        [Route("api/blog")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogVM UpdatedBlog)
        {
            try
            {

                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(UpdatedBlog.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.UpdateBlog(foundblog, UpdatedBlog))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/accept")]
        public async Task<IActionResult> AcceptBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog? foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 1))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/deny")]
        public async Task<IActionResult> DenyBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog? foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 2))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/delete")]
        public async Task<IActionResult> DeleteBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(blogId.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 3))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at DeleteBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at DeleteBlog"));
            }

        }

    }
}
