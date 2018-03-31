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

        [HttpPost]
        public IActionResult AddBlog([FromBody]Blog blog)
        {
            if (ModelState.IsValid)
            {
                context.Add(blog);
                context.SaveChanges();
                return Ok(blog);
            }
            else return BadRequest(new { noo = "search your feelings", know = 4, blog.Url });
        }
    }
}
