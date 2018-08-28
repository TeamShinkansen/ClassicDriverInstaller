using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ClassicDriverInstaller
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string file; // Contains name of certificate file
            X509Store store = new X509Store(StoreName.AuthRoot, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(X509Certificate2.CreateFromSignedFile(Path.Combine(appPath, "Nintendo_Classic.cat"))));
            store.Close();
            var process = Process.Start(new ProcessStartInfo()
            {
                FileName = "pnputil",
                Arguments = $"-i -a \"{Path.Combine(appPath, "Nintendo_Classic.inf")}\"",
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden
                
            });
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
