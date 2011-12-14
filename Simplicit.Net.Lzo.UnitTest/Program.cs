using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

			// Now compress the 70000 byte string to something much smaller
			byte[] compressed = lzo.Compress(Encoding.Default.GetBytes(str));
			Console.WriteLine("Compressed-Length: " + compressed.Length);

			// Decompress the string to its original content
			string str2 = Encoding.Default.GetString(lzo.Decompress(compressed));
			Console.WriteLine("Decompressed-Length: " + str2.Length);
			Console.WriteLine("Equality: " + str.Equals(str2));
		}
	}
}
