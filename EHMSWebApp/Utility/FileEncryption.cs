using System.Security.Cryptography;
using System.Text;

namespace EHMSWebApp.Utility
{
    public static class FileEncryption
    {
        private static readonly string EncryptionKey = "YourEncryptionKey123";

        public static byte[] EncryptFile(byte[] fileBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.IV = new byte[16];

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(fileBytes, 0, fileBytes.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }
        public static byte[] DecryptFile(byte[] encryptedBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.IV = new byte[16];

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(encryptedBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var decryptedStream = new MemoryStream();
            cryptoStream.CopyTo(decryptedStream);

            return decryptedStream.ToArray();
        }
    }
}
