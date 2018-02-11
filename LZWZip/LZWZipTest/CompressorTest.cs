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
            byte[] inputData = { 80, 81, 81, 80, 80 };
            Compressor compressor = new Compressor();
            compressor.DecomposeToMaxLengthTest();
            Assert.IsNotNull(compressor);
        }
    }
}
