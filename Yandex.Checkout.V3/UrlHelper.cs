using Newtonsoft.Json.Linq;

namespace Yandex.Checkout.V3;

static class UrlHelper
{
    public static string MakeUrl(string path, object filter, string cursor, int? pageSize)
    {
            string query = ToQueryString(filter, cursor, pageSize);
            return JoinPathAndQuery(path, query);
        }

    internal static string JoinPathAndQuery(string path, string query)
    {
            var parts = new[] { path, query }.Where(s => !string.IsNullOrEmpty(s));
            return string.Join("?", parts);
        }

    public static string ToQueryString(object filter, string cursor, int? pageSize = null)
    {
            var dict = ToStringDictionary(filter);
            if (cursor != null) dict.Add("cursor", cursor);
            if (pageSize != null) dict.Add("limit", pageSize.ToString());
            return ToQueryString(dict);
        }

    private static string ToQueryString(Dictionary<string, string> dict)
    {
            var pairs = dict.Select(EncodePair);
            string query = string.Join("&", pairs);
            return query;
        }

    private static Dictionary<string, string> ToStringDictionary(object filter)
    {
            // Serialise to JSON
            string json = Serializer.SerializeObject(filter ?? new object());
            
            // JSON to dictionary
            IDictionary<string, JToken> jObject = Serializer.DeserializeObject<JObject>(json);
            
            // Flatten inner dictionaries so {created_at: {gte: ""}} becomes {created_at.gte: ""}
            var jPairs = jObject.SelectMany(p => p.Value is IDictionary<string, JToken> d
                ? d.Select(di => new KeyValuePair<string, JToken>($"{p.Key}.{di.Key}", di.Value)) 
                : new [] { p });

            // Format date values and return as a Dictionary<string, string>:
            return jPairs.ToDictionary(jo => jo.Key, jo => ValueToString(jo.Value));
        }

    private static string ValueToString(JToken jToken)
    {
            if (jToken.Type is JTokenType.Date)
            {
                return jToken.Value<DateTime>().ToUniversalTime().ToString("s") + "Z";
            }
            return jToken.ToString();
        }

    private static string EncodePair(KeyValuePair<string, string> p)
    {
            string key = UrlEncode(p.Key);
            string value = UrlEncode(p.Value);
            return $"{key}={value}";
        }

    private static string UrlEncode(string s)
    {
            return Uri.EscapeDataString(s);
        }
}