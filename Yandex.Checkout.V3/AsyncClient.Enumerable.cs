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
        /// <param name="filter">Request filter parameters</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="ReceiptInformation"/></returns>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_receipts_list
        /// </remarks>
        public async IAsyncEnumerable<ReceiptInformation> GetReceiptsAsync(GetReceiptsFilter filter = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string cursor = null;
            do
            {
                var batch = await QueryAsync<ReceiptInformationResponse>(HttpMethod.Get, null, filter.CreateRequestUrl(cursor), null, cancellationToken);

                foreach (var item in batch.Items)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }

                cursor = batch.NextCursor;
            } while (!string.IsNullOrEmpty(cursor));
        }
    }
}
