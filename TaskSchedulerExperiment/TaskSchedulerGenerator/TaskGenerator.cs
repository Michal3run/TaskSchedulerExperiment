using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.Engines;
using TaskSchedulerGenerator.TaskGenerators;
using TaskSchedulerGenerator.TaskIO;
using TaskSchedulerGenerator.VariableCalculators;

namespace TaskSchedulerGenerator
{
    public class TaskGenerator
    {
        ContainerBuilder Builder;

        internal TaskGenerator(ISaver saver, IConfiguration configuration) : this()
        {
            Builder.RegisterInstance(saver).As<ISaver>();
            Builder.RegisterInstance(configuration).As<IConfiguration>();
        }

        private TaskGenerator()
        {
            Builder = new ContainerBuilder();
            Builder.RegisterType<TaskQuantityCalculator>().As<ITaskQuantityCalculator>();
            Builder.RegisterType<Engine>().As<IEngine>();
            Builder.RegisterType<TaskListGenerator>().As<ITaskListGenerator>();
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
