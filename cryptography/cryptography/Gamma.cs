using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptography
{
    class Gamma : Cryptographer
    {
        public Gamma() { }
        public Gamma(int Key)
        {
            this.Key = Key;
        }
        public Gamma(int Key, string Text)
            : this(Key)
        {
            this.Text = Text;
        }

        public override string Encrypt()
        {
            string answer = "";
            Random random = new Random(Key);
            foreach (char symbol in Text)
            {                
                int key = random.Next(AlphabetLength) ^ (int)symbol;
                answer += (char)(key);
            }
            return answer;

        }
        public override string Decrypt()
        {
           return Encrypt();
        }

        public override bool SetKey(string text)
        {
            int key;
            if (text == "") return false;
            if (int.TryParse(text, out key) && Math.Abs(key) < AlphabetLength)
            {
                Key = key;
                return true;
            }
            else return false;
        }
    }
}
