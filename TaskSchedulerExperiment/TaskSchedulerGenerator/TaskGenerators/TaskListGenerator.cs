using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;
using TaskSchedulerGenerator.NumberGenerators;

namespace TaskSchedulerGenerator.TaskGenerators
{
    class TaskListGenerator : ITaskListGenerator
    {
        INumberGenerator TaskLengthGenerator;
        INumberGenerator MaxDelayGenerator;
        INumberGenerator TaskPerTickGenerator;

        public TaskListGenerator(INumberGenerator taskLengthGenerator, INumberGenerator maxDelayGenerator, INumberGenerator taskPerTickGenerator)
        {
            TaskLengthGenerator = taskLengthGenerator;
            MaxDelayGenerator = maxDelayGenerator;
            TaskPerTickGenerator = taskPerTickGenerator;
        }

        public IEnumerable<TaskModel> GenerateTaskList(int ticks, int tickLength)
        {
            foreach (var tickNo in Enumerable.Range(0, ticks))
            {
                var tasksQuantity = Convert.ToInt32(TaskPerTickGenerator.GetNumber());
                var delayBetweenTasks = (float)tickLength / tasksQuantity;
                for(var innerTime = 0f; innerTime < tickLength; innerTime+= delayBetweenTasks)
                {
                    yield return new TaskModel
                    {
                        CreateTime = tickNo * tickLength + (int)Math.Floor(innerTime),
                        Duration = Convert.ToInt32(TaskLengthGenerator.GetNumber()),
                        MaxWaitingTime = Convert.ToInt32(MaxDelayGenerator.GetNumber())
                    };
                }
            }
        }
    }
}
