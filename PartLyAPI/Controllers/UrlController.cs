namespace PartLyAPi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UrlController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlMapper>>> GetUrls()
        {
            return await _context.UrlMapper.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UrlMapper>> GetUrlById(int id)
        {
            var url = await _context.UrlMapper.Include(u => u.CreatedByUser).FirstOrDefaultAsync(x => x.Id == id);

            if (url == null)
            {
                return NotFound();
            }

            var response = new UrlResponseModel
            {
                Id = url.Id,
                Url = url.Url,
                ShortUrl = url.ShortUrl,
                CreatedDate = url.CreatedDate,
                CreatedByUserName = url.CreatedByUser?.UserName
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUrl([FromBody] Url url)
        {
            var existingUrl = await _context.UrlMapper.FirstOrDefaultAsync(x => x.Url == url.UrlLink);

            try
            {
                if (existingUrl == null)
                {
                    var shortUrl = $"{ShortUrlHelper.GenerateShortUrl()}";
                    var newUrl = new UrlMapper { Url = url.UrlLink, ShortUrl = shortUrl, CreatedDate = DateTime.Now };
                    _context.UrlMapper.Add(newUrl);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetUrlById), new { id = newUrl.Id }, newUrl);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
            return BadRequest(new { message = "Link already exists" });
        }

        [Route("/{shortUrl}")]
        [HttpGet]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var url = await _context.UrlMapper.FirstOrDefaultAsync(x => x.ShortUrl == $"{shortUrl}");

            if (url == null)
            {
                return NotFound(new { message = "Short URL not found" });
            }

            return Redirect(url.Url);
        }
    }
}
