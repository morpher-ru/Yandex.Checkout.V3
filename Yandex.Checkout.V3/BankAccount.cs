﻿namespace Yandex.Checkout.V3
{
    public class BankAccount : Receiver
    {
        /// <summary>
        /// Номер банковского счета. Формат — 20 символов.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Банковский идентификационный код (БИК) банка, в котором открыт счет. Формат — 9 символов.
        /// </summary>
        public string Bic { get; set; }
    }
}
