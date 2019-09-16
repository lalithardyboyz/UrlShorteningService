using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShorteningWithLibrary.Data;

namespace UrlShorteningWithLibrary.Repository
{
    public class ShorteningRepository : IShorteningRepository
    {
        private readonly ShortnerDbContext shortnerDbContext;

        public ShorteningRepository(ShortnerDbContext shortnerDbContext)
        {
            this.shortnerDbContext = shortnerDbContext;
        }

        /// <summary>
        /// To Get the long url stored in db against short url by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UrlShorteningDetails> GetLongUrlById(int id)
        {
            return await shortnerDbContext.UrlShorteningDetails.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UrlShorteningDetails> GetShortUrlBylongUrl(string longUrl)
        {
            return await shortnerDbContext.UrlShorteningDetails.FirstOrDefaultAsync(x => x.LongUrl == longUrl);
        }

        public void Save(UrlShorteningDetails urlShorteningDetails)
        {
            shortnerDbContext.UrlShorteningDetails.Add(urlShorteningDetails);
            shortnerDbContext.SaveChanges();
        }
    } 
}
