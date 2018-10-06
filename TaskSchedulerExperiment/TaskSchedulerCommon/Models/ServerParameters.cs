using TaskSchedulerCommon.Interfaces;

namespace TaskSchedulerCommon.Models
{
    public class ServerParameters
    {
        //Time for which server will work
        public int TotalWorkingTime { get; set; }
        
        public ITaskScheduler TaskScheduler { get; set; }
    }
}
