using System;
using System.Collections.Generic;
using System.Linq;

namespace Yandex.Checkout.V3
{
    static class UrlHelper
    {
        public static string MakeUrl(string path, object filter, string cursor)
        {
            string query = ToQueryString(filter, cursor);
            var parts = new[] { path, query }.Select(s => !string.IsNullOrEmpty(s));
            return string.Join("?", parts);
        }

        public static string ToQueryString(object filter, string cursor)
        {
            string json = Serializer.SerializeObject(filter ?? new object());
            var dict = Serializer.DeserializeObject<Dictionary<string, string>>(json);
            if (cursor != null) dict.Add("cursor", cursor);
            var pairs = dict.Select(EncodePair);
            string query = string.Join("&", pairs);
            return query;
        }

        private static string EncodePair(KeyValuePair<string, string> p)
        {
            string key = UrlEncode(p.Key);
            string value = UrlEncode(FixDates(p.Value));
            return $"{key}={value}";
        }

        private static string UrlEncode(string s)
        {
            return Uri.EscapeDataString(s);
        }

        private static string FixDates(string dateStr)
        {
            return dateStr.Replace("+00:00", "Z");
        }
    }
}
