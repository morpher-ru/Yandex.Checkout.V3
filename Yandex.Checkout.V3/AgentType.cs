using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Тип посредника
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AgentType
    {
        /// <summary>
        /// Банковский платежный агент
        /// </summary>
        [EnumMember(Value = "banking_payment_agent")]
        banking_payment_agent,

        /// <summary>
        /// Банковский платежный субагент
        /// </summary>
        [EnumMember(Value = "banking_payment_subagent")]
        banking_payment_subagent,

        /// <summary>
        /// Платежный агент
        /// </summary>
        [EnumMember(Value = "payment_agent")]
        payment_agent,

        /// <summary>
        /// Платежный субагент
        /// </summary>
        [EnumMember(Value = "payment_subagent")]
        payment_subagent,

        /// <summary>
        /// Поверенный
        /// </summary>
        [EnumMember(Value = "attorney")]
        attorney,

        /// <summary>
        /// Комиссионер
        /// </summary>
        [EnumMember(Value = "commissioner")]
        commissioner,

        /// <summary>
        /// Агент
        /// </summary>
        [EnumMember(Value = "agent")]
        agent
    }
}
