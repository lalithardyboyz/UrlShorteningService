using System;
using System.Text;
using System.Threading.Tasks;
using UrlShorteningWithLibrary.Data;
using UrlShorteningWithLibrary.Repository;

namespace UrlShorteningWithLibrary.Service
{
    public class UrlShorteningService : IUrlShorteningService
    {
        public UrlShorteningService(IShorteningRepository shorteningRepository)
        {
            ShorteningRepository = shorteningRepository;
        }

        private IShorteningRepository ShorteningRepository { get; set; }

        public async Task<string> GetOriginalUrl(string shortUrl)
        {
            int id = 0;
            char[] shortURLArr = shortUrl.ToCharArray();

            // conversion from short url to id. this id will be used to retrieve the original url from database.
            for (int i = 0; i < shortURLArr.Length; i++)
            {
                if (shortURLArr[i] >= 'a' && shortURLArr[i] <= 'z')
                    id = id * 62 + shortURLArr[i] - 'a';
                else if (shortURLArr[i] >= 'A' && shortURLArr[i] <= 'Z')
                    id = id * 62 + shortURLArr[i] - 'A' + 26;
                else if(shortURLArr[i] >= '0' && shortURLArr[i] <= '9')
                    id = id * 62 + shortURLArr[i] - '0' + 52;
            }

            UrlShorteningDetails urlShorteningDetails= await ShorteningRepository.GetLongUrlById(id);
            return urlShorteningDetails == null ? "Not Found" : urlShorteningDetails.LongUrl;
        }

        public async Task<string> GetShortUrl(string longUrl)
        {
            try
            {
                UrlShorteningDetails urlShorteningDetails = await ShorteningRepository.GetShortUrlBylongUrl(longUrl.Trim());
                if (urlShorteningDetails != null)
                {
                    return Map62Url(urlShorteningDetails.Id);
                }
                else
                {
                    //Insert Long URL in DB
                    urlShorteningDetails = new UrlShorteningDetails()
                    {
                        LongUrl = longUrl,
                        DateCreated = DateTime.Now.ToLocalTime(),
                        DateExpiry = DateTime.Now.Date.AddDays(30),
                        Active = "Y"
                    };

                    ShorteningRepository.Save(urlShorteningDetails);
                    return urlShorteningDetails.Id > 0 ? Map62Url(urlShorteningDetails.Id) : throw new InvalidOperationException();
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private string Map62Url(long id)
        {
            // Map to store 62 possible characters  
            char[] map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            StringBuilder shorturl = new StringBuilder();

            //Convert given integer id to a base 62 number
            while (id > 0)
            {
                // use above map to store actual character in short url  
                shorturl.Append(map[(int)id % 62]);
                id = id / 62;
            }

            // Reverse shortURL to complete base conversion 
            char[] charArray = shorturl.ToString().ToCharArray();
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
    }
}
