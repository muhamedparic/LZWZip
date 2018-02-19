using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZWZip
{
    public class ByteArrayComparer : EqualityComparer<byte[]> // https://stackoverflow.com/a/30353296
    {
        public override bool Equals(byte[] first, byte[] second)
        {
            if (first == null || second == null)
            {
                // null == null returns true.
                // non-null == null returns false.
                return first == second;
            }
            if (ReferenceEquals(first, second))
            {
                return true;
            }
            if (first.Length != second.Length)
            {
                return false;
            }
            // Linq extension method is based on IEnumerable, must evaluate every item.
            return first.SequenceEqual(second);
        }
        public override int GetHashCode(byte[] obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (obj.Length == 0)
                return 0;
            else if (obj.Length == 1)
                return obj[0];
            else
                return obj[0] * 256 + obj[1];
        }
    }
}
