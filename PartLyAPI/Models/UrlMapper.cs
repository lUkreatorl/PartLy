namespace PartLyAPi.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UrlMapper
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public IdentityUser CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Url
    {
        public string UrlLink { get; set; }
    }


}
