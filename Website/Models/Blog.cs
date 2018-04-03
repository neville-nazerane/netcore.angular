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
        [NoRepeat]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }


        class NeverAttribute : ValidationAttribute
        {
            public override bool IsValid(object value) => value?.ToString() != "never";
        }

        internal void Validate(BloggingContext context)
        {
            isRepeat = context.Blogs.Any(b => b.Url == Url);
        }

        bool isRepeat;
        class NoRepeatAttribute : ValidationAttribute
        {

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance is Blog blog && blog.isRepeat)
                    return new ValidationResult("Blog already exists");
                else return ValidationResult.Success;
            }

        }

    }
}
