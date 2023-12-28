using Newtonsoft.Json.Linq;
using System.Globalization;

namespace TabloRazor.Table.WebApiDataProvider
{
    public static class UriBuilder
    {
        public static async Task<string> ToQueryString(this object obj)
        {
            var keyValueContenct = ToKeyValue(obj);
            var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContenct);
            return await formUrlEncodedContent.ReadAsStringAsync();
        }

        private static IEnumerable<KeyValuePair<string, string>> ToKeyValue(object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }
            JToken token = metaToken as JToken;

            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }
            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = ToKeyValue(child);
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent).ToDictionary(k => k.Key, v => v.Value);
                    }
                }
                return contentData;
            }
            var jValue = token as JValue;
            if (jValue?.Value != null)
            {
                return null;
            }
            var value = jValue?.Type == JTokenType.Date ?
                jValue?.ToString("o", CultureInfo.InvariantCulture) :
                jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
    }
}
