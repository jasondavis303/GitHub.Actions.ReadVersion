using CommandLine;

namespace GitHub.Actions.ReadVersion
{
    class CLOptions
    {
        [Option("from-file", Required = true)]
        public string FromFile { get; set; }

        [Option("out-var", Default = "READ_VERSION")]
        public string OutputVariable { get; set; }

        [Option("env-file")]
        public string EnvironmentFile { get; set; }
        
        [Option("zero-on-fail", HelpText = "Return 0.0.0.0 if there is any problem reading the file")]
        public bool ZeroOnFail { get; set; }
    }
}
