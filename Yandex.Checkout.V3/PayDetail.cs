using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class PayDetail
    { 
     public string id { get; set; }
     public string status { get; set; }
     public bool paid { get; set; }
     public Amount amount { get; set; }
     public string created_at { get; set; }
     public string expires_at { get; set; }
     public Payment_Method payment_method { get; set; }
    }
}
