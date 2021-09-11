# Line Counter

This is a small command-line utility to count lines in text. That's it. That's all it does. Technically, it doesn't even care if it's text. 

This is the .NET 6.0 version of my [Golang-based line counter](https://github.com/hyrmn/lc). If you're interested in a faster, but also harder to read, .NET version, then check out [my version that leverages intrinsics](https://github.com/hyrmn/fnlc)

There are some counting assumptions that I made. I had originally chosen to have this match my editor's line count. That is, if Visual Studio Code shows `x` lines then my logic would also show `x` lines. However, I've chosen to follow the behavior of `wc -l`. I count carriage returns (`\n`). If a file does not end with a carriage return then the last line will not be counted.

## How to Build

Clone this repository and then run `dotnet build` from the solution root.

```posh
> dotnet build -c Release --nologo
```

This will create a release build of the utility at `/nlc/bin/Release/net6.0/nlc.exe`. From there, you will need to copy it to a place in your `%PATH%`.

## How to Use

`nlc` can either have information piped to it or it have a file path passed via the command line.

To read a file:

```
> nlc "path/to/your/file.txt"
```

To read from stdin (information piped in):

```
> echo "Count the lines in this" | nlc
```

The only output from `nlc` will be the line count. This is because I want the ability to pipe this on to other programs easily.

So the full run might look like 

```
> nlc "path/to/your/file.txt"
109
```

## Runtime Considerations

Using `time` (Unix timing utility) `wc` (Unix word count utility) on my machine to parse a 1.6GB text file of lorem ipsum text, I get the following averages after an initial warmup call:

```
real    0m0.822s
user    0m0.156s
sys     0m0.655s
```

Using `nlc` to parse the same file, I get the following averages after an initial warmup call:

```
real    0m0.619s
user    0m0.000s
sys     0m0.015s
```

Note: There is no timeout when waiting for piped input from stdin. If stdin never ends the stream then `nlc` will hang until it is force-quit (`ctrl+c`).
