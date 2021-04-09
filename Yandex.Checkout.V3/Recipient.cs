namespace Yandex.Checkout.V3
{
    public class Recipient
    {
        /// <summary>
        /// Идентификатор магазина в Яндекс.Кассе
        /// </summary>
        public string AccountId { get; init; }

        public string GatewayId { get; init; }
    }
}
