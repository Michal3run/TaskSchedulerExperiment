using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Schdulers
{
    public abstract class SchedulerBase : ITaskScheduler
    {
        private List<SchedulerTask> _readyTasks { get; } = new List<SchedulerTask>();

        private List<SchedulerTask> _doneTasks { get; } = new List<SchedulerTask>();

        protected SchedulerBase()
        {
        }        

        public abstract void Process(int currentTime);

        public void AddNewTasks(IEnumerable<TaskModel> tasks)
        {
            var schedulerTasks = tasks.Select(GetSchedulerTask).ToList();
            _readyTasks.AddRange(schedulerTasks);
        }

        public bool AllCurrentTasksAreDone => _readyTasks.Count == 0;

        public ProcessingOutput GetProcessingOutput()
        {
            var delayedTasks = (decimal)_doneTasks.Count(t => t.IsDeyaled);

            return new ProcessingOutput
            {
                PercentOfDelayedTasks = _doneTasks.Count == 0 ? 0 : delayedTasks / _doneTasks.Count * 100
            };
        }

        protected bool TryGetReadyTask(out SchedulerTask task)
        {
            task = _readyTasks.FirstOrDefault();
            return task != null;
        }

        protected void UpdateLists(SchedulerTask task)
        {
            _doneTasks.Add(task);
            _readyTasks.Remove(task);
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
