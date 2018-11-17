using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Models;
using TaskSchedulerCore.Managers;

namespace TaskSchedulerExperiment
{
    public class AllFilesTester : TestBase
    {
        public override void Test()
        {
            var result = GetProcessingOutputs();
            result.ForEach(r => Console.WriteLine(r.GetOutputTextInfo()));

            Console.WriteLine($"-------");

            foreach (var typeData in result.GroupBy(x => x.InputParamters.SchedulerType))
            {
                var avgDelay = typeData.Average(x => x.PercentOfDelayedTasks);
                Console.WriteLine($"Type: {typeData.Key}, AVG delay: {avgDelay}");
            }
        }

        private List<ProcessingOutput> GetProcessingOutputs()
        {
            var files = Directory.GetFiles(FileDirectory).Where(x => x.Contains("csv")).Select(x => Path.GetFileName(x)).ToList();

            var result = new List<ProcessingOutput>();

            foreach (var file in files)
            {
                //foreach (var scheduler in Enum.GetValues(typeof(ESchedulerType)).Cast<ESchedulerType>().ToList())
                Parallel.ForEach(Enum.GetValues(typeof(ESchedulerType)).Cast<ESchedulerType>().ToList(), scheduler =>
                {
                    var parameters = GetInputParamters(file, scheduler);
                    if (parameters.Load > 0.8m && scheduler == ESchedulerType.DoubleQueue)
                    {
                        var output = GetProcessingOutput(parameters);
                        result.Add(output);
                    }
                });
                //}
            }

            result = result.OrderByDescending(x => x.PercentOfDelayedTasks).ToList();
            return result;
        }

        private ProcessingOutput GetProcessingOutput(InputParamters parameters)
        {
            var serverParameters = GetServerParameters(parameters.SchedulerType, parameters.OriginalFileName);

            using (var serverManager = new ServerManager(serverParameters))
            {
                Console.WriteLine($"Processing... {parameters.OriginalFileName}, schedulerType: {parameters.SchedulerType}");
                var output = serverManager.GetProcessingOutput();
                output.InputParamters = parameters;
                return output;
            }
        }

        private InputParamters GetInputParamters(string filePath, ESchedulerType schedulerType)
        {
            var splitted = filePath.Split('.')[0].Split('_');
            return new InputParamters
            {
                OriginalFileName = filePath,
                SchedulerType = schedulerType,
                Load = Convert.ToDecimal(splitted[0]),
                TaskLength = Convert.ToInt32(splitted[1]),
                Delay = Convert.ToInt32(splitted[2])
            };
        }

        private ServerParameters GetServerParameters(ESchedulerType schedulerType, string fileName)
        {
            return new ServerParameters
            {
                SchedulerType = schedulerType,
                TasksFilePath = GetFileWithPath(fileName)
            };
        }
    }
}
