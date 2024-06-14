namespace UrlShortener.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UrlMapper> GetUrl()
        {
            return new List<UrlMapper>()
            {
                new UrlMapper()
                {
                    Id = 1,
                    Url = "https://www.twitch.tv/likenar",
                    ShortUrl = "part.ly/likenar"
                },
                new UrlMapper()
                {
                    Id = 2,
                    Url = "https://www.twitch.tv/dmitry_bale",
                    ShortUrl = "part.ly/bale"
                }, new UrlMapper()
                {
                    Id = 3,
                    Url = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    ShortUrl = "part.ly/task"
                }
            };
        }

        [HttpGet("{id}")]
        public UrlMapper GetUrlById(int id)
        {
            return new UrlMapper()
            {
                Id = 1,
                Url = "https://www.twitch.tv/likenar",
                ShortUrl = "part.ly/likenar",
                CreatedDate = DateTime.Now,
                CreatedByUser = new IdentityUser("TestUsername")
            };
        }
    }
}
