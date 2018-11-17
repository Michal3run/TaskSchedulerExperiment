namespace TaskSchedulerCommon.Models
{
    public class ProcessingOutput
    {
        public InputParamters InputParamters { get; set; }
        
        public decimal PercentOfDelayedTasks { get; set; }

        public string GetOutputTextInfo()
        {
            return string.Join(" ; ",
                InputParamters.Load,
                InputParamters.TaskLength,
                InputParamters.Delay,
                InputParamters.SchedulerType.ToString(),
                PercentOfDelayedTasks,
                InputParamters.OriginalFileName);
        }
    }
}
