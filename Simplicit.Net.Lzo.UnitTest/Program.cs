using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Simplicit.Net.Lzo.UnitTest
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the compressor object
			LZOCompressor lzo = new LZOCompressor();

			// Build a quite redundant string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 10000; i++)
			{
				sb.Append("LZO.NET");
			}
			string str = sb.ToString();
			Console.WriteLine("Original-Length: " + str.Length);

			byte[] data = Encoding.Default.GetBytes(str);

			// Now compress the 70000 byte string to something much smaller
			byte[] compressed = lzo.Compress(data);
			Console.WriteLine("Compressed-Length: " + compressed.Length);

			// Decompress the string to its original content
			string str2 = Encoding.Default.GetString(lzo.Decompress(compressed));
			Console.WriteLine("Decompressed-Length: " + str2.Length);
			Console.WriteLine("Equality: " + str.Equals(str2));

			Console.WriteLine("benchmark...");
			Stopwatch stopWatch = new Stopwatch();
			int count = 0;
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < 2000)
			{
				lzo.Compress(data);
				count++;
			}
			stopWatch.Stop();
			Console.WriteLine("Compression throughput is (KByte/sec): " + data.Length * count / stopWatch.ElapsedMilliseconds);

			count = 0;
			stopWatch.Reset();
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < 2000)
			{
				lzo.Decompress(compressed);
				count++;
			}
			stopWatch.Stop();
			Console.WriteLine("Uncompression throughput is (KByte/sec): " + data.Length * count / stopWatch.ElapsedMilliseconds);

			Console.ReadKey();
		}
	}
}
