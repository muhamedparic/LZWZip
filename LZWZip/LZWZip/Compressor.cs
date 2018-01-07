using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lzw
{
    public enum CompressionMode
    {
        CompressThenJoin,
        JoinThenCompress
    }

    public class Compressor
    {
        private List<FileStream> files = new List<FileStream>();
        public List<FileStream> Files
        {
            get
            {
                return files;
            }
        }

        public uint MaxSymbolLength { get; set; }
        public CompressionMode FileCompressionMode { get; set; } = CompressionMode.CompressThenJoin;

        public MemoryStream Run()
        {
            return null;
        }
    }
}
