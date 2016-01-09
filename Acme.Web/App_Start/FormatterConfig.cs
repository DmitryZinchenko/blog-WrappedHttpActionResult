using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acme.Web
{
    public static class FormatterConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Formatters.Clear();

            var jsonFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            configuration.Formatters.Add(jsonFormatter);
        }
    }
}