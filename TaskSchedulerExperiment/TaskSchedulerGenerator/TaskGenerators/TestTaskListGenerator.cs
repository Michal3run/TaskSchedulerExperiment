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

namespace TaskSchedulerGenerator.TaskGenerators
{
    [TestFixture]
    class TestTaskListGenerator
    {
        [Test]
        public void TestGenerateTaskListWithConstantGenerators()
        {
            var taskLengthGenerator = CreateConstantNumberGenerator<ITaskLengthGenerator>(1);
            var maxDelayGenerator = CreateConstantNumberGenerator<IMaxDelayGenerator>(2);
            var taskPerTickGenerator = CreateConstantNumberGenerator<ITaskPerTickGenerator>(10);

            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 10).Select(x => new TaskModel { CreateTime = x, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        [Test]
        public void TestGenerateTaskListWithConstantGeneratorsAndDoubleRate()
        {
            var taskLengthGenerator = CreateConstantNumberGenerator<ITaskLengthGenerator>(1);
            var maxDelayGenerator = CreateConstantNumberGenerator<IMaxDelayGenerator>(2);
            var taskPerTickGenerator = CreateConstantNumberGenerator<ITaskPerTickGenerator>(20);

            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 20).Select(x => new TaskModel { CreateTime = x/2, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        [Test]
        public void TestGenerateTaskListWithConstantGeneratorsGeneratingError()
        {
            var taskLengthGenerator = CreateConstantNumberGenerator<ITaskLengthGenerator>(1);
            var maxDelayGenerator = CreateConstantNumberGenerator<IMaxDelayGenerator>(2);
            var taskPerTickGenerator = CreateConstantNumberGenerator<ITaskPerTickGenerator>(5);

            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 5).Select(x => new TaskModel { CreateTime = x * 2, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        private T CreateConstantNumberGenerator<T>(int constantNumber) where T: class, INumberGenerator
        {
            var generator = Substitute.For<T>();
            generator.GetNumber().Returns(constantNumber);
            return generator;
        }
    }
}
