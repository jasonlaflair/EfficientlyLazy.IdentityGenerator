using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace EfficientlyLazy.IdentityGenerator.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var source =
            //    @"C:\Development\EfficientlyLazy.IdentityGenerator\src\EfficientlyLazy.IdentityGenerator\DataFiles\NamesLast.txt";

            //var dest =
            //    @"C:\Development\EfficientlyLazy.IdentityGenerator\src\EfficientlyLazy.IdentityGenerator\DataFiles\NamesLast.data";

            //using (var sr = new StreamReader(source))
            //{
            //    using (var gw = new GZipStream(File.Create(dest), CompressionMode.Compress))
            //    {
            //        using (var sw = new StreamWriter(gw))
            //        {
            //            string line;

            //            while ((line = sr.ReadLine()) != null)
            //            {
            //                sw.WriteLine(line);
            //            }
            //        }
            //    }
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
