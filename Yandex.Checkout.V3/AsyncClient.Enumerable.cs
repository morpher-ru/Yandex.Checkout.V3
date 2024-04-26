using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Yandex.Checkout.V3
{
    public partial class AsyncClient
    {
        /// <summary>
        /// Query receipts
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_receipts_list
        /// </remarks>
        public IAsyncEnumerable<Receipt> GetReceiptsAsync(
            GetReceiptsFilter filter = null,
            CancellationToken ct = default,
            string idempotenceKey = default)
        {
            return GetListAsync<Receipt>("receipts", filter, ct, idempotenceKey);
        }

        /// <summary>
        /// Query refunds by search criteria
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_refunds_list
        /// </remarks>
        public IAsyncEnumerable<Refund> GetRefundsAsync(
            RefundFilter filter,
            CancellationToken ct = default,
            string idempotenceKey = default)
        {
            return GetListAsync<Refund>("refunds", filter, ct, idempotenceKey);
        }

        private async IAsyncEnumerable<T> GetListAsync<T>(
            string path,
            object filter,
            [EnumeratorCancellation] CancellationToken cancellationToken,
            string idempotenceKey)
        {
            string cursor = null;

            do
            {
                var batch = await QueryAsync<ListBatch<T>>(
                    HttpMethod.Get, 
                    body: null,
                    UrlHelper.MakeUrl(path, filter, cursor),
                    idempotenceKey,
                    cancellationToken);

                foreach (T item in batch.Items)
                {
                    yield return item;
                }

                cursor = batch.NextCursor;
                
            } while (!string.IsNullOrEmpty(cursor));
        }
    }
}
