namespace TaskSchedulerCommon.Models
{
    public class Timer
    {
        public int CurrentTime { get; private set; }

        public void Tick()
        {
            CurrentTime++;
        }
    }
}
