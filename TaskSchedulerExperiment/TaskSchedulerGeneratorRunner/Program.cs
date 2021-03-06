﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskIO;

namespace TaskSchedulerGeneratorRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var loads = Enumerable.Range(0, 3).Select(x => 0.5m + x * 0.2m);
            var taskLengths = new Dictionary<string, int>
            {
                {"medium", 10 }
            };
           var taskDelays = new Dictionary<string, float>
            {
                {"medium", 8f }
            };
            var coefficientsOfVariations = new[] { 0.5f, 3, 7, 15 };

            var testCases = loads.SelectMany(x => taskLengths, (load, taskLength) => new { load, taskLength })
                .SelectMany(x => taskDelays, (x, delay) => new { x.load, x.taskLength, delay })
                .SelectMany(x => coefficientsOfVariations, (x, cov) => new { x.load, x.taskLength, x.delay, cov })
                .ToList();

            foreach (var testCase in testCases)
            {
                var configuration = new Configuration
                {
                    MaxDelayGenerator = new UniformRandomNumberGenerator(),
                    TaskLengthGenerator = new UniformRandomNumberGenerator(),
                    TaskPerTickGenerator = new ParetoTaskPerTickGenerator(),
                    SimulationLength = 1000000,
                    TickLength = 50,
                    SystemLoad = testCase.load,

                    MeanTaskLength = testCase.taskLength.Value,
                    CoefficientOfVariationTaskLength = 0.2f,

                    MeanMaxDelay = testCase.delay.Value,
                    CoefficientOfVariationMaxDelay = 0.2f,

                    CoefficientOfVariationTaskPerTick = testCase.cov,

                    OutputPath = $@"..\..\..\Input\{testCase.load}_{testCase.taskLength.Value}_{testCase.delay.Value}_{testCase.cov}.csv",
                };
                var generator = new TaskGenerator(configuration);
                generator.Generate();
            }

            /*var configuration = new Configuration
            {
                MaxDelayGenerator = new UniformRandomNumberGenerator(),
                TaskLengthGenerator = new UniformRandomNumberGenerator(),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(),
                SimulationLength = 1000000,
                TickLength = 50,
                SystemLoad = 0.5m,

                MeanTaskLength = 5,
                CoefficientOfVariationTaskLength = 0.2f,

                MeanMaxDelay = 12,
                CoefficientOfVariationMaxDelay = 0.2f,

                CoefficientOfVariationTaskPerTick = 1f,

                OutputPath = @"..\..\..\Input\output.csv",
            };
            var generator = new TaskGenerator(configuration);
            generator.Generate();*/
        }
    }
}
