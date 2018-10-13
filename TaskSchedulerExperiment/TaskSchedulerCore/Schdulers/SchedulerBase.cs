using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;
using TaskSchedulerData.Helpers;

namespace TaskSchedulerCore.Schdulers
{
    public abstract class SchedulerBase : ITaskScheduler
    {
        private readonly List<SchedulerTask> _doneTasks = new List<SchedulerTask>();

        protected Queue<SchedulerTask> ReadyTasks { get; } = new Queue<SchedulerTask>();

        protected SchedulerTask CurrentTask { get; private set; }

        public abstract void Process(int currentTime);

        public void AddNewTasks(IEnumerable<TaskModel> tasks)
        {
            var schedulerTasks = tasks.Select(GetSchedulerTask).ToList();
            ReadyTasks.AddRange(schedulerTasks);
        }

        public virtual bool AllCurrentTasksAreDone => ReadyTasks.Count == 0;

        public ProcessingOutput GetProcessingOutput()
        {
            var delayedTasks = (decimal)_doneTasks.Count(t => t.IsDelayed);

            return new ProcessingOutput
            {
                PercentOfDelayedTasks = _doneTasks.Count == 0 ? 0 : delayedTasks / _doneTasks.Count * 100
            };
        }

        protected bool TrySetCurrentTaskFromReadyTasks() => TrySetCurrentTask(ReadyTasks);

        protected bool TrySetCurrentTask(Queue<SchedulerTask> source)
        {
            if (source.Count == 0) return false;

            CurrentTask = source.Dequeue();
            return true;
        }

        protected bool TrySetCurrentTask(List<SchedulerTask> source)
        {
            CurrentTask = source.FirstOrDefault();
            return CurrentTask != null;
        }

        protected void AddCurrentTaskToDone()
        {
            _doneTasks.Add(CurrentTask);
            CurrentTask = null;
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
