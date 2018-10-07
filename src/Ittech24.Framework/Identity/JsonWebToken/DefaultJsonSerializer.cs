using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ittech24.Framework.Identity.JsonWebToken
{
    public class DefaultJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, 
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
        }
    }
}
