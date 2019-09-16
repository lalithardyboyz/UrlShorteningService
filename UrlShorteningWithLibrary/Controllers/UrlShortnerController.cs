using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorteningWithLibrary.Model;
using UrlShorteningWithLibrary.Service;

namespace UrlShorteningWithLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortnerController : ControllerBase
    {
        public UrlShortnerController(IUrlShorteningService urlShorteningService)
        {
            UrlShorteningService = urlShorteningService;
        }

        private IUrlShorteningService UrlShorteningService { get; set; }

        [HttpPost("GetShortUrl")]
        public async Task<string> GetShortUrl(LongUrlModel longUrl)
        {
            if (!longUrl.LongUrl.Contains("http://") && !longUrl.LongUrl.Contains("https://"))
            {
                longUrl.LongUrl = "http://" + longUrl.LongUrl;
            }

            try
            {
                var request = WebRequest.Create(longUrl.LongUrl) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        return await UrlShorteningService.GetShortUrl(longUrl.LongUrl);
                    else
                        throw new UriFormatException();
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
        [HttpPost("GetOriginalUrl")]
        public async Task<string> GetOriginalUrl(ShortUrlModel shortUrl)
        {
            return await UrlShorteningService.GetOriginalUrl(shortUrl.ShortUrl);
        }
    }
}
