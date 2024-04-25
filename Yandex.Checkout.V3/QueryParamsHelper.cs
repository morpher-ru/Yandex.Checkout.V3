using System.Collections.Generic;
using System.Linq;

namespace Yandex.Checkout.V3
{
    public static class QueryParamsHelper
    {
        public static string AddQueryString(
            string basePath,
            Dictionary<string, string> queryParams)
        {
            if (queryParams.Count <= 0)
            {
                return basePath;
            }

            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            return $"{basePath}?{queryString}";
        }
    }
}
