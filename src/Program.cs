using nlc;

var count = 0;

if (Console.IsInputRedirected)
{
    count = Counter.CountLines(Console.OpenStandardInput());
}
else
{
    if (args.Length == 0)
    {
        Console.WriteLine("Usage:\n\tlc \"path\\to\\file.txt\"");
        return;
    }
    if (!File.Exists(args[0]))
    {
        Console.WriteLine($"Could not find {args[0]}. Check the file path.");
        return;
    }

    using var file = new FileStream(args[0], FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: 1, FileOptions.SequentialScan);
    count = Counter.CountLines(file);
}

Console.WriteLine(count);
