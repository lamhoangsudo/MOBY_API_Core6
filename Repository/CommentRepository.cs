﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MOBYContext context;

        public CommentRepository(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<List<CommentVM>> GetAllComment()
        {
            List<CommentVM> ListComment = await context.Comments
                .Include(c => c.Replies)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();

            return ListComment;
        }

        public async Task<List<CommentVM>> GetCommentByBlogID(int id)
        {
            List<CommentVM> ListComment = await context.Comments.Where(cmt => cmt.BlogId == id)
                .Include(c => c.Replies)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();


            return ListComment;
        }

        public async Task<List<CommentVM>> GetCommentByItemID(int id)
        {
            List<CommentVM> ListComment = await context.Comments.Where(cmt => cmt.ItemId == id)
                .Include(c => c.Replies)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();
            return ListComment;
        }

        public async Task<bool> CreateComment(CreateCommentVM cmt, int userId)
        {
            Comment newCmt = new Comment();
            newCmt.ItemId = cmt.ItemId;
            newCmt.BlogId = cmt.BlogId;
            newCmt.UserId = userId;
            newCmt.CommentContent = cmt.CommentContent;
            newCmt.DateCreate = DateTime.Now;

            await context.AddAsync(newCmt);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateComment(UpdateCommentVM cmt, int userId)
        {
            Comment? currentcmt = await context.Comments.Where(cmt => cmt.CommentId == cmt.CommentId && cmt.UserId == userId).FirstOrDefaultAsync();

            if (currentcmt != null)
            {
                currentcmt.CommentContent = cmt.CommentContent;
                currentcmt.DateUpdate = DateTime.Now;

                if (await context.SaveChangesAsync() != 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteComment(GetCommentIDVM cmt, int userId)
        {
            Comment? currentcmt = await context.Comments.Where(cmt => cmt.CommentId == cmt.CommentId && cmt.UserId == userId).FirstOrDefaultAsync();

            if (currentcmt != null)
            {
                context.Remove(currentcmt);
                if (await context.SaveChangesAsync() != 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}