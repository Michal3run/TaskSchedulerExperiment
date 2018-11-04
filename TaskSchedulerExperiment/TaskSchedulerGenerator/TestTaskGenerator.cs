﻿using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskIO;
using Shouldly;

namespace TaskSchedulerGenerator
{
    [TestFixture]
    class TestTaskGenerator
    {

        [Test]
        public void TestProcessInternalIntegration()
        {
            var saver = Substitute.For<ISaver>();

            var configuration = new Configuration
            {
                MaxDelayGenerator = new UniformRandomNumberGenerator(),
                TaskLengthGenerator = new UniformRandomNumberGenerator(),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(),
                SimulationLength = 10,
                TickLength = 10,
                SystemLoad = 1m,
                MeanTaskLength = 2,
                CoefficientOfVariationTaskLength = 0,
                MeanMaxDelay = 1f,
                CoefficientOfVariationMaxDelay = 0,
                CoefficientOfVariationTaskPerTick = 0
            };

            var generator = new TaskGenerator(saver, configuration);

            generator.Generate();

            saver.Received().Save(Arg.Is<IEnumerable<TaskModel>>(x => x.Count() == 5));
        }

        [Test]
        public void TestStatisticalParametersOfCreatedTasks()
        {
            const int averageMaxDelay = 12;
            
            var configuration = new Configuration
            {
                MaxDelayGenerator = new UniformRandomNumberGenerator(),
                TaskLengthGenerator = new UniformRandomNumberGenerator(),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(),
                SimulationLength = 100000,
                TickLength = 20,
                SystemLoad = 0.5m,
                MeanTaskLength = 2,
                CoefficientOfVariationTaskLength = 0.5f,
                MeanMaxDelay = averageMaxDelay,
                CoefficientOfVariationMaxDelay = 2f / 3f,
                CoefficientOfVariationTaskPerTick = 1f
            };

            var saver = new StatisticalValidatorSaver((float)configuration.SystemLoad, configuration.SimulationLength, averageMaxDelay);
            var generator = new TaskGenerator(saver, configuration);

            generator.Generate();
        }

        [Test]
        public void TestStatisticalParametersOfLargeData()
        {
            const int averageMaxDelay = 12;

            var configuration = new Configuration
            {
                MaxDelayGenerator = new UniformRandomNumberGenerator(),
                TaskLengthGenerator = new UniformRandomNumberGenerator(),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(),
                SimulationLength = 1000000,
                TickLength = 20,
                SystemLoad = 0.5m,

                MeanTaskLength = 5,
                CoefficientOfVariationTaskLength = 0.2f,

                MeanMaxDelay = 12,
                CoefficientOfVariationMaxDelay = 2f / 3f,

                CoefficientOfVariationTaskPerTick = 1f,
            };

            var saver = new StatisticalValidatorSaver((float)configuration.SystemLoad, configuration.SimulationLength, averageMaxDelay);
            var generator = new TaskGenerator(saver, configuration);

            generator.Generate();
        }

        private class StatisticalValidatorSaver : ISaver
        {
            float ExpectedLoad;
            int SimulationLength;
            int AverageMaxDelay;

            public StatisticalValidatorSaver(float expectedLoad, int simulationLength, int averageMaxDelay)
            {
                ExpectedLoad = expectedLoad;
                SimulationLength = simulationLength;
                AverageMaxDelay = averageMaxDelay;
            }

            public void Save(IEnumerable<TaskModel> tasksEnumerable)
            {
                var tasks = tasksEnumerable.ToList();
                float load = tasks.Sum(x => x.Duration) / (float)SimulationLength;
                load.ShouldBe(ExpectedLoad, 0.05f);
                float averageDelay = tasks.Sum(x => x.MaxWaitingTime) / (float)tasks.Count();
                averageDelay.ShouldBe((float)AverageMaxDelay, 0.1f);
            }
        }

    }
}
