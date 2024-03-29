﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Checkout.V3
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReceiptStatus
    {
        [EnumMember(Value = ReceiptStatusEx.PendingStatus)]
        Pending,

        [EnumMember(Value = ReceiptStatusEx.SucceededStatus)]
        Succeeded,

        [EnumMember(Value = ReceiptStatusEx.CanceledStatus)]
        Canceled
    }

    internal static class ReceiptStatusEx
    {
        public const string PendingStatus = "pending";
        public const string SucceededStatus = "succeeded";
        public const string CanceledStatus = "canceled";

        public static string ToText(this ReceiptStatus status)
        {
            return status switch
            {
                ReceiptStatus.Pending => PendingStatus,
                ReceiptStatus.Succeeded => SucceededStatus,
                ReceiptStatus.Canceled => CanceledStatus,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
