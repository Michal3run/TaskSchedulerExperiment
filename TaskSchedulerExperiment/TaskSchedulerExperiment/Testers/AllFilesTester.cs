using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Models;
using TaskSchedulerCore.Managers;
using TaskSchedulerExperiment.Saver;

namespace TaskSchedulerExperiment
{
    public class AllFilesTester : TestBase
    {
        public AllFilesTester(IResultSaver saver) : base(saver)
        { }

        public override void Test()
        {
            var result = GetProcessingOutputs();
            result.ForEach(r => Saver.Save(r.GetOutputTextInfo()));

            Saver.Save($"-------");

            foreach (var typeData in result.GroupBy(x => x.InputParamters.SchedulerType))
            {
                var avgDelay = typeData.Average(x => x.PercentOfDelayedTasks);
                Saver.Save($"Type: {typeData.Key}, AVG delay: {avgDelay}");
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
                    var output = GetProcessingOutput(parameters);
                    result.Add(output);
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
                Saver.Save($"Processing... {parameters.OriginalFileName}, schedulerType: {parameters.SchedulerType}");
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
                Delay = Convert.ToInt32(splitted[2]),
                Cov = Convert.ToInt32(splitted[3]),
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
