using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;
using TaskSchedulerGenerator.TaskGenerators;
using TaskSchedulerGenerator.TaskIO;
using TaskSchedulerGenerator.VariableCalculators;

namespace TaskSchedulerGenerator.Engines
{
    [TestFixture]
    class TestEngine
    {

        [Test]
        public void TestProcess()
        {
            var expectedArray = new List<TaskModel>
            {
                CreateTask(0,1,2),
                CreateTask(1,1,4),
                CreateTask(2,2,4)
            };
            const int meanTaskLength = 5;
            const decimal systemLoad = 0.75m;
            const int simulationLength = 10000;
            const int taskCount = 3;

            ITaskListGenerator taskListGenerator = Substitute.For<ITaskListGenerator>();
            taskListGenerator.GenerateTaskList(simulationLength, taskCount).Returns(expectedArray);

            ITaskQuantityCalculator taskQuantityCalculator = Substitute.For<ITaskQuantityCalculator>();
            taskQuantityCalculator.CalculateTaskQuantity(simulationLength, systemLoad, meanTaskLength).Returns(taskCount);

            ISaver saver = Substitute.For<ISaver>();

            IConfiguration configuration = Substitute.For<IConfiguration>();            
            configuration.MeanTaskLength.Returns(meanTaskLength);            
            configuration.SystemLoad.Returns(systemLoad);           
            configuration.SimulationLength.Returns(simulationLength);

            var engine = new Engine(taskListGenerator, taskQuantityCalculator, saver, configuration);


            engine.Process();

            taskQuantityCalculator.Received().CalculateTaskQuantity(simulationLength, systemLoad, meanTaskLength);
            taskListGenerator.Received().GenerateTaskList(simulationLength, taskCount);
            saver.Received().Save(Arg.Is<IEnumerable<TaskModel>>(x => x == expectedArray));
        }


        TaskModel CreateTask(int createTime, int duration, int maxWaitingTime)
        {
            return new TaskModel
            {
                CreateTime = createTime,
                Duration = duration,
                MaxWaitingTime = maxWaitingTime
            };
        }
    }
}
