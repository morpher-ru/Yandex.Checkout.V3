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
            ListOptions options = null)
        {
            return GetListAsync<Receipt>("receipts", filter, options, GetCancellationToken(options));
        }

        /// <summary>
        /// Query refunds by search criteria
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_refunds_list
        /// </remarks>
        public IAsyncEnumerable<Refund> GetRefundsAsync(
            RefundFilter filter,
            ListOptions options = null)
        {
            return GetListAsync<Refund>("refunds", filter, options, GetCancellationToken(options));
        }
        
        /// <summary>
        /// Query payments by search criteria
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_payments_list
        /// </remarks>
        public IAsyncEnumerable<Payment> GetPaymentsAsync(
            PaymentFilter filter = null,
            ListOptions options = null)
        {
            return GetListAsync<Payment>("payments", filter, options, GetCancellationToken(options));
        }

        /// <summary>
        /// Query deals by given search criteria
        /// </summary>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_deals_list
        /// </remarks>
        public IAsyncEnumerable<Deal> GetDealsAsync(
            DealFilter filter = null,
            ListOptions options = null)
        {
            return GetListAsync<Deal>("deals", filter, options, GetCancellationToken(options));
        }

        private static CancellationToken GetCancellationToken(ListOptions options)
        {
            return options?.CancellationToken ?? CancellationToken.None;
        }

        private async IAsyncEnumerable<T> GetListAsync<T>(string path,
            object filter,
            ListOptions options,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            string cursor = null;

            do
            {
                var batch = await QueryAsync<ListBatch<T>>(
                    HttpMethod.Get, 
                    body: null,
                    UrlHelper.MakeUrl(path, filter, cursor, options?.PageSize),
                    options?.IdempotenceKey,
                    cancellationToken);

                foreach (T item in batch.Items)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }

                cursor = batch.NextCursor;
                
            } while (!string.IsNullOrEmpty(cursor));
        }
    }
}
