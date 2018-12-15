namespace TaskSchedulerCommon.Models
{
    public class InputParamters
    {
        public string OriginalFileName { get; set; }
        public decimal Load { get; set; }
        public int TaskLength { get; set; }
        public int Delay { get; set; }
        public int Cov { get; set; }
        public ESchedulerType SchedulerType { get; set; }
    }
}
