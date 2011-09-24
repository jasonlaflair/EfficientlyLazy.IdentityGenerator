using System;
using System.IO;
using System.IO.Compression;

namespace QuickTextFileCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var source = args[0];
                var target = args[1];

                Compress(source, target);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Compress(string source, string target)
        {
            Console.WriteLine(string.Format("Source: {0}", source));
            Console.WriteLine(string.Format("Target: {0}", target));

            using (var sr = new StreamReader(source))
            {
                using (var gz = new GZipStream(File.Create(target), CompressionMode.Compress))
                {
                    using (var sw = new StreamWriter(gz))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
