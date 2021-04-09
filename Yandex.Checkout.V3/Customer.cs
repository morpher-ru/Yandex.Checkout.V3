namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Для юрлица — название организации, для ИП и физического лица — ФИО.
        /// </summary>
        public string FullName { get; init; }

        /// <summary>
        /// ИНН пользователя (10 или 12 цифр).
        /// </summary>
        public string Inn { get; init; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Телефон пользователя. Указывается в формате ITU-T E.164, например 79000000000.
        /// </summary>
        public string Phone { get; init; }
    }
}
