using System;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Ittech24.Identity.JWT 
{
    public class JWToken : IJWToken
    {
        public JWTAlgorithm Algorithm {get;set;} = JWTAlgorithm.HS256;
        public JoseHeader Header {get;set;}
        public object Payload {get;set;}
        public X509Certificate2 Certificate {get;set;}
        public string Key {get;set;}

        public void Sign()
        {
            if(Header == null) {
                Header = BuildJoseHeader();
            }
            
        }

        public void Sign(JWTAlgorithm algorithm)
        {
            Algorithm = algorithm;
            Sign();
        }

        public void LoadCertificateFromFile(string filename)
        {
            if(!File.Exists(filename))
            {
                throw new FileNotFoundException("Certificate was not found");
            }
            // byte[] test 
            // using
            Certificate = new X509Certificate2();
        }

        private JoseHeader BuildJoseHeader(bool generateKeyID = true){
            Header = new JoseHeader();
            Header.Algorithm = Algorithm.ToString();
            Header.Type = "JWT";
            if(Certificate !=null && generateKeyID){
                Header.KeyId = Certificate.SerialNumber;
            }
            return Header;
        }
    }
}