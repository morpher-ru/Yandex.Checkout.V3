using System;
using System.Net;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Represents Yandex.Checkout API error responce
    /// </summary>
    [Serializable]
    public class YandexCheckoutException : Exception
    {
        /// <summary>
        /// Status code returned from server
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Error object returned from server
        /// </summary>
        public Error Error { get; }

        public YandexCheckoutException(HttpStatusCode statusCode, Error error) : base(error.Description)
        {
        	StatusCode = statusCode;
        	Error = error;
        }
    }
}
