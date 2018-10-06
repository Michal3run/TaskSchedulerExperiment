using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Managers
{
    public class QueueManager : IQueueManager
    {
        private readonly Queue<TaskModel> _tasks;

        public QueueManager(IEnumerable<TaskModel> tasks)
        {
            _tasks = new Queue<TaskModel>(tasks.OrderBy(x => x.CreateTime));
        }

        public IEnumerable<TaskModel> GetTasksToProcess(int currentTime)
        {
            var result = new List<TaskModel>();

            while (_tasks.Any() && IsTaskReady(_tasks.Peek(), currentTime))
            {
                result.Add(_tasks.Dequeue());
            }

            return result;
        }

        /// <summary>
        /// Returns true if task is present or past 
        /// </summary>
        private bool IsTaskReady(TaskModel task, int currentTime) => task.CreateTime <= currentTime;
    }
}
