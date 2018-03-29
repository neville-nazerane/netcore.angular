using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class AngularBloggingContext
    {

        public Post Post { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public Blog Blog { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }

    }
}
