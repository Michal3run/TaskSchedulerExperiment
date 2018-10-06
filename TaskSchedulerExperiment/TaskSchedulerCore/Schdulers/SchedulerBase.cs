using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Schdulers
{
    public abstract class SchedulerBase : ITaskScheduler
    {
        protected SchedulerBase()
        {
        }

        protected List<SchedulerTask> ReadyTasks { get; } = new List<SchedulerTask>();

        private List<SchedulerTask> _doneTasks { get; } = new List<SchedulerTask>();

        public abstract void Process(int currentTime);

        public void AddNewTasks(IEnumerable<TaskModel> tasks)
        {
            var schedulerTasks = tasks.Select(GetSchedulerTask).ToList();
            ReadyTasks.AddRange(schedulerTasks);
        }

        public bool AllCurrentTasksAreDone => ReadyTasks.Count == 0;

        public ProcessingOutput GetProcessingOutput()
        {
            var delayedTasks = (decimal)_doneTasks.Count(t => t.IsDeyaled);

            return new ProcessingOutput
            {
                PercentOfDelayedTasks = _doneTasks.Count == 0 ? 0 : delayedTasks / _doneTasks.Count * 100
            };
        }

        protected void UpdateLists(SchedulerTask task)
        {
            _doneTasks.Add(task);
            ReadyTasks.Remove(task);
        }

        private SchedulerTask GetSchedulerTask(TaskModel task)
        {
            return new SchedulerTask
            {
                CreateTime = task.CreateTime,
                Duration = task.Duration,
                MaxWaitingTime = task.MaxWaitingTime
            };
        }
    }
}
