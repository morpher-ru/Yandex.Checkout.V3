using System.Collections.Generic;
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Yandex.Checkout.V3
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class ReceiptInformationResponse
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public List<ReceiptInformation> Items { get; set; } = new();

        public string NextCursor { get; set; }
    }
}
