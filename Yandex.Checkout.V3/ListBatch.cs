// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Yandex.Checkout.V3;

class ListBatch<T>
{
    public string NextCursor { get; set; }
    public List<T> Items { get; set; }
}
