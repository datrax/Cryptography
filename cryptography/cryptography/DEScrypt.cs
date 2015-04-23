using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;

namespace cryptography
{
    class DEScrypt : Cryptographer
    {
        public override string Encrypt()
        {
            DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(Key),
                IV = Encoding.Default.GetBytes("ABCDEFGH")
            };
            using (FileStream filestream = new FileStream("Encoded.txt", FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(filestream, desCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] text = Encoding.UTF8.GetBytes(Text);
                cryptoStream.Write(text, 0, text.Length);            
            }
            using (StreamReader reader = new StreamReader("Encoded.txt"))
            {
                return reader.ReadToEnd();
            }
        }

        public override string Decrypt()
        {
            DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(Key),
                IV = Encoding.UTF8.GetBytes("ABCDEFGH")
            };
            using (FileStream fileStream = new FileStream("Encoded.txt", FileMode.Open, FileAccess.Read))
            using (CryptoStream cryptoStream = new CryptoStream(fileStream, desCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read))
            using (StreamReader streamReader = new StreamReader(cryptoStream))
            {
                return streamReader.ReadToEnd();
            }
        }
        public override bool SetKey(string text)
        {
            if (Encoding.UTF8.GetBytes(text).Length != 8)
            {
                return false;
            }
            Key = text;
            return true;
        }

        internal void OpenLeftText(object send, System.Windows.Input.MouseButtonEventArgs ev)
        {
            Process.Start("Encoded.txt");
            ((TextBox)send).Text = "Preview isn't supported click left mouse button to see the text";
        }
    }
}
