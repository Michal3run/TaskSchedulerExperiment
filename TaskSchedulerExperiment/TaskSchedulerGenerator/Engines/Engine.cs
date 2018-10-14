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
        ISaver Saver;
        IConfiguration Configuration;

        public Engine(ITaskListGenerator taskListGenerator, ISaver saver, IConfiguration configuration)
        {
            TaskListGenerator = taskListGenerator;
            Saver = saver;
            Configuration = configuration;
        }

        public void Process()
        {
            var tickLength = Configuration.TickLength;
            var tasks = TaskListGenerator.GenerateTaskList(Configuration.SimulationLength/tickLength, tickLength);
            Saver.Save(tasks);
        }
    }
}
