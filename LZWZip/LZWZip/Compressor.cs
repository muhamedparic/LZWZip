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

        public void DecomposeToMaxLengthTest()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                ["a"] = 0,
                ["b"] = 1,
                ["ab"] = 2,
                ["ba"] = 3,
                ["aa"] = 4,
                ["aaa"] = 5,
                ["aba"] = 6
            };

            var decomposed = FlattenStringToMaxLength("aba", 2, dictionary);

            foreach (string s in decomposed)
            {
                Console.WriteLine(s);
            }
        }

        public Stream Run()
        {
            return null;
        }

        private static List<int> CompressStream(Stream stream, Dictionary<string, int> dictionary, ref Dictionary<int, string> inverseDictionary)
        {
            List<int> outputValues = new List<int>();
            string currentString = "";
            int nextDictionaryValue = dictionary.Values.Max() + 1;
            inverseDictionary = new Dictionary<int, string>();

            while (true)
            {
                int streamInt = stream.ReadByte();
                if (streamInt == -1)
                    break;

                char currentByte = (char)streamInt;
                string appendedString = currentString + currentByte;
                
                if (dictionary.ContainsKey(appendedString))
                {
                    currentString = appendedString;
                }
                else
                {
                    outputValues.Add(dictionary[currentString]);
                    inverseDictionary[nextDictionaryValue] = appendedString;
                    dictionary[appendedString] = nextDictionaryValue++;
                    currentString = currentByte.ToString();
                }
             
            }

            if (currentString.Length > 0)
                outputValues.Add(dictionary[currentString]);

            return outputValues;
        }

        private List<int> FlattenToOptimalSymbolLength(in List<int> symbols, in Dictionary<string, int> dictionary, in Dictionary<int, string> inverseDictionary, out int optimalSymbolLength)
        {
            Int64 currentSymbolLength = RequiredSymbolLength(symbols);
            List<int> currentList = symbols;
            Int64 currentTotalLength = currentSymbolLength * currentList.Count;

            while (true)
            {
                if (currentSymbolLength == 1)
                    break;

                Int64 newSymbolLength = currentSymbolLength - 1;
                List<int> listWithShorterSymbols = FlattenToMaxLength(currentList, (int)newSymbolLength, dictionary, inverseDictionary);
                Int64 newTotalLength = newSymbolLength * listWithShorterSymbols.Count;

                if (newTotalLength < currentTotalLength)
                {
                    currentList = listWithShorterSymbols;
                    currentSymbolLength = newSymbolLength;
                    currentTotalLength = newTotalLength;
                }
                else
                {
                    break;
                }
            }

            optimalSymbolLength = (int)currentSymbolLength;
            return currentList;
        }

        private int RequiredSymbolLength(in List<int> symbols)
        {
            int maxSymbolLength = 0;

            foreach (int symbol in symbols)
                maxSymbolLength = Math.Max(maxSymbolLength, RequiredSymbolLength(symbol));

            return maxSymbolLength;
        }

        private int RequiredSymbolLength(int dictionaryValue)
        {
            return (int)Math.Log(dictionaryValue, 2) + 1;
        }

        private List<int> FlattenToMaxLength(in List<int> symbols, int maxSymbolLength, in Dictionary<string, int> dictionary, in Dictionary<int, string> inverseDictionary)
        {
            List<int> finalList = new List<int>();
            
            foreach (int symbol in symbols)
            {
                if (RequiredSymbolLength(symbol) <= maxSymbolLength)
                    finalList.Add(symbol);

                string originalString = inverseDictionary[symbol];
                List<string> stringList = FlattenStringToMaxLength(originalString, maxSymbolLength, dictionary);

                foreach (string individualString in stringList)
                    finalList.Add(dictionary[individualString]);
            }

            return finalList;
        }

        private List<string> FlattenStringToMaxLength(string input, int maxSymbolLength, in Dictionary<string, int> dictionary)
        {
            if (RequiredSymbolLength(dictionary[input]) <= maxSymbolLength)
                return new List<string> { input };

            List<string> stringList = FlattenStringToMaxLength(input.Substring(0, input.Length - 1), maxSymbolLength, dictionary);
            stringList.Add(input[input.Length - 1].ToString());
            return stringList;
        }
    }
}
