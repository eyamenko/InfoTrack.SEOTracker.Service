using FluentScheduler;
using InfoTrack.SEOTracker.Service.DependencyResolver;
using InfoTrack.SEOTracker.Service.Contracts;
using Serilog;
using System;
using System.Threading;
using System.Linq;

namespace InfoTrack.SEOTracker.Service
{
    class Program
    {
        private static readonly string[] EXIT_COMMANDS = { "exit", "quit" };

        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            Log.Information("InfoTrack.SEOTracker.Service");

            var worker = DependencyConfiguration.Resolve<IWorker>();

            JobManager.AddJob(() => worker.Run().Wait(), s => s.ToRunNow().AndEvery(1).Days());

            WaitForExit();

            Log.Information("Exiting...");
            Thread.Sleep(1000);
        }

        static void WaitForExit()
        {
            while (true)
            {
                var command = Console.ReadLine();

                if (EXIT_COMMANDS.Any(c => c.Equals(command, StringComparison.OrdinalIgnoreCase)))
                    return;

                ShowHelp();
            }
        }

        static void ShowHelp()
        {
            Log.Information(string.Join("|", EXIT_COMMANDS) + "\t" + "exit application");
        }
    }
}