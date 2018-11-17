using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Models;
using TaskSchedulerCore.Managers;

namespace TaskSchedulerExperiment
{
    public class OneFileTester : TestBase
    {
        private readonly string _fileName;

        public OneFileTester(string fileName)
        {
            _fileName = fileName;
        }

        public override void Test()
        {
            Console.WriteLine("Getting parameters");

            var parameters = GetServerParameters(_fileName);

            Console.WriteLine($"SchedulerType: {parameters.SchedulerType.ToString()}");
            Console.WriteLine("Creating server manager");

            using (var serverManager = new ServerManager(parameters))
            {
                Console.WriteLine("Processing...");
                var output = serverManager.GetProcessingOutput();
                Console.WriteLine($"Finished! Percent of delayed tasks: {output?.PercentOfDelayedTasks} %");                
            }
        }
        
        private ServerParameters GetServerParameters(string fileName)
        {
            return new ServerParameters
            {
                SchedulerType = ESchedulerType.DoubleQueue,
                TasksFilePath = $@"..\..\..\Input\{fileName}.csv"
            };
        }
    }
}