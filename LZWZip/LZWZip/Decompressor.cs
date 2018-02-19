using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LZWZip
{
    public class Decompressor
    {
        public Stream InputStream { get; set; }

        public Decompressor()
        {

        }

        private static byte[] ToByteArray(string s)
        {
            byte[] array = new byte[s.Length];

            for (int i = 0; i < s.Length; i++)
                array[i] = (byte)s[i];

            return array;
        }

        public Stream Run()
        {
            string outputString = "";

            Dictionary<int, string> inverseDictionary = Compressor.DefaultInverseDictionary();
            string lastWord = "", currentWord;

            int symbolLength = InputStream.ReadByte();
            int nextDictionaryValue = inverseDictionary.Keys.Max() + 1;
            int symbolCount = (int)((8 * (InputStream.Length - 1)) / symbolLength);
            int[] symbols = StreamToSymbolList(InputStream, symbolCount, symbolLength);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (int symbol in symbols)
            {
                bool success = inverseDictionary.TryGetValue(symbol, out currentWord);
                if (!success)
                    currentWord = lastWord + lastWord[0];
                outputString += currentWord;

                if (lastWord != "")
                    inverseDictionary[nextDictionaryValue++] = lastWord + currentWord[0];
                lastWord = currentWord;
            }

            sw.Stop();
            InputStream.Close();
            //Console.WriteLine(sw.Elapsed);

            return new MemoryStream(ToByteArray(outputString));
        }

        private int[] StreamToSymbolList(Stream stream, int symbolCount, int symbolLength)
        {
            byte[] byteStream;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byteStream = memoryStream.ToArray();
            }

            int[] symbols = new int[symbolCount];
            Int64 streamSizeInBits = symbolCount * symbolLength;

            for (Int64 i = 0; i < streamSizeInBits; i++)
            {
                int byteStreamElement = (int)(i / 8);
                int byteStreamOffset = (int)(i % 8);

                int intStreamElement = (int)(i / symbolLength);
                int intStreamOffset = (int)(i % symbolLength);

                bool bit = (byteStream[byteStreamElement] & (1 << byteStreamOffset)) != 0;

                if (bit)
                    symbols[intStreamElement] |= (1 << intStreamOffset);
            }

            return symbols;
        }
    }
}
