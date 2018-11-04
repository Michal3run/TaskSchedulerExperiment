using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.Engines;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskGenerators;
using TaskSchedulerGenerator.TaskIO;
using TaskSchedulerGenerator.VariableCalculators;

namespace TaskSchedulerGenerator
{
    public class TaskGenerator
    {
        ContainerBuilder Builder;

        public TaskGenerator(IConfiguration configuration) : this()
        {
            Builder.RegisterInstance(configuration).As<IConfiguration>();
            Builder.RegisterType<CsvSaver>().As<ISaver>();
            Builder.Register(x => configuration.MaxDelayGenerator).As<IMaxDelayGenerator>();
            Builder.Register(x => configuration.TaskLengthGenerator).As<ITaskLengthGenerator>();
            Builder.Register(x => configuration.TaskPerTickGenerator).As<ITaskPerTickGenerator>();
        }

        internal TaskGenerator(ISaver saver, IConfiguration configuration) : this()
        {
            Builder.RegisterInstance(saver).As<ISaver>();
            Builder.RegisterInstance(configuration).As<IConfiguration>();
            Builder.Register(x => configuration.MaxDelayGenerator).As<IMaxDelayGenerator>();
            Builder.Register(x => configuration.TaskLengthGenerator).As<ITaskLengthGenerator>();
            Builder.Register(x => configuration.TaskPerTickGenerator).As<ITaskPerTickGenerator>();
        }

        private TaskGenerator()
        {
            Builder = new ContainerBuilder();
            Builder.RegisterType<TaskQuantityCalculator>().As<ITaskQuantityCalculator>();
            Builder.RegisterType<Engine>().As<IEngine>();
            Builder.RegisterType<TaskListGenerator>().As<ITaskListGenerator>();
            Builder.RegisterType<AverageTaskDurationCalculator>().As<IAverageTaskDurationCalculator>();
        }

        public void Generate()
        {
            var container = Builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var engine = scope.Resolve<IEngine>();
                engine.Process();
            }
        }
    }
}
