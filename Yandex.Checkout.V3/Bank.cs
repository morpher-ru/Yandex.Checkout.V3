namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Банк получателя.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Название банка получателя.
        /// Обязательный параметр.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// БИК банка получателя.
        /// Обязательный параметр.
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// Счет получателя в банке.
        /// Обязательный параметр.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Корреспондентский счет банка получателя.
        /// Обязательный параметр.
        /// </summary>
        public string CorrespondentAccount { get; set; }
    }
}
