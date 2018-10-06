using TaskSchedulerCommon.Interfaces;

namespace TaskSchedulerCommon.Models
{
    public class Timer : ITimer
    {
        private int _ticksCount = 0;
        private int _totalWorkingTime;

        public Timer(int totalWorkingTime)
        {
            _totalWorkingTime = totalWorkingTime;
        }

        public int CurrentTime => _ticksCount;

        public bool IsActive => _ticksCount <= _totalWorkingTime;

        public void Tick()
        {
            _ticksCount++;
        }
    }
}
