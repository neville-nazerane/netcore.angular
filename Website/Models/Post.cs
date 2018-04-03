using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Post
    {

        public int PostId { get; set; }

        [Required]
        [MaxLength(15)]
        [Never(ErrorMessage = "Never say post")]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        class NeverAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) => value.ToString() != "post";
        }

    }
}
