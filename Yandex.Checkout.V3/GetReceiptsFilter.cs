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

        /// <summary>
        /// Указатель на следующий фрагмент списка
        /// </summary>
        internal string Cursor { get; set; }

        internal string CreateRequestUrl()
        {
            var url = new StringBuilder();
            url.Append("receipts?");

            if (CreatedAtGte.HasValue)
            {
                url.Append("created_at.gte=");
                AppendTime(CreatedAtGte.Value);
                url.Append("&");
            }

            if (CreatedAtGt.HasValue)
            {
                url.Append("created_at.gt=");
                AppendTime(CreatedAtGt.Value);
                url.Append("&");
            }

            if (CreatedAtLte.HasValue)
            {
                url.Append("created_at.lte=");
                AppendTime(CreatedAtLte.Value);
                url.Append("&");
            }

            if (CreatedAtLt.HasValue)
            {
                url.Append("created_at.lt=");
                AppendTime(CreatedAtLt.Value);
                url.Append("&");
            }

            if (Status.HasValue)
            {
                url.Append("status=");
                url.Append(Status.Value.ToText());
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(PaymentId))
            {
                url.Append("payment_id=");
                url.Append(PaymentId);
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(RefundId))
            {
                url.Append("refund_id=");
                url.Append(RefundId);
                url.Append("&");
            }

            if (Limit.HasValue)
            {
                url.Append("limit=");
                url.Append(Limit.Value);
                url.Append("&");
            }

            if (!string.IsNullOrEmpty(Cursor))
            {
                url.Append("cursor=");
                url.Append(Cursor);
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
