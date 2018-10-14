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

namespace TaskSchedulerGenerator.TaskGenerators
{
    [TestFixture]
    class TestTaskListGenerator
    {
        [Test]
        public void TestGenerateTaskListWithConstantGenerators()
        {
            INumberGenerator taskLengthGenerator = CreateConstantNumberGenerator(1);
            INumberGenerator maxDelayGenerator = CreateConstantNumberGenerator(2);
            INumberGenerator taskPerTickGenerator = CreateConstantNumberGenerator(10);
            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 10).Select(x => new TaskModel { CreateTime = x, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        [Test]
        public void TestGenerateTaskListWithConstantGeneratorsAndDoubleRate()
        {
            INumberGenerator taskLengthGenerator = CreateConstantNumberGenerator(1);
            INumberGenerator maxDelayGenerator = CreateConstantNumberGenerator(2);
            INumberGenerator taskPerTickGenerator = CreateConstantNumberGenerator(20);
            var listGenerator = new TaskListGenerator(taskLengthGenerator, maxDelayGenerator, taskPerTickGenerator);

            var tasks = listGenerator.GenerateTaskList(1, 10);

            var expectedTasks = Enumerable.Range(0, 20).Select(x => new TaskModel { CreateTime = x/2, Duration = 1, MaxWaitingTime = 2 });
            tasks.ShouldBe(expectedTasks);

        }

        private INumberGenerator CreateConstantNumberGenerator(int constantNumber)
        {
            var generator = Substitute.For<INumberGenerator>();
            generator.GetNumber().Returns(constantNumber);
            return generator;
        }
    }
}
