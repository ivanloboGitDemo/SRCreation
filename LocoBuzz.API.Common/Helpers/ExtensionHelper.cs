using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.Common.Helpers
{
    public static class ExtensionHelper
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }

        public static T RestClient<T>(this RestClient restClient, string Url, Method MethodType, string contentType, T deserializedObject) where T : class,new()
        {
            var client = new RestSharp.RestClient(Url);
            var request = new RestSharp.RestRequest(MethodType);
            request.OnBeforeDeserialization = resp => { resp.ContentType = contentType; };
            var result = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(result.Content);
        }
    }
}
