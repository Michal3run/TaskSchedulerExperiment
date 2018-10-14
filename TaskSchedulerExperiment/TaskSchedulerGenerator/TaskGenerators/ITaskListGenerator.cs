using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerGenerator.TaskGenerators
{
    interface ITaskListGenerator
    {
        IEnumerable<TaskModel> GenerateTaskList(int simulationLength, int taskCount);
    }
}
