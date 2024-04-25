using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class GetReceiptsFilterExtensionsTests
    {
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
    }
}
