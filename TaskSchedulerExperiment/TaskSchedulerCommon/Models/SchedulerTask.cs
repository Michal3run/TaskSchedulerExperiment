using System.Diagnostics;

namespace TaskSchedulerCommon.Models
{
    [DebuggerDisplay("IsDone: {" + nameof(IsDone) + "} CreateTime: {" + nameof(CreateTime) + ("}, Duration: {" + nameof(Duration) + "}"))]
    public class SchedulerTask : TaskModel
    {
        public bool IsDeyaled => WaitingTime > MaxWaitingTime;

        public bool IsDone => ProcessedTime >= Duration;

        /// <summary>
        /// Processor time spend during task
        /// </summary>
        public int ProcessedTime { get; set; }

        public int? WaitingTime { get; set; }
    }
}
