using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptography
{
    class Cezar:Cryptographer
    {
        public Cezar() { }
        public Cezar(int Key)
        {
            this.Key = Key;
        }
        public Cezar(int Key,string Text)
            :this(Key)
        {
            this.Text = Text;
        }

        public  override string Encrypt()
        {
            string answer = "";
            foreach (char symbol in Text)
            {
                answer += (char)(((int)symbol + Key+AlphabetLength) % AlphabetLength);
            }
            return answer;

        }
        public  override string Decrypt()
        {
            string answer="";
            foreach (char symbol in Text)
            {
                answer += (char)(((int)symbol - Key + AlphabetLength) % AlphabetLength);
            }
            return answer;
        }
    }
}
