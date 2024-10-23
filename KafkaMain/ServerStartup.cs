using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KafkaMain
{
    public static class ServerStartup
    {
        public static void StartServer()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/k cd C:\\kafka",
                    RedirectStandardInput = true,      // Redirect input so we can send commands

                    UseShellExecute = false,           // Do not use shell execute
                    CreateNoWindow = false
                }
            };
            Console.WriteLine("going to start");
            process.Start();
            process.StandardInput.WriteLine(@".\bin\windows\zookeeper-startup.bat .\config\zookeeper.properties");
            //process.StandardInput.WriteLine("cd c");
            //process.StandardInput.WriteLine("cd kafka");
            //string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            //Console.WriteLine(output);
        }
    }
}
