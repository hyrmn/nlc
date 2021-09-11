using Microsoft.Toolkit.HighPerformance;

using System.Runtime.CompilerServices;

namespace nlc;

public static class Counter
{
    const int BufferSize = 512 * 1024; //A "reasonable" bigger is better when reading from a really big file
    const byte Rune = (byte)'\n';

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int CountLines(Stream stream)
    {
        var count = 0;
        int bytesRead;

        var buffer = new Span<byte>(new byte[BufferSize]);

        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            count += buffer.Slice(0, bytesRead).Count(Rune);
        }

        return count;
    }
}
