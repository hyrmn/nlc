using Microsoft.Toolkit.HighPerformance;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace nlc;

public static class Counter
{
    const int BufferSize = 512 * 1024; //A "reasonable" bigger is better when reading from a really big file
    const byte Rune = (byte)'\n';
    const int vectorSize = 256 / 8;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe int CountLines(Stream stream)
    {
        var count = 0;
        int bytesRead;

        void* ptr = (void*)NativeMemory.AlignedAlloc(byteCount: BufferSize, alignment: vectorSize);
        Span<byte> buffer = new Span<byte>(ptr, BufferSize);

        try
        {
            while ((bytesRead = stream.Read(buffer)) > 0)
            {
                count += buffer.Slice(0, bytesRead).Count(Rune);
            }
        }
        finally
        {
            NativeMemory.AlignedFree(ptr);
        }

        return count;
    }
}
