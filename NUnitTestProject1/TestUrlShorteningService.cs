using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UrlShorteningWithLibrary.Data;
using UrlShorteningWithLibrary.Repository;
using UrlShorteningWithLibrary.Service;

namespace Tests
{
    [TestFixture]
    public class TestUrlShorteningService
    {
        ShortnerDbContext shortnerDbContext;
        IShorteningRepository shorteningRepository;
        UrlShorteningService urlShorteningService;

        [SetUp]
        public void Setup()
        {
            shortnerDbContext = new ShortnerDbContext(new DbContextOptions<ShortnerDbContext>());
            shorteningRepository = new ShorteningRepository(shortnerDbContext);
            urlShorteningService = new UrlShorteningService(shorteningRepository);
        }

        [Test]
        public void GetShortUrl_InputLongUrl_ShouldNotBeEmpty()
        {
            string longUrl = "";
            Assert.AreEqual(longUrl, string.Empty);
            Assert.Pass();
        }

        [Test]
        public void GetShortUrl_ReturnedShortUrl_ShouldNotBeEmptyOrNull()
        {
            string longUrl = "https://fitsmallbusiness.com/free-domain-name/";
            string ShortUrl = null;
            Assert.True(string.IsNullOrEmpty(ShortUrl));
            Assert.Pass();
        }

        [Test]
        public async Task GetShortUrl_ShortenedUrl_ShouldBeShorterThanInputLongUrl()
        {
            string longUrl = "https://fitsmallbusiness.com/free-domain-name/";
            string ShortUrl = await urlShorteningService.GetShortUrl(longUrl);
            Assert.Greater(longUrl.Length, ShortUrl.Length);
            Assert.Pass();
        }

        [Test]
        public async Task GetShortUrl_InputLongUrl_ShouldBeSame_ReverseShortUrl()
        {
            string longUrl = "https://fitsmallbusiness.com/free-domain-name/";
            string ShortUrl = await urlShorteningService.GetShortUrl(longUrl);

            string originalUrl = await urlShorteningService.GetOriginalUrl(ShortUrl);
            Assert.AreEqual(longUrl, originalUrl);
            Assert.Pass();
        }
    }
}