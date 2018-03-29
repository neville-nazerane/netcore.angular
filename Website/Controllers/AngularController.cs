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

    }
}
