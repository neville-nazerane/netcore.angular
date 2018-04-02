using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Services
{
    public class BloggingProvider
    {
        private readonly BloggingContext context;

        public BloggingProvider(BloggingContext context)
        {
            this.context = context;
        }

        public IQueryable<Post> GetPosts(ClaimsPrincipal User, int? blogID = null)
        {
            IQueryable<Post> posts = context.Posts;
            if (blogID != null)
            {
                posts = posts.Where(p => p.BlogId == blogID);
            }
            return posts;
        }

        public IQueryable<Blog> GetBlogs(ClaimsPrincipal User, int? postID = null)
        {
            IQueryable<Blog> blogs = null;
            if (postID != null)
            {
                blogs = from p in context.Posts
                        where p.PostId == postID
                        select p.Blog;
            }
            else blogs = context.Blogs;
            return blogs.Include(b => b.Posts);
        }

    }
}
