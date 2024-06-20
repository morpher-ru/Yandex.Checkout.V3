using System.Runtime.CompilerServices;

namespace Yandex.Checkout.V3;

public partial class AsyncClient
{
    /// <summary>
    /// Query receipts
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_receipts_list
    /// </remarks>
    public IAsyncEnumerable<Receipt> GetReceiptsAsync(
        ReceiptFilter filter = null,
        CancellationToken ct = default,
        ListOptions options = null)
    {
        return GetListAsync<Receipt>("receipts", filter, options, ct);
    }

    /// <summary>
    /// Query refunds by search criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_refunds_list
    /// </remarks>
    public IAsyncEnumerable<Refund> GetRefundsAsync(
        RefundFilter filter = null,
        CancellationToken ct = default,
        ListOptions options = null)
    {
        return GetListAsync<Refund>("refunds", filter, options, ct);
    }
        
    /// <summary>
    /// Query payments by search criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_payments_list
    /// </remarks>
    public IAsyncEnumerable<Payment> GetPaymentsAsync(
        PaymentFilter filter = null,
        CancellationToken ct = default,
        ListOptions options = null)
    {
        return GetListAsync<Payment>("payments", filter, options, ct);
    }

    /// <summary>
    /// Query deals by given search criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_deals_list
    /// </remarks>
    public IAsyncEnumerable<Deal> GetDealsAsync(
        DealFilter filter = null,
        CancellationToken ct = default,
        ListOptions options = null)
    {
        return GetListAsync<Deal>("deals", filter, options, ct);
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
                null,
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
