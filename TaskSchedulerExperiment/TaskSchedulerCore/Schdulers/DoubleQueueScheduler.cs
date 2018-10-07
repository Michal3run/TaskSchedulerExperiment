using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Schdulers
{
    public class DoubleQueueScheduler : SchedulerBase
    {
        /// <summary>
        /// Tasks that are already "lost", we prefer newest tasks
        /// </summary>
        private readonly Queue<SchedulerTask> _secondTierTasks = new Queue<SchedulerTask>();

        public override void Process(int currentTime)
        {
            if (CurrentTask == null)
            {
                if (!TrySetCurrentTaskFromReadyTasks())
                {
                    //no current tasks to process, we get seconeTier
                    if (!TrySetCurrentTask(_secondTierTasks))
                    {
                        return; 
                        //no tasks to process..
                    }
                }
            }

            while (!CheckIfCurrentTaskIsBest())
            {
            }

            ProcessCurrentTask(currentTime);
        }

        private bool CheckIfCurrentTaskIsBest()
        {
            if (CurrentTask.IsDeyaled && ReadyTasks.Count > 0)
            {
                _secondTierTasks.Enqueue(CurrentTask); //already delayed, we take next task
                TrySetCurrentTaskFromReadyTasks();
                return false; //we have to check task, that just have been assigned as CurrentTask
            }

            return true;
            //task is not delayed, or delayed, but we don't have any more tasks
            //TODO: priority: we shoud check if secondTierTasks contains better task
        }
        
        private void ProcessCurrentTask(int currentTime)
        {
            CurrentTask.WaitingTime = GetTaskWaitingTime(currentTime, CurrentTask);
            CurrentTask.ProcessedTime++; //TODO: merge with Timer.Tick() to keep consistency

            if (CurrentTask.IsDone)
            {
                AddCurrentTaskToDone();
            }
        }
        
        private int GetTaskWaitingTime(int currentTime, SchedulerTask task)
        {
            var timeFromCreate = currentTime - task.CreateTime;
            var waitingTime = timeFromCreate - task.ProcessedTime; //time when task is processed is not counted as waitingTime
            return waitingTime;
        }
    }
}
