using System.Collections.Generic;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCommon.Interfaces
{
    public interface IQueueManager
    {
        IEnumerable<TaskModel> GetTasksToProcess(int currentTime);
    }
}
