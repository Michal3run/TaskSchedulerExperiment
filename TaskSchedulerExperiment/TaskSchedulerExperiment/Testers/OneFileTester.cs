using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Models;
using TaskSchedulerCore.Managers;
using TaskSchedulerExperiment.Saver;

namespace TaskSchedulerExperiment
{
    public class OneFileTester : TestBase
    {
        private readonly string _fileName;

        public OneFileTester(IResultSaver saver, string fileName) : base(saver)
        {
            _fileName = fileName;
        }

        public override void Test()
        {
            Saver.Save("Getting parameters");

            var parameters = GetServerParameters(_fileName);

            Saver.Save($"SchedulerType: {parameters.SchedulerType.ToString()}");
            Saver.Save("Creating server manager");

            using (var serverManager = new ServerManager(parameters))
            {
                Saver.Save("Processing...");
                var output = serverManager.GetProcessingOutput();
                Saver.Save($"Finished! Percent of delayed tasks: {output?.PercentOfDelayedTasks} %");                
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