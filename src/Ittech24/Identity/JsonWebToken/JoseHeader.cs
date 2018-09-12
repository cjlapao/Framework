using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Ittech24.Extensions;

namespace Ittech24.Identity.JsonWebToken
{
    [DataContract]
    public class JoseHeader
    {
        [DataMember(Name = "typ", Order = 2)]
        private string typ
        {
            get
            {
                if (Type != JWTType.NONE)
                    return Type.ToString();
                return null;
            }
            set
            {
                Enum.TryParse(typeof(JWTType), value, out object result);
                if (result != null)
                    Type = (JWTType)result;
            }
        }
        [DataMember(Name = "cty", Order = 4)]
        private string cty
        {
            get
            {
                if (ContentType != JWTType.NONE)
                    return ContentType.ToString();
                return null;
            }
        }
        [DataMember(Name = "alg", Order = 1)]
        public JWTAlgorithm Algorithm { get; set; }
        public JWTType Type { get; set; }
        [DataMember(Name = "kid", Order = 3)]
        public string KeyId {get;set;}
        public JWTType ContentType { get; set; }

        public string ToJson()
        {
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            return jsonSerializer.Serialize(this);
        }

        public string Encode()
        {
            return ToJson().Base64UrlEncode();
        }

        public static void TryParse(string input, out JoseHeader joseHeader)
        {
            joseHeader = null;
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            try
            {
                joseHeader = jsonSerializer.Deserialize<JoseHeader>(input);
            }
            catch { }
        }
    }
}
