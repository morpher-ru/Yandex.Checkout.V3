using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Checkout.V3
{
    public class AsyncClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        internal AsyncClient(
            string authorization,
            string apiUrl,
            string userAgent)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);

            if (!string.IsNullOrEmpty(userAgent))
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        }

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Payment>(HttpMethod.Post, payment, "payments", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/capture", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes to the payment use <see cref="CapturePaymentAsync(string,string,System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(Payment payment, string idempotenceKey = null, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Payment>(HttpMethod.Post, payment, $"payments/{payment.Id}/capture", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> QueryPaymentAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Payment>(HttpMethod.Get, null, $"payments/{id}", null, cancellationToken);

        /// <summary>
        /// Payment cancellation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CancelPaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/cancel", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <summary>
        /// Refund creation
        /// </summary>
        /// <param name="refund">Refund data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> CreateRefundAsync(NewRefund refund, string idempotenceKey = null, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Refund>(HttpMethod.Post, refund, "refunds", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <summary>
        /// Query refund
        /// </summary>
        /// <param name="id">Refund id</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> QueryRefundAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
            => QueryAsync<Refund>(HttpMethod.Post, null, $"refunds/{id}", null, cancellationToken);

        private async Task<T> QueryAsync<T>(HttpMethod method, object body, string url, string idempotenceKey, CancellationToken cancellationToken)
        {
            using (var request = CreateRequest(method, body, url, idempotenceKey))
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
            var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                request.Content = new StringContent(Serializer.SerializeObject(body), Encoding.UTF8, Client.ApplicationJson);
            }

            if (!string.IsNullOrEmpty(idempotenceKey))
                request.Headers.Add("Idempotence-Key", idempotenceKey);
            
            return request;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
