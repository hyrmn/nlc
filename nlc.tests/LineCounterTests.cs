using System;
using System.IO;
using System.Text;
using Xunit;

namespace nlc.tests
{
    public class LineCounterTests
    {
        [Fact]
        public void CountLines_SingleLine()
        {
            using var sample = new MemoryStream(Encoding.UTF8.GetBytes("Simple line\n"));
            var sut = new LineCounter();
            var want = 1;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }

        [Fact]
        public void ountLines_SingleLineNoEndingBreak()
        {
            using var sample = new MemoryStream(Encoding.UTF8.GetBytes("Simple line"));
            var sut = new LineCounter();
            var want = 0;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }

        [Fact]
        public void CountLines_MultiLine()
        {
            using var sample = new MemoryStream(Encoding.UTF8.GetBytes("Line1\nLine2\nLine3\n"));
            var sut = new LineCounter();
            var want = 3;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }

        [Fact]
        public void CountLines_MultiLineNoEndingBreak()
        {
            using var sample = new MemoryStream(Encoding.UTF8.GetBytes("Line1\nLine2\nLine3"));
            var sut = new LineCounter();
            var want = 2;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }

        [Fact]
        public void CountLines_EmptyFile()
        {
            using var sample = new FileStream(@"testdata\empty.txt", FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: LineCounter.BufferSize, FileOptions.SequentialScan);
            var sut = new LineCounter();
            var want = 0;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }

        [Fact]
        public void CountLines_Ipsum()
        {
            using var sample = new FileStream(@"testdata\ipsum.txt", FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: LineCounter.BufferSize, FileOptions.SequentialScan);
            var sut = new LineCounter();
            var want = 9;
            var got = sut.CountLines(sample);

            Assert.Equal(want, got);
        }
    }
}
