using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SettlementType
    {
        /// <summary>
        /// Безналичный расчет
        /// </summary>
        [EnumMember(Value = "cashless")] Cashless,

        /// <summary>
        /// Предоплата (аванс)
        /// </summary>
        [EnumMember(Value = "prepayment")] Prepayment,

        /// <summary>
        /// Постоплата (кредит)
        /// </summary>
        [EnumMember(Value = "postpayment")] Postpayment,

        /// <summary>
        /// Встречное предоставление
        /// </summary>
        [EnumMember(Value = "consideration")] Consideration,

        /// <summary>
        /// Выплата
        /// </summary>
        [EnumMember(Value = "payout")] Payout
    }
}
