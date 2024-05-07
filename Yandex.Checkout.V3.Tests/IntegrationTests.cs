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

        private readonly RefundFilter _refundFilter = new()
        {
            CreatedAt = new DateFilter
            {
                Gte = new DateTimeOffset(2000, 01, 01, 00, 00, 00, TimeSpan.Zero)
            }
        };
        
        [TestMethod]
        public async Task GetReceiptsAsync()
        {
            var receipts = _client.MakeAsync().GetReceiptsAsync();

            var list = await receipts.ToListAsync();
            
            Assert.IsNotNull(list);
            
            Console.WriteLine($"{list.Count} receipts");
        }
        
        readonly Client _client = new(
            shopId: "501156",
            secretKey: "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");
    }
}
