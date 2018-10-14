using System;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Models;
using TaskSchedulerCore.Managers;

namespace TaskSchedulerExperiment
{
    class Tester
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting parameters");

            var parameters = GetServerParameters();

            Console.WriteLine($"SchedulerType: {parameters.SchedulerType.ToString()}");
            Console.WriteLine("Creating server manager");

            using (var serverManager = new ServerManager(parameters))
            {
                Console.WriteLine("Processing...");
                var output = serverManager.GetProcessingOutput();
                Console.WriteLine($"Finished! Percent of delayed tasks: {output?.PercentOfDelayedTasks} %");
                Console.WriteLine($"Press any key to exit");
                Console.ReadKey();
            }
        }

        private static ServerParameters GetServerParameters()
        {
            return new ServerParameters
            {
                SchedulerType = ESchedulerType.RoundRobin
            };
        }
    }
}
