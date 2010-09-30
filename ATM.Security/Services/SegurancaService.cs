namespace ATM.Security.Services
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	public static class SegurancaService
	{
		private const string _key = "bsi-2007";

		public static string Criptografar(object puro)
		{
			return EncryptString(puro.ToString(), _key);
		}

		public static string Decriptografar(string cifra)
		{
			return DecryptString(cifra, _key);
		}

		public static string GerarHash(string textoPuro)
		{
			byte[] pass = Encoding.UTF8.GetBytes(textoPuro);

			MD5 md5 = new MD5CryptoServiceProvider();

			return Encoding.UTF8.GetString(md5.ComputeHash(pass));
		}

		private static string EncryptString(string Message, string Passphrase)
		{
			byte[] Results;
			var UTF8 = new UTF8Encoding();

			var HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

			var TDESAlgorithm = new TripleDESCryptoServiceProvider();

			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;

			byte[] DataToEncrypt = UTF8.GetBytes(Message);

			try
			{
				ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
				Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
			}
			finally
			{
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}

			return Convert.ToBase64String(Results);
		}

		private static string DecryptString(string Message, string Passphrase)
		{
			byte[] Results;
			var UTF8 = new UTF8Encoding();

			var HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

			var TDESAlgorithm = new TripleDESCryptoServiceProvider();

			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;

			byte[] DataToDecrypt = Convert.FromBase64String(Message);

			try
			{
				ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
				Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
			}
			finally
			{
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}

			return UTF8.GetString(Results);
		}
	}
}