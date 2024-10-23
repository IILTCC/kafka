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
        public static void StartKafkaServer()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    // opening a 2 new cmds and running there zookeeper and kafka server
                    Arguments = "/c start cmd.exe /k \"cd C:\\kafka && .\\bin\\windows\\zookeeper-startup.bat .\\config\\zookeeper.properties\"",
                    RedirectStandardInput = true,     
                    UseShellExecute = false,     
                    CreateNoWindow = false
                }
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
