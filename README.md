[![build](https://github.com/jasondavis303/GitHub.Actions.ReadVersion/actions/workflows/release.yml/badge.svg)](https://github.com/jasondavis303/GitHub.Actions.ReadVersion/actions/workflows/release.yml)

Windows Exe: https://github.com/jasondavis303/GitHub.Actions.ReadVersion/releases/latest/download/garv.exe

Linux Exe: https://github.com/jasondavis303/GitHub.Actions.ReadVersion/releases/latest/download/garv

<pre>
Usage: garv.exe [options]

  --from-file        Required.

  --out-var          (Default: READ_VERSION)

  --env-file         GitHub Actions environment file to write to. 
                     (As of this release, doesn't seem to work on Windows)

  --zero-on-fail     Return 0.0.0.0 if there is any problem reading the file

  --help             Display this help screen.

  --version          Display version information.
</pre>