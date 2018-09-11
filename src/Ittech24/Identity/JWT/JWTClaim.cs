using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Ittech24.Extensions;
using Newtonsoft.Json;

namespace Ittech24.Identity.JWT
{
    [DataContract]
    public class JWTClaim
    {
        [DataMember(Name = "jti")]
        public string JWTId { get; set; }
        [DataMember(Name = "iss")]
        public string Issuer { get; set; }
        [DataMember(Name = "sub")]
        public string Subject { get; set; }
        [DataMember(Name = "aud")]
        public string Audience { get; set; }
        [DataMember(Name = "exp")]
        public int Expiration { get; set; }
        [DataMember(Name = "nbf")]
        public int NotBefore { get; set; }
        [DataMember(Name = "iat")]
        public int IssuedAt { get; set; }
        [JsonExtensionData]
        public Dictionary<string, object> ExtraProperties { get; set; } = new Dictionary<string, object>();

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, 
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
        }

        public virtual string ToBase64UrlEncode()
        {
            var test = ToJson();
            return ToJson().Base64UrlEncode();
        }
    }
}
