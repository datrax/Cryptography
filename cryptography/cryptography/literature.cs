using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace cryptography
{
    class Literature:Cryptographer
    {
        private string path;
        private string[] textLines;

        public override string Encrypt()
        {
            List<string>options=new List<string>();
            string answer = "";
            foreach (char symbol in Text)
            {
                for (int i = 0; i < textLines.Count(); i++)
                {
                    for (int j = 0; j < textLines[i].Count(); j++)
                    {
                        if (symbol.Equals(textLines[i][j]))
                        {
                            string t = "";
                            if(i.ToString().Length<2)t+="0"+i.ToString();
                            else
                                t += i.ToString();
                            if (j.ToString().Length < 2) t += "0" + j.ToString();
                            else
                                t += j.ToString();
                            options.Add(t);
                        }
                    }
                }
                if (options.Count == 0)
                {
                    MessageBox.Show("Use another key file");
                    return "";
                }
                Random rand = new Random();
                answer += options[rand.Next(options.Count)] + " ";
                options.Clear();              
            }
            return answer;
        }

        public override string Decrypt()
        {
            string answer = "";
            string buf="";
            for (int i = 0; i < Text.Length; i++)
            {
                buf += Text[i];
                if (i%5 == 4)
                {
                    answer+= textLines[int.Parse(buf.Substring(0, 2))][int.Parse(buf.Substring(2, 2))];
                    buf = "";
                }          
            }
            return answer;
        }

        public override bool SetKey(string text)
        {
            try
            {
                textLines = File.ReadAllLines(path,Encoding.Default);               
            }
            catch
            {
                MessageBox.Show("Неверно выбраный файл");
            }
            return true;
        }

        public void SetSource(object sender, EventArgs eventArgs)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            if ( dlg.ShowDialog() != true) return;
            path = dlg.FileName;
            ((TextBox)sender).Text = path;
        }
        public void OpenSource(object sender, EventArgs eventArgs)
        {
            try
            {
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Неверно выбраный файл");
            }
        }
    }
}
