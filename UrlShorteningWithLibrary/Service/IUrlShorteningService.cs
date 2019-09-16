using System.Threading.Tasks;

namespace UrlShorteningWithLibrary.Service
{
    public interface IUrlShorteningService
    {
        Task<string> GetShortUrl(string longUrl);
        Task<string> GetOriginalUrl(string shortUrl);
    }
}
