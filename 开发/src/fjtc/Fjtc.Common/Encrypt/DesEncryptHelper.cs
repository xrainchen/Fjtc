using RPoney;
using RPoney.Encrypt;
using RPoney.Encrypt.Imp;

namespace Fjtc.Common.Encrypt
{
    public static class DesEncryptHelper
    {
        private static readonly IEncryptService Encript=new EncryptServiceFactory().CreateSecurityService(PublicEnum.EncryptServiceEnum.DES);

        public  static string DesEncrypt(this string sourceHexText, string hexKey= "3530373235306239")
        {
            return Encript.Encrypt(sourceHexText.GetBytes(), hexKey.GetBytes()).GetHexString();
        }
        
        public static string DesDecrypt(this string sourceHexText, string hexKey = "3530373235306239")
        {
            return Encript.Decrypt(sourceHexText.GetBytes(), hexKey.GetBytes()).GetHexString();
        }
    }
}
