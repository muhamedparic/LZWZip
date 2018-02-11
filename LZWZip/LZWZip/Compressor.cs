using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LZWZip
{
    public enum CompressionMode
    {
        CompressThenJoin,
        JoinThenCompress
    }

    public class Compressor
    {
        private List<Stream> inputStreams = new List<Stream>();
        public List<Stream> InputStreams { get => inputStreams; }

        public uint MaxSymbolLength { get; set; }
        public CompressionMode FileCompressionMode { get; set; } = CompressionMode.CompressThenJoin;

        private static Dictionary<string, int> defaultDictionary = new Dictionary<string, int>();

        public Compressor()
        {
            // Populating the default dictionary
            if (defaultDictionary.Count == 0)
            {
                for (int i = 0; i < 256; i++)
                    defaultDictionary[((char)i).ToString()] = i;
            }
        }

        public MemoryStream Run()
        {
            return null;
        }

        private static List<int> CompressStream(Stream stream, Dictionary<string, int> dictionary)
        {
            List<int> outputValues = new List<int>();
            string currentString = "";
            int nextDictionaryValue = dictionary.Max().Value + 1;

            while (true)
            {
                int streamInt = stream.ReadByte();
                if (streamInt == -1)
                    break;

                char currentByte = (char)streamInt;
                
                if (dictionary.ContainsKey(currentString + currentByte))
                {
                    currentString += currentByte;
                }
                else
                {
                    outputValues.Add(dictionary[currentString]);
                    dictionary[currentString + currentByte] = nextDictionaryValue++;
                    currentString = currentByte.ToString();
                }
             
            }

            if (currentString.Length > 0)
                outputValues.Add(dictionary[currentString]);

            return outputValues;
        }
    }
}
