using TaskSchedulerCommon.Interfaces;

namespace TaskSchedulerCommon.Models
{
    public class ServerParameters
    {
        /// <summary>
        /// Time for which server will work
        /// </summary>
        public int TotalWorkingTime { get; set; } //TO REFACTOR?
        
        public ITaskScheduler TaskScheduler { get; set; }
    }
}
