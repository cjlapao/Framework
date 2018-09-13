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
        private Dictionary<string, object> header = new Dictionary<string, object>();

        [DataMember(Name = "alg")]
        public JWTAlgorithm Algorithm
        {
            get
            {
                if (header.ContainsKey("alg"))
                    if(Enum.TryParse(header["alg"].ToString(), true, out JWTAlgorithm result))
                        return result;
                return JWTAlgorithm.Empty;
            }
            set
            {
                string algorithm = null;
                switch (value)
                {
                    case JWTAlgorithm.Empty:
                        break;
                    case JWTAlgorithm.HS256:
                        algorithm = "HS256";
                        break;
                    case JWTAlgorithm.HS384:
                        algorithm = "HS385";
                        break;
                    case JWTAlgorithm.HS512:
                        algorithm = "HS512";
                        break;
                    case JWTAlgorithm.none:
                        algorithm = "none";
                        break;
                    case JWTAlgorithm.RS256:
                        algorithm = "RS256";
                        break;
                }
                Add("alg", algorithm);
            }
        }
        [DataMember(Name = "typ")]
        public JWTType Type
        {
            get
            {
                if (header.ContainsKey("typ"))
                    if (Enum.TryParse<JWTType>(header["typ"].ToString(), true, out JWTType result))
                        return result;
                return JWTType.NONE;
            }
            set
            {
                string type = null;
                switch (value)
                {
                    case JWTType.NONE:
                        break;
                    case JWTType.JWE:
                        type = "JWE";
                        break;
                    case JWTType.JWS:
                        type = "JWS";
                        break;
                    case JWTType.JWT:
                        type = "JWT";
                        break;
                }
                Add("typ", type);
            }
        }
        [DataMember(Name = "kid")]
        public string KeyId
        {
            get
            {
                return (string)Get("kid");
            }
            set
            {
                Add("kid", value);
            }
        }
        [DataMember(Name = "cty")]
        public JWTType ContentType
        {
            get
            {
                if (header.ContainsKey("cty"))
                    if (Enum.TryParse(header["cty"].ToString(), true, out JWTType result))
                        return result;
                return JWTType.NONE;
            }
            set
            {
                string type = null;
                switch (value)
                {
                    case JWTType.NONE:
                        break;
                    case JWTType.JWE:
                        type = "JWE";
                        break;
                    case JWTType.JWS:
                        type = "JWS";
                        break;
                    case JWTType.JWT:
                        type = "JWT";
                        break;
                }
                Add("cty", type);
            }
        }

        public object Get(string key)
        {
            if (header.ContainsKey(key))
                return header[key];
            return null;
        }

        public bool ContainsKey(string key)
        {
            return header.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            return header.ContainsValue(value);
        }

        public void Add(string key, object value)
        {
            if (header.ContainsKey(key))
            {
                if (value != null)
                    header[key] = value;
                else
                    Remove(key);
            }
            else
            {
                if (value != null)
                    header.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            if (header.ContainsKey(key))
                header.Remove(key);
        }

        public string ToJson()
        {
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            return jsonSerializer.Serialize(header);
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

        //public static Dictionary<string,object> Build()
        //{

        //}
    }
}
