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
        private readonly List<SchedulerTask> _secondTierTasks = new List<SchedulerTask>();

        private bool _currentTaskIsSecondTier;

        public override bool AllCurrentTasksAreDone => base.AllCurrentTasksAreDone && _secondTierTasks.Count == 0;

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
                    else
                    {
                        //task taken from _secondTier
                        _currentTaskIsSecondTier = true;
                    }
                }
            }

            while (!CheckIfCurrentTaskIsBest(currentTime))
            {
            }

            ProcessCurrentTask();
        }

        private bool CheckIfCurrentTaskIsBest(int currentTime)
        {
            if (_currentTaskIsSecondTier && ReadyTasks.Any())
            {
                //we have better task in ReadyTasks
                TrySetCurrentTaskFromReadyTasks(); //replaces CurrentTask with task from ReadyTasks, "old" CurrentTask from _secondTierList in still first on list
                _currentTaskIsSecondTier = false;
            }

            CurrentTask.WaitingTime = GetTaskWaitingTime(currentTime, CurrentTask);

            if (CurrentTask.IsDelayed && ReadyTasks.Any())
            {
                _secondTierTasks.Add(CurrentTask); //already delayed, we take next task
                TrySetCurrentTaskFromReadyTasks();
                return false; //we have to check task, that just have been assigned as CurrentTask
            }

            return true;
            //task is not delayed, or delayed, but we don't have any more tasks
            //TODO: priority: we shoud check if secondTierTasks contains better task
        }
        
        private void ProcessCurrentTask()
        {
            CurrentTask.ProcessedTime++; //TODO: merge with Timer.Tick() to keep consistency

            if (CurrentTask.IsDone)
            {
                if (_currentTaskIsSecondTier)
                {
                    _currentTaskIsSecondTier = false;
                    _secondTierTasks.Remove(CurrentTask);
                }
                
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
