using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace GameMaker_Studio_Decompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string file;
            MatchCollection m;
            using (StreamReader reader = new StreamReader(@"D:\DE\assets\data.win.hex"))
            {
                file = reader.ReadToEnd();
            }

            m = Regex.Matches(file, @"89504E47(?s).*?4E44AE426082");
            for (int i = 0; i < m.Count; i++)
            {
                File.WriteAllBytes(i.ToString() + ".png", StringToByteArray(Regex.Replace(m[i].Value, @"\s+", "")));
                Console.Write("\r{0}%   ", ((double)i / m.Count) * 100);
            }
        }


        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
