using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    internal class ReceiptInformationResponse
    {
        public List<ReceiptInformation> Items { get; set; } = new();

        public string NextCursor { get; set; }
    }
}
