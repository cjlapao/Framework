using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ittech24.Identity.JWT
{
    [DataContract]
    public class JoseHeader
    {
        [DataMember(Name = "alg")]
        public string Algorithm { get; set; }
        [DataMember(Name = "typ")]
        public string Type { get; set; }
        [DataMember(Name = "kid")]
        public string KeyId {get;set;}
    }
}
