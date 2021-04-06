using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    internal class ReceiptInformationResponse
    {
        public List<ReceiptInformation> Items { get; set; } = new List<ReceiptInformation>();

        public string NextCursor { get; set; }
    }
}
