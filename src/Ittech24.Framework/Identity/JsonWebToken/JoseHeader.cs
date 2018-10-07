using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Ittech24.Framework.Extensions;
using Newtonsoft.Json;

namespace Ittech24.Framework.Identity.JsonWebToken
{
    [DataContract]
    public class JoseHeader
    {
        private Dictionary<string, object> header = new Dictionary<string, object>();

        public JWTAlgorithm Algorithm
        {
            get
            {
                if (header.ContainsKey(JoseJWSPublicRegistry.Algorithm))
                    if(Enum.TryParse(header[JoseJWSPublicRegistry.Algorithm].ToString(), true, out JWTAlgorithm result))
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
        public JWTType Type
        {
            get
            {
                if (header.ContainsKey(JoseJWSPublicRegistry.Type))
                    if (Enum.TryParse(header[JoseJWSPublicRegistry.Type].ToString(), true, out JWTType result))
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
        public string KeyId
        {
            get
            {
                return (string)Get(JoseJWSPublicRegistry.KeyID);
            }
            set
            {
                Add(JoseJWSPublicRegistry.KeyID, value);
            }
        }
        public JWTType ContentType
        {
            get
            {
                if (header.ContainsKey(JoseJWSPublicRegistry.ContentType))
                    if (Enum.TryParse(header[JoseJWSPublicRegistry.ContentType].ToString(), true, out JWTType result))
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

        public JoseHeader()
        {
            Type = JWTType.JWT;
            Algorithm = JWTAlgorithm.none;
        }

        public object this[string key] => Get(key);

        public int Count() => header.Count();

        public object Get(string key)
        {
            if (header.ContainsKey(key))
                return header[key];
            return null;
        }

        public bool ContainsKey(string key) => header.ContainsKey(key);

        public bool ContainsValue(object value) => header.ContainsValue(value);

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

        public override string ToString() => ToJson();
        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if(obj is JoseHeader)
            {
                if (obj.ToString() == ToString())
                    return true;
            }
            return false;
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

        public static bool TryParse(IDictionary<string, object> inputDic, out JoseHeader joseHeader)
        {
            joseHeader = null;
            try
            {
                joseHeader = new JoseHeader();
                foreach (var item in inputDic)
                {
                    joseHeader.Add(item.Key, item.Value);
                }
                return true;
            }
            catch { }
            return false;
        }

    }
}
