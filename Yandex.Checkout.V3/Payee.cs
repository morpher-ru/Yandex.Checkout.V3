namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Получатель платежа — государственная или коммерческая организация, 
    /// которая предоставляет услуги или является информационным посредником, 
    /// который собирает и обрабатывает начисления от других поставщиков услуг.
    /// </summary>
    public class Payee
    {
        /// <summary>
        /// Название получателя.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// ИНН.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public string Inn { get; set; }

        /// <summary>
        /// КПП.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public string Kpp { get; set; }

        /// <summary>
        /// Банк получателя.
        /// </summary>
        /// <remarks>
        /// Обязательный параметр.
        /// </remarks>
        public Bank Bank { get; set; }
    }
}
