using System;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCore.Schdulers;

namespace TaskSchedulerCore.Managers
{
    public class SchedulerManager
    {
        public static ITaskScheduler GetTaskScheduler(ESchedulerType type)
        {
            switch (type)
            {
                case ESchedulerType.FCFS: return new FCFSScheduler();
                case ESchedulerType.RoundRobin: return new RoundRobinScheduler();
                case ESchedulerType.DoubleQueue: return new DoubleQueueScheduler();
                default: throw new Exception($"Unknown schedulerType: {type}");
            }
        }
    }
}
