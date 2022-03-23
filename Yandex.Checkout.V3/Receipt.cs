using System;
using System.Collections.Generic;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Чек
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Информация о пользователе. Необходимо указать как минимум контактные данные:
        /// электронную почту (customer.email <see cref="V3.Customer.Email"/>) или 
        /// номер телефона (customer.phone <see cref="V3.Customer.Phone"/>).
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Телефон пользователя для отправки чека. Указывается в формате ITU-T E.164, например 79000000000. 
        /// </summary>
        [Obsolete("Данные рекомендуется передавать в параметре receipt.customer.phone.")]
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта пользователя для отправки чека.
        /// </summary>
        [Obsolete("Данные рекомендуется передавать в параметре")]
        public string Email { get; set; }

        /// <summary>
        /// Пoзиции чека, <see cref="ReceiptItem"/>
        /// </summary>
        public List<ReceiptItem> Items { get; set; } = new();

        /// <summary>
        /// Система налогообложения, <see cref="TaxSystem"/>
        /// </summary>
        public TaxSystem? TaxSystemCode { get; set; }
    }
}
