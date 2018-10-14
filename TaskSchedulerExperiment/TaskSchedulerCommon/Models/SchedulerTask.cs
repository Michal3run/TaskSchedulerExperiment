using System.Diagnostics;

namespace TaskSchedulerCommon.Models
{
    [DebuggerDisplay("IsDone: {" + nameof(IsDone) + "} CreateTime: {" + nameof(CreateTime) + ("}, Duration: {" + nameof(Duration) + "}"))]
    public class SchedulerTask : TaskModel
    {
        public bool IsDelayed => WaitingTime > MaxWaitingTime;

        public bool IsDone => ProcessedTime >= Duration;

        /// <summary>
        /// Processor time spend during task
        /// </summary>
        public float ProcessedTime { get; set; }

        public float? WaitingTime { get; set; }
    }
}
