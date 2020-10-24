using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ReopenOnClose
{
    internal class Program
    {
        // if you want to allow only one instance otherwise remove the next line
        private static readonly Mutex mutex = new Mutex(false, "YOURGUID-YOURGUID-YOURGUID-YO");

        private static readonly ManualResetEvent run = new ManualResetEvent(true);
        public static string program = "notepad";
        private static EventHandler exitHandler;

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private static bool ExitHandler(CtrlType sig)
        {
            Console.WriteLine("Shutting down: " + sig);
            run.Reset();
            Thread.Sleep(2000);
            return
                false; // If the function handles the control signal, it should return TRUE. If it returns FALSE, the next handler function in the list of handlers for this process is used (from MSDN).
        }


        private static void Main(string[] args)
        {
            //set window on top
            var hWnd = Process.GetCurrentProcess().MainWindowHandle;
            SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);

            // if you want to allow only one instance otherwise remove the next 4 lines
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false)) return; // singleton application already started

            exitHandler += ExitHandler;
            SetConsoleCtrlHandler(exitHandler, true);

            try
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.SetBufferSize(Console.BufferWidth, 1024);
                SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);


                Console.Title = "Your Console Title - XYZ";

                // start your threads here
                var thread1 = new Thread(ThreadFunc1);
                thread1.Start();

                var thread2 = new Thread(ThreadFunc2);
                thread2.IsBackground = true; // a background thread
                thread2.Start();

                while (run.WaitOne(0)) Thread.Sleep(100);

                // do thread syncs here signal them the end so they can clean up or use the manual reset event in them or abort them
                thread1.Abort();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("fail: ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null) Console.WriteLine("Inner: " + ex.InnerException.Message);
            }
            finally
            {
                // do app cleanup here
                Process.Start(Environment.CurrentDirectory + @"\ReopenOnClose.exe");
                // if you want to allow only one instance otherwise remove the next line
                mutex.ReleaseMutex();
            }
        }

        public static void ThreadFunc1()
        {
            Console.Write("> Everything done now -_-");
        }


        public static void ThreadFunc2()
        {
            Process[] pname;
            while (true)
            {
                pname = Process.GetProcessesByName(program);
                if (pname.Length != 0)
                    try
                    {
                        pname[0].Kill();
                        Array.Clear(pname, 0, pname.Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
            }
        }

        private delegate bool EventHandler(CtrlType sig);

        private enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        #region Window on top

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        #endregion

        #region Disable Buttons

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion
    }
}