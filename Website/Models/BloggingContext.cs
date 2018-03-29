using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class BloggingContext : DbContext
    {


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
        }


    }
}
