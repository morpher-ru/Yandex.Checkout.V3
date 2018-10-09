using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Error
    {
        public string type { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string parameter { get; set; }
    }
}
