using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Blog
    {

        public int BlogId { get; set; }

        [Required]
        [Never(ErrorMessage = "Never say never")]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }


        class NeverAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) => value.ToString() != "never";
        }

    }
}
