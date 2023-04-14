using System.Security.Cryptography;
using System.Text;

namespace OpenAISharp
{
    public class TripleDesEncryption
    {
        private static byte[] _key;
        private static byte[] _iv;
        public TripleDesEncryption(byte[] key, string iv)
        {
            _key = key;
            _iv = Convert.FromBase64String(iv);
        }
        public string Encrypt(string plainText)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Key = _key;
                tripleDes.IV = _iv;

                ICryptoTransform cryptoTransform = tripleDes.CreateEncryptor();
                byte[] encryptedBytes = cryptoTransform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public string Decrypt(string encryptedText)
        {
            byte[] inputBytes = Convert.FromBase64String(encryptedText);
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Key = _key;
                tripleDes.IV = _iv;

                ICryptoTransform cryptoTransform = tripleDes.CreateDecryptor();
                byte[] decryptedBytes = cryptoTransform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
