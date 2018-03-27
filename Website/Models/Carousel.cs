using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Carousel
    {

        public int Id { get; set; }

        public string Img { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public static IEnumerable<Carousel> GetAll()
            => new List<Carousel> {
                new Carousel{
                    Id = 0,
                    Img = "banner1.svg",
                    Title = "Learn how to build ASP.NET apps that can run anywhere.",
                    Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409"
                },
                new Carousel{
                    Id = 1,
                    Img = "banner2.svg",
                    Title = "There are powerful new features in Visual Studio for building modern web apps.",
                    Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409"
                },
                new Carousel{
                    Id = 2,
                    Img = "banner3.svg",
                    Title = "Bring in libraries from NuGet and npm, and automate tasks using Grunt or Gulp.",
                    Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409"
                },
                new Carousel{
                    Id = 3,
                    Img = "banner4.svg",
                    Title = "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps.",
                    Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409"
                }
            };


    }
}
