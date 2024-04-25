using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class GetReceiptsFilterExtensionsTests
    {
        [TestMethod]
        public void CreateRequestUrl_EmptyFilter_EmptyQuery()
        {
            var filter = new GetReceiptsFilter();

            string url = filter.CreateRequestUrl(null);

            Assert.AreEqual("receipts", url);
        }
        
        [TestMethod]
        public void CreateRequestUrlDateTest()
        {
            var filter = new GetReceiptsFilter
            {
                CreatedAtLte = new DateTimeOffset(2024, 04, 24, 15, 30, 00, TimeSpan.Zero)
            };

            string url = filter.CreateRequestUrl(null);

            string expected = "receipts?created_at.lte=2024-04-24T15:30:00Z";
            
            Assert.AreEqual(expected, url);
        }
        
        [TestMethod]
        public void CreateRequestUrl_StatusPending()
        {
            var filter = new GetReceiptsFilter
            {
                Status = ReceiptStatus.Pending
            };

            string url = filter.CreateRequestUrl(null);

            string expected = "receipts?status=pending";
            
            Assert.AreEqual(expected, url);
        }
        
        [TestMethod]
        public void CreateRequestUrl_Limit42()
        {
            var filter = new GetReceiptsFilter
            {
                Limit = 42
            };

            string url = filter.CreateRequestUrl(null);

            string expected = "receipts?limit=42";
            
            Assert.AreEqual(expected, url);
        }
    }
}
