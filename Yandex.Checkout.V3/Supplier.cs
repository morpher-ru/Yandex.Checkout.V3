namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Информация о поставщике товара или услуги
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Наименование поставщика
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефон поставщика. Указывается в формате ITU-T E.164
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ИНН поставщика (10 или 12 цифр)
        /// </summary>
        public string Inn { get; set; }
    }
}
