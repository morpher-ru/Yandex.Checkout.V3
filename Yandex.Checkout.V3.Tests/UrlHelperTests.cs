using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class UrlHelperTests
    {
        [TestMethod]
        public void CreateRequestUrl_NullFilter_EmptyQuery()
        {
            string queryString = UrlHelper.ToQueryString(filter: null, cursor: null);

            Assert.AreEqual("", queryString);
        }
        
        [TestMethod]
        public void CreateRequestUrl_NullFilterCursor_Cursor()
        {
            string queryString = UrlHelper.ToQueryString(filter: null, cursor: "123");

            Assert.AreEqual("cursor=123", queryString);
        }
        
        [TestMethod]
        public void CreateRequestUrl_EmptyFilter_EmptyQuery()
        {
            var filter = new ReceiptFilter();

            string queryString = UrlHelper.ToQueryString(filter, null);

            Assert.AreEqual("", queryString);
        }
        
        [TestMethod]
        public void CreateRequestUrlDateTest()
        {
            var filter = new ReceiptFilter
            {
                CreatedAt = new DateFilter
                {
                    Lte = new DateTimeOffset(2024, 04, 24, 15, 30, 00, TimeSpan.Zero)
                }
            };

            string queryString = UrlHelper.ToQueryString(filter, null);

            string expected = "created_at.lte=2024-04-24T15%3A30%3A00Z";
            
            Assert.AreEqual(expected, queryString);
        }
        
        [TestMethod]
        public void CreateRequestUrl_StatusPending()
        {
            var filter = new ReceiptFilter
            {
                Status = ReceiptStatus.Pending
            };

            string queryString = UrlHelper.ToQueryString(filter, null);

            string expected = "status=pending";
            
            Assert.AreEqual(expected, queryString);
        }
        
        [TestMethod]
        public void CreateRequestUrl_Limit42()
        {
            string queryString = UrlHelper.ToQueryString(null, null, 42);

            string expected = "limit=42";
            
            Assert.AreEqual(expected, queryString);
        }

        [TestMethod]
        public void JoinPathAndQuery_EmptyQuery()
        {
            Assert.AreEqual("path", UrlHelper.JoinPathAndQuery("path", ""));
        }

        [TestMethod]
        public void JoinPathAndQuery_NonEmptyQuery()
        {
            Assert.AreEqual("path?query", UrlHelper.JoinPathAndQuery("path", "query"));
        }
    }
}
