using CommandLine;
using System;
using System.IO;
using System.Linq;
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
            Console.Out.WriteLine($"::set-output name={opts.OutputVariable}::{version}");
            if(!string.IsNullOrWhiteSpace(opts.EnvironmentFile))
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
                return new Version(doc.Root.Descendants("Version").First().Value);
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
