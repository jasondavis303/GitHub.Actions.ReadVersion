using CommandLine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace GitHub.Actions.ReadVersion
{
    class Program
    {
        static int Main(string[] args)
        {
            int ret = -1;

            try
            {
                Console.WriteLine("GitHub.Actions.ReadVersion");
                Console.WriteLine("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version);               
                Parser.Default.ParseArguments<CLOptions>(args)
                    .WithParsed(opts =>
                    {
                        Run(opts);
                        ret = 0;
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine();

                if (ex.HResult != 0)
                    ret = ex.HResult;
            }

            return ret;
        }

        static void Run(CLOptions opts)
        {
            var version = ReadVersion(opts);

            Console.WriteLine();
            Console.WriteLine("File: {0}", opts.FromFile);
            Console.WriteLine("Found Version: {0}", version);
            Console.WriteLine();

            Console.Out.WriteLine($"::set-output name={opts.OutputVariable}::{version}");
            if (!string.IsNullOrWhiteSpace(opts.EnvironmentFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(opts.EnvironmentFile));
                File.AppendAllLines(opts.EnvironmentFile, new string[] { $"{opts.OutputVariable}={version}" });
            }
        }

        static Version ReadVersion(CLOptions opts)
        {
            try
            {
                var doc = XDocument.Load(opts.FromFile);
                return new Version(doc.Descendants("Version").First().Value);
            }
            catch
            {
                if (opts.ZeroOnFail)
                    return new Version();

                throw;
            }
        }
    }
}
