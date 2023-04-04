using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace OpenAISharp
{
    public class EncryptionUtils
    {
        static byte[] ConvertStringTo16Bytes(string macString)
        {
            byte[] keyBytes = new byte[16];
            string[] macBytes = macString.Split('-');
            for (int i = 0; i < 6; i++)
            {
                keyBytes[i] = Convert.ToByte(macBytes[i], 16);
                keyBytes[i + 8] = Convert.ToByte(macBytes[i], 16);
            }
            for (int i = 12; i < 16; i++)
            {
                keyBytes[i] = 0;
            }
            return keyBytes;
        }
        public static byte[] GetMacAddress16BytesFormat()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            PhysicalAddress macAddress = nics[0].GetPhysicalAddress();
            string macString = BitConverter.ToString(macAddress.GetAddressBytes());
            byte[] keyBytes = ConvertStringTo16Bytes(macString);
            return keyBytes;
        }
        public static string GetNewInitailizationVectorBase64String()
        {
            byte[] iv = new byte[8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }
            string InitializationVectorBase64String = Convert.ToBase64String(iv);
            return InitializationVectorBase64String;
        }
    }
}