using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace cryptography
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal delegate void del(object sender, EventArgs eventArgs);
    public partial class MainWindow : Window
    {
        Cryptographer cryptographer = new Cezar();
        public MainWindow()
        {
            InitializeComponent();
            methodList.Items.Add("Cezar");
            methodList.Items.Add("Trithemius");
            methodList.Items.Add("Gamma");
            methodList.Items.Add("Literature");
            methodList.SelectedItem = methodList.Items[1];
        }

        private void DropDefaultText(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Substring(file.LastIndexOf('.')) == ".txt")
                {
                    System.Diagnostics.Debug.WriteLine(file);
                    StreamReader re = new StreamReader(file);
                    encryptedText.Text += re.ReadToEnd();
                    re.Close();
                }
            }
        }

        private void DropEncodedText(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Substring(file.LastIndexOf('.')) == ".txt")
                {
                    System.Diagnostics.Debug.WriteLine(file);
                    StreamReader re = new StreamReader(file);
                    decryptedText.Text += re.ReadToEnd();
                    re.Close();
                }
            }
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void Encrypting(object sender, RoutedEventArgs e)
        {
            cryptographer.Text = encryptedText.Text;
            cryptographer.SetKey(keyField.Text);
            decryptedText.Text = cryptographer.Encrypt();
        }
        private void Decrypting(object sender, RoutedEventArgs e)
        {

            cryptographer.Text = decryptedText.Text;
            if ((string)DecryptButton.Content == "Decrypt")
            {
                cryptographer.SetKey(keyField.Text);
                encryptedText.Text = cryptographer.Decrypt();
            }
            else
            {
                new Thread(StartAttacking).Start();
            }
        }

        private void StartAttacking()
        {
            StringBuilder answer = new StringBuilder();
            for (int i = 0; i < cryptographer.AlphabetLength; i++)
            {
                cryptographer.Key = i;
                answer.Append(i + ": " + cryptographer.Decrypt() + Environment.NewLine);
            }
            File.WriteAllText("Temp.txt", answer.ToString(), Encoding.Unicode);
            Process.Start("Temp.txt");
        }
        private void CheckTheKey(object sender, TextChangedEventArgs e)
        {

            if (cryptographer.SetKey(keyField.Text))
            {
                if (keyField.Text == "")
                {
                    EncryptButton.IsEnabled = false;
                    DecryptButton.IsEnabled = true;
                    DecryptButton.Content = "Attack";
                    return;

                }
                else
                {
                    DecryptButton.Content = "Decrypt";
                    EncryptButton.IsEnabled = true;
                }

                EncryptButton.IsEnabled = true;
                DecryptButton.IsEnabled = true;
            }
            else
            {
                EncryptButton.IsEnabled = false;
                DecryptButton.IsEnabled = false;
            }
        }


        private void SetMethod(object sender, SelectionChangedEventArgs e)
        {
            string input = methodList.SelectedItem.ToString();
            if (input == "Cezar")
                cryptographer = new Cezar();
            if (input == "Trithemius")
                cryptographer = new Trithemius();
            if (input == "Gamma")
                cryptographer = new Gamma();
            MouseButtonEventHandler mbl = new MouseButtonEventHandler((send, ev) => ((Literature)cryptographer).SetSource(send, ev));
            MouseButtonEventHandler mbr = new MouseButtonEventHandler((send, ev) => ((Literature)cryptographer).OpenSOurce(send, ev));
            if (input == "Literature")
            {
                cryptographer = new Literature();
                keyField.PreviewMouseLeftButtonDown += mbl;
                keyField.PreviewMouseRightButtonDown += mbr;
            }
            else
            {
                keyField.PreviewMouseLeftButtonDown -= mbl;
                keyField.PreviewMouseRightButtonDown -= mbr;
            }
        }


    }
}
