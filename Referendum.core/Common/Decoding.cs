using System;
using System.Security.Cryptography;

namespace Referendum.core.Common
{
	public class Decoding
	{

		public static string Decod(string TextToDecrypt, string strKey, string strIV)
		{
			byte[] EncryptedBytes = Convert.FromBase64String(TextToDecrypt);

			//string strIV = "O9fGelU066lJf7tiIjTw7w==";
			byte[] dataIV = System.Convert.FromBase64String(strIV);


			//Setup the AES provider for decrypting.            
			AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();

			aesProvider.BlockSize = 128;
			aesProvider.KeySize = 256;
			//My key and iv that i have used in openssl
			aesProvider.Key = System.Text.Encoding.ASCII.GetBytes(strKey);
			//aesProvider.IV = System.Text.Encoding.UTF8.GetBytes(strIV);
			aesProvider.Padding = PaddingMode.PKCS7;
			aesProvider.Mode = CipherMode.CBC;


			ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, dataIV);
			byte[] DecryptedBytes = cryptoTransform.TransformFinalBlock(EncryptedBytes, 0, EncryptedBytes.Length);
			return System.Text.Encoding.ASCII.GetString(DecryptedBytes);
		}
	}
}
