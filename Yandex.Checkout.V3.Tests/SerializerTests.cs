using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Checkout.V3.Tests
{
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void ReceiptIndustryDetailsSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new ReceiptIndustryDetails {
                DocumentDate = new DateTime(2024, 05, 07, 13, 00, 01, DateTimeKind.Utc),
                DocumentNumber = "123"});
            Assert.AreEqual("{\"document_date\":\"2024-05-07T13:00:01Z\",\"document_number\":\"123\"}", s);
        }
        
        [TestMethod]
        public void RefundReceiptRegistrationDeserializedCorrectly()
        {
            var refund = Serializer.DeserializeObject<Refund>("{\"receipt_registration\":\"succeeded\", \"amount\":{\"value\":1}, \"payment_id\":1}");
            Assert.AreEqual(ReceiptRegistrationStatus.Succeeded, refund.ReceiptRegistration);
        }

        [TestMethod]
        public void RefundSucceededReceiptRegistrationSucceededSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {Status = RefundStatus.Succeeded, ReceiptRegistration = ReceiptRegistrationStatus.Succeeded });
            Assert.AreEqual("{\"status\":\"succeeded\",\"receipt_registration\":\"succeeded\"}", s);
        }
        
        [TestMethod]
        public void RefundPendingReceiptRegistrationSucceededSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {Status = RefundStatus.Pending, ReceiptRegistration = ReceiptRegistrationStatus.Succeeded });
            Assert.AreEqual("{\"status\":\"pending\",\"receipt_registration\":\"succeeded\"}", s);
        }

        [TestMethod]
        public void RefundSucceededReceiptRegistrationNullSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {Status = RefundStatus.Succeeded});
            Assert.AreEqual("{\"status\":\"succeeded\"}", s);
        }
        
        [TestMethod]
        public void RefundPendingReceiptRegistrationNullSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new Refund {Status = RefundStatus.Pending});
            Assert.AreEqual("{\"status\":\"pending\"}", s);
        }

        [TestMethod]
        public void CreatePayoutRequestSerializedCorrectly()
        {
            var s = Serializer.SerializeObject(new NewPayout {PayoutToken = "token"});
            Assert.AreEqual("{\"payout_token\":\"token\"}", s);
        }

        [TestMethod]
        public void CardPaymentMethodDeserializedCorrectly()
        {
            var card = Serializer.DeserializeObject<Card>("""
                                                          {
                                                          "first6": "555555",
                                                          "last4": "4444",
                                                          "expiry_month": "07",
                                                          "expiry_year": "2022",
                                                          "card_type": "MasterCard",
                                                          "issuer_country": "RU",
                                                          "issuer_name": "Sberbank"
                                                          }
                                                          """);
            Assert.AreEqual("555555", card.First6);
            Assert.AreEqual("4444", card.Last4);
            Assert.AreEqual("07", card.ExpiryMonth);
            Assert.AreEqual("2022", card.ExpiryYear);
            Assert.AreEqual("MasterCard", card.CardType);
            Assert.AreEqual("RU", card.IssuerCountry);
            Assert.AreEqual("Sberbank", card.IssuerName);
        }
    
        [TestMethod]
        public void SuccessCardPaymentNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""payment.succeeded"",
    ""object"": {
        ""id"": ""2d3dfafc-000f-5000-a000-1e3a62e9c166"",
        ""status"": ""succeeded"",
        ""amount"": {
            ""value"": ""120.00"",
            ""currency"": ""RUB""
        },
        ""income_amount"": {
            ""value"": ""115.80"",
            ""currency"": ""RUB""
        },
        ""description"": ""Тестовый заказ W-121215"",
        ""recipient"": {
            ""account_id"": ""286740"",
            ""gateway_id"": ""2155235""
        },
        ""payment_method"": {
            ""type"": ""bank_card"",
            ""id"": ""2d3dfafc-000f-5000-a000-1e3a62e9c166"",
            ""saved"": false,
            ""title"": ""Bank card *2987"",
            ""card"": {
                ""first6"": ""220247"",
                ""last4"": ""2987"",
                ""expiry_year"": ""2023"",
                ""expiry_month"": ""01"",
                ""card_type"": ""Mir""
            }
        },
        ""captured_at"": ""2024-01-20T15:32:21.775Z"",
        ""created_at"": ""2024-01-20T15:31:08.552Z"",
        ""test"": true,
        ""refunded_amount"": {
            ""value"": ""0.00"",
            ""currency"": ""RUB""
        },
        ""paid"": true,
        ""refundable"": true,
        ""metadata"": {},
        ""authorization_details"": {
            ""rrn"": ""239131514178272"",
            ""auth_code"": ""635290"",
            ""three_d_secure"": {
                ""applied"": false,
                ""method_completed"": false,
                ""challenge_completed"": false
            }
        }
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as PaymentSucceededNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            Payment payment = notification.Object;
            Assert.AreEqual("2d3dfafc-000f-5000-a000-1e3a62e9c166", payment.Id);
            Assert.AreEqual(PaymentStatus.Succeeded, payment.Status);
            Assert.AreEqual(120.00m, payment.Amount.Value);

            Assert.AreEqual("239131514178272", payment.AuthorizationDetails.Rrn);
            Assert.IsNotNull(payment.AuthorizationDetails.ThreeDSecure);
            Assert.AreEqual(false, payment.AuthorizationDetails.ThreeDSecure.Applied);
            Assert.AreEqual(false, payment.AuthorizationDetails.ThreeDSecure.ChallengeCompleted);
            Assert.AreEqual(false, payment.AuthorizationDetails.ThreeDSecure.MethodCompleted);
        }

        [TestMethod]
        public void SuccessCard3DsPaymentNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""payment.succeeded"",
    ""object"": {
        ""id"": ""2d3decc6-000f-5000-9000-1eb4695f2a99"",
        ""status"": ""succeeded"",
        ""amount"": {
            ""value"": ""120.00"",
            ""currency"": ""RUB""
        },
        ""income_amount"": {
            ""value"": ""115.80"",
            ""currency"": ""RUB""
        },
        ""description"": ""Тестовый заказ W-121213"",
        ""recipient"": {
            ""account_id"": ""286740"",
            ""gateway_id"": ""2155235""
        },
        ""payment_method"": {
            ""type"": ""bank_card"",
            ""id"": ""2d3decc6-000f-5000-9000-1eb4695f2a99"",
            ""saved"": false,
            ""title"": ""Bank card *0004"",
            ""card"": {
                ""first6"": ""220000"",
                ""last4"": ""0004"",
                ""expiry_year"": ""2027"",
                ""expiry_month"": ""01"",
                ""card_type"": ""Mir""
            }
        },
        ""captured_at"": ""2024-01-20T14:31:16.733Z"",
        ""created_at"": ""2024-01-20T14:30:30.616Z"",
        ""test"": true,
        ""refunded_amount"": {
            ""value"": ""0.00"",
            ""currency"": ""RUB""
        },
        ""paid"": true,
        ""refundable"": true,
        ""metadata"": {},
        ""authorization_details"": {
            ""rrn"": ""917238214791044"",
            ""auth_code"": ""147310"",
            ""three_d_secure"": {
                ""applied"": true,
                ""protocol"": ""v1"",
                ""method_completed"": false,
                ""challenge_completed"": true
            }
        }
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as PaymentSucceededNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            Payment payment = notification.Object;
            Assert.AreEqual("2d3decc6-000f-5000-9000-1eb4695f2a99", payment.Id);
            Assert.AreEqual(PaymentStatus.Succeeded, payment.Status);
            Assert.AreEqual(true, payment.Paid);
            Assert.AreEqual(true, payment.Refundable);
            Assert.AreEqual(120.00m, payment.Amount.Value);

            Assert.AreEqual("917238214791044", payment.AuthorizationDetails.Rrn);
            Assert.AreEqual("147310", payment.AuthorizationDetails.AuthCode);
            Assert.IsNotNull(payment.AuthorizationDetails.ThreeDSecure);
            Assert.AreEqual(true, payment.AuthorizationDetails.ThreeDSecure.Applied);
            Assert.AreEqual(true, payment.AuthorizationDetails.ThreeDSecure.ChallengeCompleted);
            Assert.AreEqual(false, payment.AuthorizationDetails.ThreeDSecure.MethodCompleted);
            Assert.AreEqual("v1", payment.AuthorizationDetails.ThreeDSecure.Protocol);

            //check payment method
            Assert.AreEqual("bank_card", payment.PaymentMethod.Type);
            Assert.AreEqual("Mir", payment.PaymentMethod.Card.CardType);
            Assert.AreEqual("220000", payment.PaymentMethod.Card.First6);
            Assert.AreEqual("0004", payment.PaymentMethod.Card.Last4);
            Assert.AreEqual("2027", payment.PaymentMethod.Card.ExpiryYear);
            Assert.AreEqual("01", payment.PaymentMethod.Card.ExpiryMonth);
            Assert.AreEqual("Bank card *0004", payment.PaymentMethod.Title);
        }

        [TestMethod]
        public void SuccessYooMoneyPaymentNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""payment.succeeded"",
    ""object"": {
        ""id"": ""2d3dea3c-000f-5000-9000-16ec47d0bacd"",
        ""status"": ""succeeded"",
        ""amount"": {
            ""value"": ""120.00"",
            ""currency"": ""RUB""
        },
        ""income_amount"": {
            ""value"": ""115.80"",
            ""currency"": ""RUB""
        },
        ""description"": ""Тестовый заказ W-121212"",
        ""recipient"": {
            ""account_id"": ""286740"",
            ""gateway_id"": ""2155235""
        },
        ""payment_method"": {
            ""type"": ""yoo_money"",
            ""id"": ""2d3dea3c-000f-5000-9000-16ec47d0bacd"",
            ""saved"": false,
            ""title"": ""YooMoney wallet 410011758831136"",
            ""account_number"": ""410011758831136""
        },
        ""captured_at"": ""2024-01-20T14:22:06.347Z"",
        ""created_at"": ""2024-01-20T14:19:40.591Z"",
        ""test"": true,
        ""refunded_amount"": {
            ""value"": ""0.00"",
            ""currency"": ""RUB""
        },
        ""paid"": true,
        ""refundable"": true,
        ""metadata"": {}
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as PaymentSucceededNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            var payment = notification.Object;
            Assert.AreEqual("2d3dea3c-000f-5000-9000-16ec47d0bacd", payment.Id);
            Assert.AreEqual(PaymentStatus.Succeeded, payment.Status);

            Assert.AreEqual(120.00m, payment.Amount.Value);
            Assert.AreEqual("RUB", payment.Amount.Currency);
            Assert.AreEqual(115.80m, payment.IncomeAmount.Value);
            Assert.AreEqual("RUB", payment.IncomeAmount.Currency);

            Assert.AreEqual("YooMoney wallet 410011758831136", payment.PaymentMethod.Title);
            Assert.AreEqual("yoo_money", payment.PaymentMethod.Type);
            Assert.AreEqual("410011758831136", payment.PaymentMethod.AccountNumber);
            Assert.AreEqual(true, payment.Paid);
            Assert.AreEqual(true, payment.Refundable);
        }

        [TestMethod]
        public void SuccessRefundNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""refund.succeeded"",
    ""object"": {
        ""id"": ""2d3df78f-0015-5000-9000-15b3448744a5"",
        ""payment_id"": ""2d3de338-000f-5000-9000-1a427a10fed5"",
        ""status"": ""succeeded"",
        ""created_at"": ""2024-01-20T15:16:31.461Z"",
        ""amount"": {
            ""value"": ""100.00"",
            ""currency"": ""RUB""
        },
        ""description"": ""Возврат на сайте""
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as RefundSucceededNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            Refund refund = notification.Object;
            Assert.AreEqual("2d3df78f-0015-5000-9000-15b3448744a5", refund.Id);
            Assert.AreEqual("2d3de338-000f-5000-9000-1a427a10fed5", refund.PaymentId);
            Assert.AreEqual(RefundStatus.Succeeded, refund.Status);
            Assert.AreEqual("Возврат на сайте", refund.Description);
            Assert.AreEqual(100.00m, refund.Amount.Value);
        }

        [TestMethod]
        public void CancelPaymentTimeoutNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""payment.canceled"",
    ""object"": {
        ""id"": ""2d3de3e6-000f-5000-9000-178bf0715199"",
        ""status"": ""canceled"",
        ""amount"": {
            ""value"": ""120.00"",
            ""currency"": ""RUB""
        },
        ""description"": ""Тестовый заказ W-121212"",
        ""recipient"": {
            ""account_id"": ""286740"",
            ""gateway_id"": ""2155235""
        },
        ""created_at"": ""2024-01-20T13:52:38.396Z"",
        ""test"": true,
        ""paid"": false,
        ""refundable"": false,
        ""metadata"": {},
        ""cancellation_details"": {
            ""party"": ""yoo_money"",
            ""reason"": ""expired_on_confirmation""
        }
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as PaymentCanceledNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            Payment payment = notification.Object;
            Assert.AreEqual("2d3de3e6-000f-5000-9000-178bf0715199", payment.Id);
            Assert.AreEqual(PaymentStatus.Canceled, payment.Status);
            Assert.AreEqual(120.00m, payment.Amount.Value);
            Assert.AreEqual("RUB", payment.Amount.Currency);

            Assert.AreEqual("Тестовый заказ W-121212", payment.Description);
            Assert.AreEqual(false, payment.Paid);
            Assert.AreEqual(false, payment.Refundable);

            // check cancellation details
            Assert.AreEqual("yoo_money", payment.CancellationDetails.Party);
            Assert.AreEqual("expired_on_confirmation", payment.CancellationDetails.Reason);
        }

        [TestMethod]
        public void ConfirmationTypeQRSerializedCorrectly()
        {
            var confirmation = new Confirmation
            {
                Type = ConfirmationType.QR
            };

            string json = Serializer.SerializeObject(confirmation);
            
            Assert.AreEqual("{\"type\":\"qr\"}", json);
        }

        [TestMethod]
        public void CancelPaymentManualNotificationDeserializedCorrectly()
        {
            var json = @"
{
    ""type"": ""notification"",
    ""event"": ""payment.canceled"",
    ""object"": {
        ""id"": ""2d3ddffe-000f-5000-a000-1d463cc2382d"",
        ""status"": ""canceled"",
        ""amount"": {
            ""value"": ""120.00"",
            ""currency"": ""RUB""
        },
        ""recipient"": {
            ""account_id"": ""286740"",
            ""gateway_id"": ""2155235""
        },
        ""payment_method"": {
            ""type"": ""bank_card"",
            ""id"": ""2d3ddffe-000f-5000-a000-1d463cc2382d"",
            ""saved"": false,
            ""title"": ""Bank card *4444"",
            ""card"": {
                ""first6"": ""555555"",
                ""last4"": ""4444"",
                ""expiry_year"": ""2027"",
                ""expiry_month"": ""01"",
                ""card_type"": ""MasterCard"",
                ""issuer_country"": ""US""
            }
        },
        ""created_at"": ""2024-01-20T13:35:58.316Z"",
        ""test"": true,
        ""paid"": false,
        ""refundable"": false,
        ""metadata"": {},
        ""cancellation_details"": {
            ""party"": ""merchant"",
            ""reason"": ""canceled_by_merchant""
        },
        ""authorization_details"": {
            ""rrn"": ""286354435996122"",
            ""auth_code"": ""632756"",
            ""three_d_secure"": {
                ""applied"": false,
                ""method_completed"": false,
                ""challenge_completed"": false
            }
        }
    }
}";

            var notification = Client.ParseMessage("POST", "application/json", json) as PaymentCanceledNotification;
            Assert.IsNotNull(notification);
            Assert.IsNotNull(notification.Object);

            Payment payment = notification.Object;
            Assert.AreEqual("2d3ddffe-000f-5000-a000-1d463cc2382d", payment.Id);
            Assert.AreEqual(PaymentStatus.Canceled, payment.Status);
            Assert.AreEqual(120.00m, payment.Amount.Value);
            Assert.AreEqual("RUB", payment.Amount.Currency);
            Assert.AreEqual(false, payment.Paid);
            Assert.AreEqual(false, payment.Refundable);

            // check authorisation details
            Assert.AreEqual("286354435996122", payment.AuthorizationDetails.Rrn);
            Assert.AreEqual("632756", payment.AuthorizationDetails.AuthCode);

            // check cancellation details
            Assert.AreEqual("merchant", payment.CancellationDetails.Party);
            Assert.AreEqual("canceled_by_merchant", payment.CancellationDetails.Reason);

            // check payment method
            Assert.IsNotNull(payment.PaymentMethod);
            Assert.AreEqual("bank_card", payment.PaymentMethod.Type);
            Assert.AreEqual("555555", payment.PaymentMethod.Card.First6);
            Assert.AreEqual("4444", payment.PaymentMethod.Card.Last4);
            Assert.AreEqual("2027", payment.PaymentMethod.Card.ExpiryYear);
            Assert.AreEqual("01", payment.PaymentMethod.Card.ExpiryMonth);
            Assert.AreEqual("MasterCard", payment.PaymentMethod.Card.CardType);
            Assert.AreEqual("US", payment.PaymentMethod.Card.IssuerCountry);
        }
    }
}
