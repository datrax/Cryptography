using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PublicSequence.xaml
    /// </summary>
    public partial class PublicSequence : Window
    {
        public PublicSequence()
        {
            InitializeComponent();
        }
        List<int> key = new List<int>();
        private void GenerateSequence(object sender, RoutedEventArgs e)
        {
            int mm;
            if (!int.TryParse(m.Text, out mm))
            {
                MessageBox.Show("m must be a number");
                return;
            }
            int tt;
            if (!int.TryParse(t.Text, out tt))
            {
                MessageBox.Show("t must be a number");
                return;
            }
            if (!SetKey(sequence.Text))
            {
                MessageBox.Show("wrong sequence type");
                return;
            }
            int[] publicKey = SequenceGenerate(tt, mm, key.ToArray());
        string text = "";
        for (int i = 0; i < publicKey.Length; i++)
        {
            text += publicKey[i] + " ";
        }
        newSequence.Text = text;
        }
        private int[] SequenceGenerate(int t, int m, int[] sequence)
        {
            var answer = new int[sequence.Length];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = (t * sequence[i]) % m;
            }
            return answer;
        }
        public  bool SetKey(string text)
        {

            key.Clear();
            int k;
            int pos = 0;
            do
            {
                for (int i = pos; i < text.Length; i++)
                {
                    if (text[i] == ' ' || i == text.Length - 1)
                    {
                        if (int.TryParse(text.Substring(pos, i - pos + 1), out k))
                        {
                            key.Add(k);
                        }
                        else
                        {
                            return false;
                        }
                        pos = i;
                    }
                }
                pos++;

            } while (pos < text.Length);
            if (key.Count > 0) return true;
            if (int.TryParse(text.Substring(0, text.Length), out k))
            {
                key.Add(k);
                return true;
            }
            return false;
        }
    }
}
