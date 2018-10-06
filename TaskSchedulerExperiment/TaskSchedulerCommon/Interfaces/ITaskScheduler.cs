using System.Collections.Generic;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCommon.Interfaces
{
    public interface ITaskScheduler
    {
        void AddNewTasks(IEnumerable<TaskModel> tasks);
        void Process(int currentTime);
        bool AllCurrentTasksAreDone { get; }
        ProcessingOutput GetProcessingOutput();
    }
}
