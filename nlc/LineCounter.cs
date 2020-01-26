using System;
using System.IO;

namespace nlc
{
    public class LineCounter
    {
        public const int BufferSize = 32 * 1024;
        private const byte rune = (byte)'\n';

        public int CountLines(Stream stream)
        {
            int read;
            int idxOf;

            Span<byte> buffer = new Span<byte>(new byte[BufferSize]);
            int count = 0;

            while ((read = stream.Read(buffer)) > 0)
            {
                var slice = buffer.Slice(0, read);
                while ((idxOf = slice.IndexOf(rune)) > -1)
                {
                    slice = slice.Slice(idxOf + 1);
                    count++;
                }
            }

            return count;
        }
    }
}
