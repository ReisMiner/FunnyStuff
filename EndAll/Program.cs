using System;
using System.Diagnostics;

namespace EndAll
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Process.GetProcessesByName("Taskmanager")[0].Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Process.GetProcessesByName("MakeUnclosable")[0].Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Process.GetProcessesByName("sqhost")[0].Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}