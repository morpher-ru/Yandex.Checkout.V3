using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Checkout.V3
{
    public partial class AsyncClient : IDisposable
    {
        readonly HttpClient _httpClient;
        readonly bool _disposeOfHttpClient;

        /// <summary>
        /// Expects the <paramref name="httpClient"/>'s BaseAddress and Authorization header to be set.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="disposeOfHttpClient">
        /// Dispose of the <paramref name="httpClient"/> when this AsyncClient is disposed.
        /// </param>
        public AsyncClient(HttpClient httpClient, bool disposeOfHttpClient = false)
        {
            _httpClient = httpClient;
            _disposeOfHttpClient = disposeOfHttpClient;
        }

        /// <summary>
        /// Deal creation
        /// </summary>
        /// <param name="newDeal">Deal information, <see cref="NewDeal"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Deal"/></returns>
        public Task<Deal> CreateDealAsync(NewDeal newDeal, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Deal>(HttpMethod.Post, newDeal, "deals", idempotenceKey, cancellationToken);

        /// <summary>
        /// Get deal by id
        /// </summary>
        /// <param name="id">Deal id, <see cref="Deal.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Deal"/></returns>
        public Task<Deal> GetDealAsync(string id, CancellationToken cancellationToken = default)
            => QueryAsync<Deal>(HttpMethod.Get, null, $"deals/{id}", null, cancellationToken);

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Payment>(HttpMethod.Post, payment, "payments", idempotenceKey, cancellationToken);

        /// <summary>
        /// Payout creation
        /// </summary>
        /// <param name="payout">Payout information, <see cref="NewPayout"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payout> CreatePayoutAsync(NewPayout payout, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Payout>(HttpMethod.Post, payout, "payouts", idempotenceKey, cancellationToken);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/capture", idempotenceKey, cancellationToken);

        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes to the payment use <see cref="CapturePaymentAsync(string,string,CancellationToken)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(Payment payment, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Payment>(HttpMethod.Post, payment, $"payments/{payment.Id}/capture", idempotenceKey, cancellationToken);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> GetPaymentAsync(string id, CancellationToken cancellationToken = default)
            => QueryAsync<Payment>(HttpMethod.Get, null, $"payments/{id}", null, cancellationToken);

        /// <summary>
        /// Payment cancellation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CancelPaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/cancel", idempotenceKey, cancellationToken);

        /// <summary>
        /// Refund creation
        /// </summary>
        /// <param name="refund">Refund data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> CreateRefundAsync(NewRefund refund, string idempotenceKey = null, CancellationToken cancellationToken = default)
            => QueryAsync<Refund>(HttpMethod.Post, refund, "refunds", idempotenceKey, cancellationToken);

        /// <summary>
        /// Query refund
        /// </summary>
        /// <param name="id">Refund id</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> GetRefundAsync(string id, CancellationToken cancellationToken = default)
            => QueryAsync<Refund>(HttpMethod.Get, null, $"refunds/{id}", null, cancellationToken);

        /// <summary>
        /// Receipt creation
        /// </summary>
        /// <param name="receipt">Receipt information, <see cref="SettlementReceipt"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="SettlementReceipt"/></returns>
        public Task<SettlementReceipt> CreateSettlementReceiptAsync(SettlementReceipt receipt, string idempotenceKey = null,
            CancellationToken cancellationToken = default)
            => QueryAsync<SettlementReceipt>(HttpMethod.Post, receipt, "receipts", idempotenceKey, cancellationToken);

        /// <summary>
        /// Query receipt
        /// </summary>
        /// <param name="id">Receipt id</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="ReceiptInformation"/></returns>
        /// <remarks>
        /// See https://yookassa.ru/developers/api#get_receipt
        /// </remarks>
        public Task<ReceiptInformation> GetReceiptAsync(string id, CancellationToken cancellationToken = default)
            => QueryAsync<ReceiptInformation>(HttpMethod.Get, null, $"receipts/{id}", null, cancellationToken);

        private async Task<T> QueryAsync<T>(HttpMethod method, object body, string url, string idempotenceKey, CancellationToken cancellationToken)
        {
            using (var request = CreateRequest(method, body, url, idempotenceKey ?? Guid.NewGuid().ToString()))
            {
                var response = await _httpClient.SendAsync(request, cancellationToken);
                using (response)
                {
                    var responseData = response.Content == null
                        ? null
                        : await response.Content.ReadAsStringAsync();

                    return Client.ProcessResponse<T>(response.StatusCode, responseData, response.Content?.Headers?.ContentType?.MediaType ?? string.Empty);
                }
            }
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, object body, string url,
            string idempotenceKey)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = method == HttpMethod.Post
                    ? new StringContent(Serializer.SerializeObject(body), Encoding.UTF8, Client.ApplicationJson)
                    : null
            };

            if (!string.IsNullOrEmpty(idempotenceKey))
                request.Headers.Add("Idempotence-Key", idempotenceKey);

            return request;
        }

        public void Dispose()
        {
            if (_disposeOfHttpClient)
                _httpClient.Dispose();
        }

        /// <summary>
        /// Parses an HTTP request into a <see cref="Yandex.Checkout.V3.Message"/> object.
        /// </summary>
        /// <returns>A <see cref="Yandex.Checkout.V3.Message"/> object or null.</returns>
        public static async Task<Message> ParseMessageAsync(string requestHttpMethod, string requestContentType, Stream requestInputStream)
        {
            return Client.ParseMessage(requestHttpMethod, requestContentType, await ReadToEndAsync(requestInputStream));
        }

        private static async Task<string> ReadToEndAsync(Stream stream)
        {
            if (stream == null) return null;

            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }
    }
}
