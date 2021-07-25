using System;

namespace Yandex.Checkout.V3
{
    public class Deal : NewDeal
    {
        public string Id { get; set; }
        
        public Amount Balance { get; set; }
        
        public DealStatus Status { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? ExpiresAt { get; set; }
        
        public bool? Test { get; set; }
    }
}
