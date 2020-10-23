using System.Diagnostics;

namespace EndAll
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Process.GetProcessesByName("Taskmanager")[0].Kill();
            Process.GetProcessesByName("MakeUnclosable")[0].Kill();
        }
    }
}