using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cryptography
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Reconcile(object sender, RoutedEventArgs e)
        {
            CngKey aliceCngKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            byte[] alicePublicKeyBlob = aliceCngKey.Export(CngKeyBlobFormat.EccPublicBlob);
            aFirst.Content = System.Text.Encoding.Unicode.GetString(alicePublicKeyBlob);
            CngKey bobCngKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            byte[] bobPublicKeyBlob = bobCngKey.Export(CngKeyBlobFormat.EccPublicBlob);
            bFirst.Content = System.Text.Encoding.Unicode.GetString(bobPublicKeyBlob);
            CngKey bobPubCngKey = CngKey.Import(bobPublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            ECDiffieHellmanCng aliceAlgorithm = new ECDiffieHellmanCng(aliceCngKey);
            byte[] aliceKey = aliceAlgorithm.DeriveKeyMaterial(bobPubCngKey);
            aSecond.Content = System.Text.Encoding.Default.GetString(aliceKey);
            CngKey alicePubCngKey = CngKey.Import(alicePublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            ECDiffieHellmanCng bobAlgorithm = new ECDiffieHellmanCng(bobCngKey);
            byte[] bobKey = bobAlgorithm.DeriveKeyMaterial(alicePubCngKey);
            bSecond.Content = System.Text.Encoding.Default.GetString(bobKey);
            File.WriteAllBytes("Key",bobKey);
        }
    }
}
