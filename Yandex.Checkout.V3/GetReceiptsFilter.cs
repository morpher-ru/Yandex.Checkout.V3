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
        internal static string CreateRequestUrl(this GetReceiptsFilter filter, string cursor)
        {
            var url = new StringBuilder();
            url.Append("receipts?");

            if (filter?.CreatedAtGte != null)
            {
                url.Append("created_at.gte=");
                AppendTime(filter.CreatedAtGte.Value);
                url.Append("&");
            }

            if (filter?.CreatedAtGt != null)
            {
                url.Append("created_at.gt=");
                AppendTime(filter.CreatedAtGt.Value);
                url.Append("&");
            }

            if (filter?.CreatedAtLte != null)
            {
                url.Append("created_at.lte=");
                AppendTime(filter.CreatedAtLte.Value);
                url.Append("&");
            }

            if (filter?.CreatedAtLt != null)
            {
                url.Append("created_at.lt=");
                AppendTime(filter.CreatedAtLt.Value);
                url.Append("&");
            }

            if (filter?.Status != null)
            {
                url.Append("status=");
                url.Append(filter.Status.Value.ToText());
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(filter?.PaymentId))
            {
                url.Append("payment_id=");
                url.Append(filter.PaymentId);
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(filter?.RefundId))
            {
                url.Append("refund_id=");
                url.Append(filter.RefundId);
                url.Append("&");
            }

            if (filter?.Limit != null)
            {
                url.Append("limit=");
                url.Append(filter.Limit.Value);
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(cursor))
            {
                url.Append("cursor=");
                url.Append(cursor);
                url.Append("&");
            }

            url.Length--; // remove last ? or &
            return url.ToString();

            void AppendTime(DateTimeOffset dt)
            {
                url.Append(dt.UtcDateTime.ToString("s"));
                url.Append("Z");
            }
        }
    }
}
