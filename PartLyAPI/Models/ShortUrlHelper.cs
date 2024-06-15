namespace PartLyAPi.Models
{
    using System.Text;
    public static class ShortUrlHelper
    {
        private static readonly string Base62Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GenerateShortUrl()
        {
            var guid = Guid.NewGuid();
            var base62Guid = ToBase62String(guid);
            return base62Guid.Substring(0, 8);
        }

        private static string ToBase62String(Guid guid)
        {
            var bytes = guid.ToByteArray();
            var result = new StringBuilder();
            var value = BitConverter.ToUInt64(bytes, 0);

            while (value > 0)
            {
                result.Insert(0, Base62Chars[(int)(value % 62)]);
                value /= 62;
            }

            return result.ToString();
        }
    }
}
