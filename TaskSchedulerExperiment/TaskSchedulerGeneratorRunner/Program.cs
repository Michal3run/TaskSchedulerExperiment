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
            var loads = Enumerable.Range(0, 5).Select(x => 0.1m + x * 0.2m);
            var taskLengths = new Dictionary<string, int>
            {
                {"short", 5 },
                {"medium", 20 },
                {"long", 40 }
            };
            var taskDelays = new Dictionary<string, float>
            {
                {"short", 5f },
                {"medium", 10f },
                {"long", 15f }
            };

            var testCases = loads.SelectMany(x => taskLengths, (load, taskLength) => new { load, taskLength })
                .SelectMany(x => taskDelays, (x, delay) => new { x.load, x.taskLength, delay });

            foreach(var testCase in testCases)
            {
                var configuration = new Configuration
                {
                    MaxDelayGenerator = new UniformRandomNumberGenerator(),
                    TaskLengthGenerator = new UniformRandomNumberGenerator(),
                    TaskPerTickGenerator = new UniformTaskPerTickGenerator(),
                    SimulationLength = 1000000,
                    TickLength = 50,
                    SystemLoad = testCase.load,

                    MeanTaskLength = testCase.taskLength.Value,
                    CoefficientOfVariationTaskLength = 0.2f,

                    MeanMaxDelay = testCase.delay.Value,
                    CoefficientOfVariationMaxDelay = 0.2f,

                    CoefficientOfVariationTaskPerTick = 0.5f,

                    OutputPath = $@"..\..\..\Input\output_load-{testCase.load}_task-{testCase.taskLength.Key}_delay-{testCase.delay.Key}.csv",
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