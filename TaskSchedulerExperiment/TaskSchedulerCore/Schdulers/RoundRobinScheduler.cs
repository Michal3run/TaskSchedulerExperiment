using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Models;
using TaskSchedulerData.Helpers;

namespace TaskSchedulerCore.Schdulers
{
    public class RoundRobinScheduler : SchedulerBase
    {
        private readonly List<SchedulerTask> _tasksNoFinishedInCurrentIteration = new List<SchedulerTask>();

        public override void Process(int currentTime)
        {
            if (ReadyTasks.Count == 0) return;
            
            var processingTime = 1 / ReadyTasks.Count;

            while (ReadyTasks.Any())
            {
                TrySetCurrentTaskFromReadyTasks();
                ProcessCurrentTask(processingTime);
            }

            //ReadyTasks queue is empty, we add tasks that are not finished in current iteration, for next iterations
            ReadyTasks.AddRange(_tasksNoFinishedInCurrentIteration);
            _tasksNoFinishedInCurrentIteration.Clear();
        }

        private void ProcessCurrentTask(float processingTime)
        {
            CurrentTask.ProcessedTime += processingTime;

            if (CurrentTask.IsDone)
            {
                AddCurrentTaskToDone();
            }
            else
            {
                _tasksNoFinishedInCurrentIteration.Add(CurrentTask);
            }
        }
    }
}
