using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

using LZWZip;

namespace LZWZipTest
{
    [TestClass]
    public class CompressorTest
    {
        private void PrintStreamTest(in Stream stream)
        {
            for (int i = 0; i < stream.Length; i++)
            {
                byte curByte = (byte)stream.ReadByte();

                Console.WriteLine(Convert.ToString(curByte, 2).PadLeft(8, '0'));
            }
        }

        [TestMethod]
        public void CompressStreamTestMethod()
        {
            byte[] inputData = new byte[3];

            for (int i = 0; i < inputData.Length; i++)
                inputData[i] = 97;

            Compressor compressor = new Compressor();
            compressor.InputStreams.Add(new MemoryStream(inputData));

            Stream outputStream = compressor.Run();
            PrintStreamTest(outputStream);
        }
    }
}
