using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using cryptography;

namespace crypt6
{
    public class Backpack : Cryptographer
    {
        public List<int> key = new List<int>();

        public override string Encrypt()
        {
            int index = 0, sum = 0;
            string answer = "";
            foreach (var sym in Text)
            {
                int ch = 1;
                for (int i = 0; i < 16; i++)
                {
                    if ((ch & sym) != 0)
                        sum += key[index];
                    ch <<= 1;
                    index++;
                    if (index % key.Count == 0)
                    {
                        answer += Convert.ToChar(sum);
                        index = 0;
                        sum = 0;
                    }
                }
            }
            if (index != 0)
            {
                answer +=Convert.ToChar(sum);
            }
            return answer;
        }

        public override string Decrypt()
        {
            var b = key.GetRange(2, key.Count - 2);
            var m = key[0];var t = key[1];
            var reverset = Euklid(t, m);
            var newText = "";
            foreach (var sym in Text)
                newText += Convert.ToChar((sym * reverset) % m);
            var bits = "";
            foreach (var symbol in newText)
            {
                var sum = 0;
                var temp = "";
                for (int i = b.Count - 1; i >= 0; i--)
                {

                    if (b[i] + sum <= Convert.ToInt32(symbol))
                    {
                        sum += b[i];
                        temp += "1";
                    }
                    else
                    {
                        temp += "0";
                    }
                }
                for (int i = temp.Length - 1; i >= 0; i--) bits += temp[i];
            }
            string answer = "";
            for (int i = 0; i < (bits.Length / 16); i++)
            {
                int symCode = 0;
                for (int j = 0; j < 16; j++)
                {
                    if (bits[j + i * 16] == '1')
                    {
                        int bit = 1 << j;
                        symCode |= bit;
                    }
                }
                answer += Convert.ToChar(symCode);
            }
            return answer;
        }

        private int Euklid(int a, int n)
        {
            int i = n, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0)
            {
                v = (v + n) % n;
            }
            return v;
        }

        private int[] PublicSequence(int t, int m, int[] sequence)
        {
            var answer = new int[sequence.Length];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = (t * sequence[i]) % m;
            }
            return answer;
        }

        public override bool SetKey(string text)
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

        public void GetPublicKey(object send, System.Windows.Input.MouseButtonEventArgs ev)
        {

            int[] publicKey = PublicSequence(key[1], key[0], key.GetRange(2, key.Count - 2).ToArray());
            string text = "";
            for (int i = 0; i < publicKey.Length; i++)
            {
                text += publicKey[i] + " ";
            }
            ((TextBox)send).Text = text;
        }
    }
}