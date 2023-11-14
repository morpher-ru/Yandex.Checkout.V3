using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SettlementReceiptType
    {
        /// <summary>
        /// Приход
        /// </summary>
        [EnumMember(Value = "payment")] Payment,

        /// <summary>
        /// Возврат прихода
        /// </summary>
        [EnumMember(Value = "refund")] Refund
    }
}
