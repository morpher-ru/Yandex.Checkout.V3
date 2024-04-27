using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    [Ignore("Comment out this line and run manually")]
    public class IntegrationTests
    {
        [TestMethod]
        public void GetRefunds()
        {
            var refunds = _client.GetRefunds(_refundFilter).ToList();
            
            Assert.IsNotNull(refunds);
        }

        [TestMethod]
        public async Task GetRefundsAsync()
        {
            var refunds = _client.MakeAsync().GetRefundsAsync(_refundFilter);
            
            Assert.IsNotNull(await refunds.ToListAsync());
        }

        private readonly RefundFilter _refundFilter = new RefundFilter
        {
            CreatedAt = new DateFilter
            {
                Gte = new DateTimeOffset(2000, 01, 01, 00, 00, 00, TimeSpan.Zero)
            }
        };
        
        readonly Client _client = new Client(
            shopId: "501156",
            secretKey: "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");
    }
}
