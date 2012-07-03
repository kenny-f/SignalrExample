using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            new SignalrConsoleServerTestApplication().Start();
            
           // new LoggingApplication().Start();

            // Keep going until somebody hits 'x'
            while (true)
            {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.X)
                {
                    break;
                }
            }
        }
    }
}
