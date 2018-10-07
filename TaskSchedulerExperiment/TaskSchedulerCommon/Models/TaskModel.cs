using System.Diagnostics;

namespace TaskSchedulerCommon.Models
{
    [DebuggerDisplay("CreateTime: {" + nameof(CreateTime) + ("}, Duration: {" + nameof(Duration) + "}"))]
    public class TaskModel
    {
        /// <summary>
        /// Time when task was send to server
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// Time to process task
        /// </summary>
        public int Duration { get; set; }

        public int MaxWaitingTime { get; set; } = 5; //TODO later
    }
}
