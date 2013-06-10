using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace ResourceTranslatorTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string teststring = string.Empty;

            for (int i = 0; i < 1000; i++)
            {
                teststring += "test";
            }

            int size = teststring.Length * sizeof(char);

            if (size > 65536 / 20)
            {
                Console.WriteLine("sdsd");
            }

            Console.ReadLine();
        }

        private static void NewMethod()
        {
            string pattern = string.Empty;

            var cultures = CultureInfo.GetCultures(CultureTypes.FrameworkCultures);

            pattern += "(";
            foreach (var culture in cultures)
            {
                pattern += "\\." + culture.Name + "\\.resx" + "|";
            }
            pattern = pattern.TrimEnd(new char[] { '|' });
            pattern += ")";

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

            string[] files = Directory.GetFiles(@"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\Resources", "*.resx");

            string filename = string.Empty;
            string selectedCulture = "fr";

            foreach (var file in files)
            {
                filename = Path.GetFileName(file);
                Console.WriteLine("Original Filename: {0}", filename);
                var m = r.Match(filename);

                if (m.Success)
                {
                    filename = r.Replace(filename, "." + selectedCulture + ".resx");
                    Console.WriteLine("New Filename: {0}", filename);
                }
            }
        }
    }
}
