using System;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Содержит фильтры по времени создания и свойство Limit.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Фильтр по времени создания: время должно быть больше указанного значения или равно ему («с такого-то момента включительно»)
        /// </summary>
        [JsonProperty("created_at.gte")]
        public DateTimeOffset? CreatedAtGte { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть больше указанного значения («с такого-то момента, не включая его»)
        /// </summary>
        [JsonProperty("created_at.gt")]
        public DateTimeOffset? CreatedAtGt { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть меньше указанного значения или равно ему («по такой-то момент включительно»)
        /// </summary>
        [JsonProperty("created_at.lte")]
        public DateTimeOffset? CreatedAtLte { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть меньше указанного значения («по такой-то момент, не включая его»)
        /// </summary>
        [JsonProperty("created_at.lt")]
        public DateTimeOffset? CreatedAtLt { get; set; }

        /// <summary>
        /// Размер выдачи результатов запроса — количество объектов, передаваемых в ответе. Возможные значения: от 1 до 100
        /// </summary>
        public int? Limit { get; set; }
    }
}
