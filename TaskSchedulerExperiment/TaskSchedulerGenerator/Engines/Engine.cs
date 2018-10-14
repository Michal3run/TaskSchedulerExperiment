using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.TaskGenerators;
using TaskSchedulerGenerator.TaskIO;
using TaskSchedulerGenerator.VariableCalculators;

namespace TaskSchedulerGenerator.Engines
{
    class Engine : IEngine
    {
        ITaskListGenerator TaskListGenerator;
        ITaskQuantityCalculator TaskQuantityCalculator;
        ISaver Saver;
        IConfiguration Configuration;

        public Engine(ITaskListGenerator taskListGenerator, ITaskQuantityCalculator taskQuantityCalculator, ISaver saver, IConfiguration configuration)
        {
            TaskListGenerator = taskListGenerator;
            TaskQuantityCalculator = taskQuantityCalculator;
            Saver = saver;
            Configuration = configuration;
        }

        public void Process()
        {            
            var taskCount = TaskQuantityCalculator.CalculateTaskQuantity(Configuration.SimulationLength, Configuration.SystemLoad, Configuration.MeanTaskLength);
            var tasks = TaskListGenerator.GenerateTaskList(Configuration.SimulationLength, taskCount);
            Saver.Save(tasks);
        }
    }
}
