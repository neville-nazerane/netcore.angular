using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models;

namespace Website.Controllers
{
    public class AngularController : Controller
    {
        private readonly BloggingContext context;

        public AngularController(BloggingContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
            => View(new AngularBloggingContext {
                Blogs = context.Blogs.Include(b => b.Posts)
            });

        public IActionResult V2() => View(new AngularBloggingContext
        {
            Blogs = context.Blogs.Include(b => b.Posts)
        });

        [HttpPost]
        public IActionResult AddBlog([FromBody]Blog blog)
        {
            blog.Validate(context);
            ModelState.Clear();
            if (TryValidateModel(blog))
            {
                context.Add(blog);
                context.SaveChanges();
                return Ok(blog);
            }
            var result = PartialView(blog);
            result.StatusCode = 400;
            return result;
        }

        [HttpGet]
        public IActionResult AddPost(int blogID)
            => PartialView(new Post { BlogId = blogID });

        [HttpPost]
        public IActionResult AddPost([FromBody]Post post)
        {
            if (ModelState.IsValid)
            {
                context.Add(post);
                context.SaveChanges();
                return Ok(post);
            }
            var result = PartialView(post);
            result.StatusCode = 400;
            return result;
        }
    }
}
