﻿using System.Collections.Generic;
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
        public async IAsyncEnumerable<ReceiptInformation> GetReceiptsAsync(GetReceiptsFilter filter,
            [EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            if (filter == null)
            {
                filter = new GetReceiptsFilter();
            }

            do
            {
                var batch = await QueryAsync<ReceiptInformationResponse>(HttpMethod.Get, null, filter.CreateRequestUrl(), null, cancellationToken);

                foreach (var item in batch.Items)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }

                filter.Cursor = batch.NextCursor;
            } while (!string.IsNullOrEmpty(filter.Cursor));
        }
    }
}
