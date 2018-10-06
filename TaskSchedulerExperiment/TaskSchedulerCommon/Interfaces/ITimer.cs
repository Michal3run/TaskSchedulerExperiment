namespace TaskSchedulerCommon.Interfaces
{
    public interface ITimer
    {
        int CurrentTime { get; }        
        bool IsActive { get; }
        void Tick();
    }
}
