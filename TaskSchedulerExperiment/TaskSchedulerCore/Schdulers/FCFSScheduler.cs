using System.Linq;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCore.Schdulers
{
    public class FCFSScheduler : SchedulerBase
    {
        private SchedulerTask _currentTask;

        public override void Process(int currentTime)
        {
            if (_currentTask == null)
            {
                if (ReadyTasks.Any())
                {
                    _currentTask = ReadyTasks.First();
                }
                else
                {
                    //no tasks to process..
                    return;
                }
            }

            _currentTask.WaitingTime = currentTime - _currentTask.CreateTime; //simple case, without expropriation
            _currentTask.ProcessedTime++; //TODO: merge with Timer.Tick() to keep consistency
            if (_currentTask.IsDone)
            {
                UpdateLists(_currentTask);
                _currentTask = null;
            }            
        }
    }
}
