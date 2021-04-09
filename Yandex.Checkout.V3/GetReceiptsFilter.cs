using System;
using System.Text;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Фильтр для запроса списка чеков 
    /// </summary>
    public class GetReceiptsFilter
    {
        /// <summary>
        /// Фильтр по времени создания: время должно быть больше указанного значения или равно ему («с такого-то момента включительно»)
        /// </summary>
        public DateTimeOffset? CreatedAtGte { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть больше указанного значения («с такого-то момента, не включая его»)
        /// </summary>
        public DateTimeOffset? CreatedAtGt { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть меньше указанного значения или равно ему («по такой-то момент включительно»)
        /// </summary>
        public DateTimeOffset? CreatedAtLte { get; set; }

        /// <summary>
        /// Фильтр по времени создания: время должно быть меньше указанного значения («по такой-то момент, не включая его»)
        /// </summary>
        public DateTimeOffset? CreatedAtLt { get; set; }

        /// <summary>
        /// Фильтр по статусу чека
        /// </summary>
        public ReceiptStatus? Status { get; set; }

        /// <summary>
        /// Фильтр по идентификатору платежа (получить все чеки для указанного платежа)
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Фильтр по идентификатору возврата (получить все чеки для указанного возврата)
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// Размер выдачи результатов запроса — количество объектов, передаваемых в ответе. Возможные значения: от 1 до 100
        /// </summary>
        public int? Limit { get; set; }
    }

    internal static class GetReceiptsFilterExtensions
    {
        /// <summary>
        /// Создать строку для запроса списка чеков
        /// </summary>
        /// <param name="filter"><see cref="GetReceiptsFilter"/></param>
        /// <param name="cursor">Указатель на следующий фрагмент списка</param>
        public static string CreateRequestUrl(this GetReceiptsFilter filter, string cursor)
        {
            var url = new StringBuilder();
            url.Append("receipts?");

            void AppendParam(string value, string name)
            {
                if (!string.IsNullOrEmpty(value)) 
                    url.Append($"{name}={value}&");
            }

            void AppendDate(DateTimeOffset? date, string name) => 
                AppendParam(date?.UtcDateTime.ToString("s") + "Z", name);

            AppendDate(filter?.CreatedAtGte, "created_at.gte");
            AppendDate(filter?.CreatedAtGt,  "created_at.gt");
            AppendDate(filter?.CreatedAtLte, "created_at.lte");
            AppendDate(filter?.CreatedAtLt,  "created_at.lt");
            AppendParam(filter?.Status?.ToText(), "status");
            AppendParam(filter?.PaymentId, "payment_id");
            AppendParam(filter?.RefundId, "refund_id");
            AppendParam(filter?.Limit?.ToString(), "limit");
            AppendParam(cursor, "cursor");

            url.Length--; // remove last ? or &
            
            return url.ToString();
        }
    }
}
