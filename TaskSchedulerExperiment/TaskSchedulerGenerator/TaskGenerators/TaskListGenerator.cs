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
    class TaskListGenerator : ITaskListGenerator
    {
        ITaskLengthGenerator TaskLengthGenerator;
        IMaxDelayGenerator MaxDelayGenerator;
        ITaskPerTickGenerator TaskPerTickGenerator;
        IConfiguration Configuration;
        ITaskQuantityCalculator TaskQuantityCalculator;
        IAverageTaskDurationCalculator AverageTaskDurationCalculator;

        public TaskListGenerator(ITaskLengthGenerator taskLengthGenerator, IMaxDelayGenerator maxDelayGenerator, ITaskPerTickGenerator taskPerTickGenerator, IConfiguration configuration, ITaskQuantityCalculator taskQuantityCalculator, IAverageTaskDurationCalculator averageTaskDurationCalculator)
        {
            TaskLengthGenerator = taskLengthGenerator;
            MaxDelayGenerator = maxDelayGenerator;
            TaskPerTickGenerator = taskPerTickGenerator;
            Configuration = configuration;
            TaskQuantityCalculator = taskQuantityCalculator;
            AverageTaskDurationCalculator = averageTaskDurationCalculator;
        }

        public IEnumerable<TaskModel> GenerateTaskList(int ticks, int tickLength)
        {
            var taskQuantity = TaskQuantityCalculator.CalculateTaskQuantity(Configuration.SimulationLength, Configuration.SystemLoad, Configuration.MeanTaskLength);
            var meanTaskPerTick = taskQuantity / (float)ticks;

            TaskPerTickGenerator.Initialize(meanTaskPerTick, Configuration.CoefficientOfVariationTaskPerTick);
            TaskLengthGenerator.Initialize(Configuration.MeanTaskLength, Configuration.CoefficientOfVariationTaskLength);
            MaxDelayGenerator.Initialize(Configuration.MeanMaxDelay, Configuration.CoefficientOfVariationMaxDelay);

            foreach (var tickNo in Enumerable.Range(0, ticks))
            {
                var tasksQuantity = TaskPerTickGenerator.GetNumber();
                if (tasksQuantity > 0)
                {
                    var delayBetweenTasks = (float)tickLength / tasksQuantity;
                    for (var innerTime = 0f; innerTime < tickLength; innerTime += delayBetweenTasks)
                    {
                        yield return new TaskModel
                        {
                            CreateTime = tickNo * tickLength + (int)Math.Floor(innerTime),
                            Duration = TaskLengthGenerator.GetNumber(),
                            MaxWaitingTime = MaxDelayGenerator.GetNumber()
                        };
                    }
                }
            }
        }
    }
}
