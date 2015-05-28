using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Input;

namespace cryptography
{
    public class AESDiff : Cryptographer
    {
        private byte[] key;
        public override string Encrypt()
        {
            GenerateKey();
            //key = File.ReadAllBytes("Key");
            Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(Text);
                }
                byte[] encrypted_text = msEncrypt.ToArray();
                File.WriteAllBytes("Encoded.txt", encrypted_text);
                return System.Text.Encoding.UTF8.GetString(encrypted_text);
            }
        }

        public override string Decrypt()
        {
            //key = File.ReadAllBytes("Key");
            Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            byte[] buffer = File.ReadAllBytes("Encoded.txt");
            using (MemoryStream msDecrypt = new MemoryStream(buffer))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                string decrypted_text = srDecrypt.ReadToEnd();
                return decrypted_text;
            }
        }
        void GenerateKey()
        {
            CngKey aliceCngKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            byte[] alicePublicKeyBlob = aliceCngKey.Export(CngKeyBlobFormat.EccPublicBlob);

            CngKey bobCngKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            byte[] bobPublicKeyBlob = bobCngKey.Export(CngKeyBlobFormat.EccPublicBlob);

            CngKey bobPubCngKey = CngKey.Import(bobPublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            ECDiffieHellmanCng aliceAlgorithm = new ECDiffieHellmanCng(aliceCngKey);
            byte[] aliceKey = aliceAlgorithm.DeriveKeyMaterial(bobPubCngKey);

            CngKey alicePubCngKey = CngKey.Import(alicePublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            ECDiffieHellmanCng bobAlgorithm = new ECDiffieHellmanCng(bobCngKey);
            byte[] bobKey = bobAlgorithm.DeriveKeyMaterial(alicePubCngKey);
            key = bobKey;
        }

        public override bool SetKey(string text)
        {

            return true;
        }

        internal void OpenLeftText(object send, MouseButtonEventArgs ev)
        {
            Process.Start("Encoded.txt");
            ((TextBox)send).Text = "Preview isn't supported click left mouse button to see the text";
        }

       /* internal void ShowKeys(object send, MouseButtonEventArgs ev)
        {
            var window = new Window1();
            window.Show();
        }

        internal void ShowKeyFromFIle(object send, MouseButtonEventArgs ev)
        {
            Process.Start("Key");
        }*/
    }
}
