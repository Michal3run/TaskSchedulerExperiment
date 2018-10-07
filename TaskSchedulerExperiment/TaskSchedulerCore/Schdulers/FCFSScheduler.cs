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
            CurrentTask.WaitingTime = CurrentTask.WaitingTime ?? currentTime - CurrentTask.CreateTime; //simple case, without expropriation
            CurrentTask.ProcessedTime++; //TODO: merge with Timer.Tick() to keep consistency

            if (CurrentTask.IsDone)
            {
                AddCurrentTaskToDone();
            }
        }
    }
}
