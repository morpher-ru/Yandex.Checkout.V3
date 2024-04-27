using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    class ListBatch<T>
    {
        public string NextCursor { get; set; }
        public List<T> Items { get; set; }
    }
}
