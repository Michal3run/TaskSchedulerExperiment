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

            var processingTime = 1 / (float)ReadyTasks.Count;

            while (ReadyTasks.Any())
            {
                TrySetCurrentTaskFromReadyTasks();
                ProcessCurrentTask(currentTime, processingTime);
            }

            //ReadyTasks queue is empty, we add tasks that are not finished in current iteration, for next iterations
            ReadyTasks.AddRange(_tasksNoFinishedInCurrentIteration);
            _tasksNoFinishedInCurrentIteration.Clear();
        }

        private void ProcessCurrentTask(int currentTime, float processingTime)
        {
            CurrentTask.ProcessedTime += processingTime;

            if (CurrentTask.IsDone)
            {
                CurrentTask.WaitingTime = GetTaskWaitingTime(currentTime, CurrentTask);
                AddCurrentTaskToDone();
            }
            else
            {
                _tasksNoFinishedInCurrentIteration.Add(CurrentTask);
            }
        }

        private float GetTaskWaitingTime(int currentTime, SchedulerTask task)
        {
            var timeFromCreate = currentTime + 1 - task.CreateTime;
            var waitingTime = timeFromCreate - task.ProcessedTime; //time when task is processed is not counted as waitingTime
            return waitingTime;
        }
    }
}
