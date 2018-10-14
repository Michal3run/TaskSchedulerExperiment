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
            const int simulationLength = 10000;
            const int tickLength = 10;

            ITaskListGenerator taskListGenerator = Substitute.For<ITaskListGenerator>();
            taskListGenerator.GenerateTaskList(simulationLength / tickLength, tickLength).Returns(expectedArray);
            

            ISaver saver = Substitute.For<ISaver>();

            IConfiguration configuration = Substitute.For<IConfiguration>();                
            configuration.SimulationLength.Returns(simulationLength);
            configuration.TickLength.Returns(tickLength);

            var engine = new Engine(taskListGenerator, saver, configuration);


            engine.Process();
            
            taskListGenerator.Received().GenerateTaskList(simulationLength/tickLength, tickLength);
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
