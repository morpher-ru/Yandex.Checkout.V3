using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.Checkout.V3
{

    [Serializable]
    public class YandexCheckoutException : Exception
    {
        public int StatusCode { get; }
        public Error Error { get; }
        
        public YandexCheckoutException(int statusCode, Error error) : this(error.description)
        {
        	StatusCode = statusCode;
        	Error = error;
        }
        public YandexCheckoutException(string message) : base(message) { }
        public YandexCheckoutException(string message, Exception inner) : base(message, inner) { }
        protected YandexCheckoutException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
