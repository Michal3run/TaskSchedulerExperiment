using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Schdulers
{
    public class FCFSScheduler : SchedulerBase
    {
        public override void Process(int currentTime)
        {
            if (CurrentTask == null)
            {
                if (!TrySetCurrentTaskFromReadyTasks())
                {
                    //no tasks to process..
                    return;
                }
            }

            ProcessCurrentTask(currentTime);
        }

        private void ProcessCurrentTask(int currentTime)
        {
            CurrentTask.ProcessedTime++;

            if (CurrentTask.IsDone)
            {
                CurrentTask.WaitingTime = GetTaskWaitingTime(currentTime, CurrentTask); 
                AddCurrentTaskToDone();
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
