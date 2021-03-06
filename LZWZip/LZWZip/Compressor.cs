﻿using System;
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
        public Stream InputStream { get; set; }
        public uint MaxSymbolLength { get; set; }
        public CompressionMode FileCompressionMode { get; set; } = CompressionMode.CompressThenJoin;
        public List<string> FileNames { get; } = new List<string>();

        public Compressor()
        {
            MaxSymbolLength = 255;
        }

        public static Dictionary<string, int> DefaultDictionary()
        {
            Dictionary<string, int> newDefaultDictionary = new Dictionary<string, int>();

            for (int i = 0; i < 256; i++)
                newDefaultDictionary[((char)i).ToString()] = i;

            return newDefaultDictionary;
        }

        public static Dictionary<int, string> DefaultInverseDictionary()
        {
            Dictionary<int, string> newInverseDictionary = new Dictionary<int, string>();

            for (int i = 0; i < 256; i++)
                newInverseDictionary[i] = ((char)i).ToString();

            return newInverseDictionary;
        }


        public Stream Run()
        {
            //MemoryStream outputStream = new MemoryStream();
            //outputStream.WriteByte((byte)InputStreams.Count); // Number of files

            //if (FileCompressionMode == CompressionMode.CompressThenJoin)
            //    outputStream.WriteByte(0);
            //else
            //    outputStream.WriteByte(1); // Compression mode indicator

            //if (FileCompressionMode == CompressionMode.JoinThenCompress)
            //{
            //    foreach (Stream stream in InputStreams)
            //    {

            //    }
            //}

            Stream inputStream = InputStream;
            Dictionary<string, int> dictionary = DefaultDictionary();
            Dictionary<int, string> inverseDictionary = DefaultInverseDictionary();

            List<int> compressedList = CompressStream(inputStream, dictionary, inverseDictionary);
            int optimalLength = -1;
            compressedList = FlattenToOptimalSymbolLength(compressedList, dictionary, inverseDictionary, out optimalLength);

            InputStream.Close();

            MemoryStream outputStream = new MemoryStream();
            outputStream.WriteByte((byte)optimalLength);

            using (Stream byteDataStream = ListToStream(compressedList, optimalLength))
            {
                byteDataStream.CopyTo(outputStream);
            }

            return outputStream;
        }

        private static List<int> CompressStream(Stream stream, Dictionary<string, int> dictionary, Dictionary<int, string> inverseDictionary)
        {
            List<int> outputValues = new List<int>();
            string currentString = "";
            int nextDictionaryValue = dictionary.Values.Max() + 1;

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
            Int64 currentSymbolLength = Math.Max(symbols.Max(symbol => RequiredSymbolLength(symbol)), 8);
            List<int> currentList = symbols;
            Int64 currentTotalLength = currentSymbolLength * currentList.Count;

            while (true)
            {
                if (currentSymbolLength == 8)
                    break;

                int newSymbolLength = (int)currentSymbolLength - 1;
                List<int> listWithShorterSymbols = FlattenToMaxLength(currentList, newSymbolLength, dictionary, inverseDictionary);
                Int64 newTotalLength = newSymbolLength * listWithShorterSymbols.Count;

                if (currentSymbolLength > MaxSymbolLength || newTotalLength < currentTotalLength)
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

        public static int RequiredSymbolLength(int dictionaryValue)
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
            if (input.Length == 1 || RequiredSymbolLength(dictionary[input]) <= maxSymbolLength)
                return new List<string> { input };

            List<string> stringList = FlattenStringToMaxLength(input.Substring(0, input.Length - 1), maxSymbolLength, dictionary);
            stringList.Add(input[input.Length - 1].ToString());
            return stringList;
        }

        private Stream ListToStream(in List<int> inputList, int bitsPerElement)
        {
            Int64 arraySizeInbits = inputList.Count * bitsPerElement;
            Int64 paddedArraySize = arraySizeInbits;

            if (paddedArraySize % 8 != 0)
                paddedArraySize += 8 - (paddedArraySize % 8);

            byte[] buffer = new byte[paddedArraySize / 8];

            for (Int64 i = 0; i < arraySizeInbits; i++)
            {
                int bufferElement = (int)(i / 8);
                int bufferBit = (int)(i % 8);

                int listElement = (int)(i / bitsPerElement);
                int listBit = (int)(i % bitsPerElement);

                bool bit = (inputList[listElement] & (1 << listBit)) != 0;

                if (bit)
                    buffer[bufferElement] |= (byte)(1 << bufferBit);
            }

            return new MemoryStream(buffer);
        }
    }
}
