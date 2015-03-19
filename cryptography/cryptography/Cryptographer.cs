﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptography
{
    public abstract class Cryptographer
    {
        public int Key { get; set; }
        public string Text { get; set; }
        public  int AlphabetLength =1120;


        public abstract string Encrypt();
        public abstract string Decrypt();


        public abstract bool SetKey(string text);

    }
}
