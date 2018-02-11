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
        [TestMethod]
        public void CompressStreamTestMethod()
        {
            MemoryStream input = new MemoryStream(new byte[] { 80, 80, 81, 80, 80 });

            var defaultDictionary = new Dictionary<string, int>(Compressor.defaultDictionary);
            var result = Compressor.CompressStream(input, defaultDictionary);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            CollectionAssert.AreEqual(new int[] { 80, 80, 81, 256 }, result.ToArray());
        }
    }
}
