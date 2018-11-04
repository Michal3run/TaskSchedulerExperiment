using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskIO;
using TaskSchedulerGenerator.VariableCalculators;

namespace TaskSchedulerGenerator.TaskGenerators
{
    [TestFixture]
    class TestTaskListGenerator
    {
        [Test]
        public void TestGenerateTaskListWithConstantGenerators()
        {
            var listGenerator = CreateGenerator(1, 2, 10);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 10).Select(x => new TaskModel { CreateTime = x, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        [Test]
        public void TestGenerateTaskListWithConstantGeneratorsAndDoubleRate()
        {
            var listGenerator = CreateGenerator(1, 2, 20);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 20).Select(x => new TaskModel { CreateTime = x/2, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        [Test]
        public void TestGenerateTaskListWithConstantGeneratorsGeneratingError()
        {
            var listGenerator = CreateGenerator(1, 2, 5);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 5).Select(x => new TaskModel { CreateTime = x * 2, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        private ITaskListGenerator CreateGenerator(int taskLength, int maxDelay, int taskPerTick)
        {
            var taskLengthGenerator = new ConstantGenerator();
            var maxDelayGenerator = CreateConstantNumberGenerator<IMaxDelayGenerator>(maxDelay);
            var taskPerTickGenerator = CreateConstantNumberGenerator<ITaskPerTickGenerator>(taskPerTick);
            var configuration = CreateConfiguration(0, taskLength);

            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator, configuration, new TaskQuantityCalculator(), new AverageTaskDurationCalculator());
            return listGenerator;
        }

        private T CreateConstantNumberGenerator<T>(int constantNumber) where T: class, INumberGenerator
        {
            var generator = Substitute.For<T>();
            generator.GetNumber().Returns(constantNumber);
            return generator;
        }

        private IConfiguration CreateConfiguration(decimal load, int taskLength)
        {
            var configuration = Substitute.For<IConfiguration>();
            configuration.SystemLoad.Returns(load);
            configuration.MeanTaskLength.Returns(taskLength);
            configuration.CoefficientOfVariationTaskLength.Returns(0);
            return configuration;
        }
    }
}
