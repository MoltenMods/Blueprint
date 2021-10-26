using System;
using System.Buffers.Binary;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blueprint.Messages.Objects
{
    public static class GameCodeParser
    {
        private const string V2 = "QWXRTYLPESDFGHUJKZOCVBINMA";

        private static readonly int[] V2Map = Enumerable.Range(65, 26).Select(v => V2.IndexOf((char) v)).ToArray();

        private static readonly RNGCryptoServiceProvider Random = new();

        public static int GenerateCodeV1() => GenerateCode(4);

        public static int GenerateCodeV2() => GenerateCode(6);

        public static string IntToGameName(int value)
        {
            if (value < -1)
            {
                // V2 (6-digit) code
                return IntToGameNameV2(value);
            }
            
            // V1 (4-digit) code
            Span<byte> code = stackalloc byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(code, value);
            return Encoding.UTF8.GetString(code);
        }

        public static int GameNameToInt(string code)
        {
            if (code.Any(x => !Char.IsLetter(x)))
            {
                return -1;
            }
            
            return code.Length switch
            {
                6 => GameNameToIntV2(code.ToUpperInvariant()),                          // V2 (6-digit) code
                4 => code[0] | ((code[1] | ((code[2] | (code[3] << 8)) << 8)) << 8),    // V1 (4-digit) code
                _ => -1
            };
        }

        private static int GenerateCode(int length)
        {
            Span<byte> data = stackalloc byte[length];
            Random.GetBytes(data);

            Span<char> dataChar = stackalloc char[length];
            for (var i = 0; i < length; i++)
            {
                dataChar[i] = V2[V2Map[data[i] % 26]];
            }

            return GameNameToInt(new String(dataChar));
        }

        private static string IntToGameNameV2(int value)
        {
            var firstTwoDigits = value & 0x3FF;
            var lastFourDigits = (value >> 10) & 0xFFFFF;

            return new String(new []
            {
                V2[firstTwoDigits % 26],
                V2[firstTwoDigits / 26],
                V2[lastFourDigits % 26],
                V2[(lastFourDigits /= 26) % 26],
                V2[(lastFourDigits /= 26) % 26],
                V2[lastFourDigits / 26 % 26]
            });
        }

        private static int GameNameToIntV2(string code)
        {
            var a = V2Map[code[0] - 65];
            var b = V2Map[code[1] - 65];
            var c = V2Map[code[2] - 65];
            var d = V2Map[code[3] - 65];
            var e = V2Map[code[4] - 65];
            var f = V2Map[code[5] - 65];

            var one = (a + 26 * b) & 0x3FF;
            var two = c + 26 * (d + 26 * (e + 26 * f));

            return (int) (one | ((two << 10) & 0x3FFFFC00) | 0x80000000);
        }
    }
}