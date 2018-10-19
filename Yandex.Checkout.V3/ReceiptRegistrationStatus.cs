using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Состояние отправки чека по 54-ФЗ
    /// </summary>
    public enum ReceiptRegistrationStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "succeeded")]
        Succeeded,
        [EnumMember(Value = "canceled")]
        Canceled
    }
}
