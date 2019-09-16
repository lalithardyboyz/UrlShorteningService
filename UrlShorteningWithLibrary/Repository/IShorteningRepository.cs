using System.Threading.Tasks;
using UrlShorteningWithLibrary.Data;

namespace UrlShorteningWithLibrary.Repository
{
    public interface IShorteningRepository
    {
        Task<UrlShorteningDetails> GetLongUrlById(int id);
        Task<UrlShorteningDetails> GetShortUrlBylongUrl(string longUrl);
        void Save(UrlShorteningDetails urlShorteningDetails);
    }
}

