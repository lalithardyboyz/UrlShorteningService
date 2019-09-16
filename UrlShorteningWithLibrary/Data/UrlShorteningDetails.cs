using System;

namespace UrlShorteningWithLibrary.Data
{
    public class UrlShorteningDetails
    {
        /// <summary>
        /// Gets or sets the Id of the URL stored in DB.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Long Url stored in DB.
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Gets or sets the Date Created stored in DB.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date Expiry stored in DB.
        /// </summary>
        public DateTime DateExpiry { get; set; }

        /// <summary>
        /// Gets or sets the Active stored in DB.
        /// </summary>
        public string Active { get; set; }
    }
}
