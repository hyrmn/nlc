using System;
using System.IO;

namespace nlc
{
    class Program
    {
        static void Main(string[] args)
        {
            LineCounter counter = new LineCounter();
            int count = 0;

            if (Console.IsInputRedirected)
            {
                count = counter.CountLines(Console.OpenStandardInput());
            }
            else
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage:\n\tlc \"path\\to\\file.txt\"");
                    return;
                }

                try
                {
                    using var file = new FileStream(args[0], FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: LineCounter.BufferSize, FileOptions.SequentialScan);
                    count = counter.CountLines(file);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Could not find {args[0]}. Check the file path.");
                }
            }

            Console.WriteLine(count);
        }
    }
}
