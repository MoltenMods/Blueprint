using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint.Utilities
{
    public static class BitfieldUtilities
    {
        public static int ToBitfield(this BitArray bitArray)
        {
            var bitfield = 0;

            for (var i = 0; i < bitArray.Length; i++)
            {
                bitfield |= bitArray.Get(i) ? 0 : 1 << i;
            }

            return bitfield;
        }

        public static int ToBitfield(this IEnumerable<int> enums)
        {
            return enums.Aggregate(0, (bitfield, x) => bitfield | (1 << x));
        }

        public static BitArray ToBitArray(this byte bitfield)
        {
            var bitArray = new BitArray(8);

            for (var i = 0; i < 8; i++)
            {
                bitArray.Set(i, (bitfield & (1 << i)) != 0);
            }

            return bitArray;
        }
    }
}