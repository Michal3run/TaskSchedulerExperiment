using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskIO;

namespace TaskSchedulerGenerator
{
    [TestFixture]
    class TestTaskGenerator
    {
        [Test]
        public void TestProcessInternalIntegration()
        {
            var saver = Substitute.For<ISaver>();
            var configuration = Substitute.For<IConfiguration>();

            configuration.SimulationLength.Returns(10);
            configuration.SystemLoad.Returns(0.5m);
            configuration.MeanTaskLength.Returns(1);
            configuration.TickLength.Returns(10);
            configuration.TaskLengthGenerator.Returns(new ConstantGenerator(1f));
            configuration.MaxDelayGenerator.Returns(new ConstantGenerator(2f));
            configuration.TaskPerTickGenerator.Returns(new ConstantGenerator(5));



            var generator = new TaskGenerator(saver, configuration);

            generator.Generate();

            saver.Received().Save(Arg.Is<IEnumerable<TaskModel>>(x => x.Count() == 5));
        }
        
    }
}
