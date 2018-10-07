using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using Ittech24.Framework.Extensions;
using Newtonsoft.Json;

namespace Ittech24.Framework.Identity.JsonWebToken
{
    [DataContract]
    public class JsonWebTokenClaim
    {
        private Dictionary<string, object> items = new Dictionary<string, object>();

        public string Id
        {
            get
            {
                return (string)Get(JWTPublicRegistry.JWTId);
            }
            set
            {
                Add(JWTPublicRegistry.JWTId, value);
            }
        }
        public string Issuer
        {
            get
            {
                return (string)Get(JWTPublicRegistry.Issuer);
            }
            set
            {
                Add(JWTPublicRegistry.Issuer, value);
            }
        }
        public string Subject
        {
            get
            {
                return (string)Get(JWTPublicRegistry.Subject);
            }
            set
            {
                Add(JWTPublicRegistry.Subject, value);
            }
        }
        public string Audience
        {
            get
            {
                return (string)Get(JWTPublicRegistry.Audience);
            }
            set
            {
                Add(JWTPublicRegistry.Audience, value);
            }
        }
        public DateTime Expiration
        {
            get
            {
                long epochTime = 0;
                if (ContainsKey(JWTPublicRegistry.ExpirationTime))
                {
                    if(Get(JWTPublicRegistry.ExpirationTime).GetType() == typeof(long))
                    {
                        epochTime = (long)Get(JWTPublicRegistry.ExpirationTime);
                        return DateTimeHelpers.FromEpoch(epochTime);
                    }
                }
                return DateTime.MinValue;
            }
            set
            {
                Add(JWTPublicRegistry.ExpirationTime, value.ToEpoch());
            }
        }
        public DateTime NotBefore
        {
            get
            {
                long epochTime = 0;
                if (ContainsKey(JWTPublicRegistry.NotBefore))
                {
                    if (Get(JWTPublicRegistry.NotBefore).GetType() == typeof(long))
                    {
                        epochTime = (long)Get(JWTPublicRegistry.NotBefore);
                        return DateTimeHelpers.FromEpoch(epochTime);
                    }
                }
                return DateTime.MinValue;
            }
            set
            {
                Add(JWTPublicRegistry.NotBefore, value.ToEpoch());
            }
        }
        public DateTime IssuedAt
        {
            get
            {
                long epochTime = 0;
                if (ContainsKey(JWTPublicRegistry.IssuedAt))
                {
                    if (Get(JWTPublicRegistry.IssuedAt).GetType() == typeof(long))
                    {
                        epochTime = (long)Get(JWTPublicRegistry.IssuedAt);
                        return DateTimeHelpers.FromEpoch(epochTime);
                    }
                }
                return DateTime.MinValue;
            }
            set
            {
                Add(JWTPublicRegistry.IssuedAt, value.ToEpoch());
            }
        }

        public object this[string key] => Get(key);

        public int Count() => items.Count();

        public object Get(string key)
        {
            if (items.ContainsKey(key))
                return items[key];
            return null;
        }

        public bool ContainsKey(string key) => items.ContainsKey(key);

        public bool ContainsValue(object value) => items.ContainsValue(value);

        public void Add(string key, object value)
        {
            if (items.ContainsKey(key))
            {
                if (value != null)
                    items[key] = value;
                else
                    Remove(key);
            }
            else
            {
                if (value != null)
                    items.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            if (items.ContainsKey(key))
                items.Remove(key);
        }

        public override string ToString() => ToJson();

        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is JsonWebTokenClaim)
            {
                if (obj.ToString() == ToString())
                    return true;
            }
            return false;
        }

        public string ToJson()
        {
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            return jsonSerializer.Serialize(items);
        }

        public string Encode()
        {
            return ToJson().Base64UrlEncode();
        }

        public static bool TryParse(string input, out JsonWebTokenClaim claim)
        {
            claim = null;
            Dictionary<string, object> items = null;
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            try
            {
                items = jsonSerializer.Deserialize<Dictionary<string, object>>(input);
                if (items != null)
                {
                    claim = new JsonWebTokenClaim();
                    foreach (var item in items)
                    {
                        claim.Add(item.Key, item.Value);
                    }
                }
                return true;
            }
            catch { }
            return false;
        }

        public static bool TryParse(IDictionary<string, object> inputDic, out JsonWebTokenClaim claim)
        {
            claim = null;
            try
            {
                claim = new JsonWebTokenClaim();
                foreach (var item in inputDic)
                {
                    claim.Add(item.Key, item.Value);
                }
                return true;
            }
            catch { }
            return false;
        }
    }
}
