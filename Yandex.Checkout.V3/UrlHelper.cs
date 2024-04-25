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
            var pairs = dict.Select(p => $"{p.Key}={p.Value.Replace("+00:00", "Z")}");
            string query = string.Join("&", pairs);
            return query;
        }
    }
}
