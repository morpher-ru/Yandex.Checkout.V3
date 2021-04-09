namespace Yandex.Checkout.V3
{
    public class Error
    {
        public string Type { get; init; }
        public string Id { get; init; }
        public string Code { get; init; }
        public string Description { get; init; }
        public string Parameter { get; init; }
    }
}
