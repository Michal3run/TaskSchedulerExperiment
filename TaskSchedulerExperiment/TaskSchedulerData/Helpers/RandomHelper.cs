using System;

namespace TaskSchedulerData.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random _random = new Random();
        private static readonly object _syncLock = new object();

        public static int GetRandomInt(int minValue, int maxValue)
        {
            lock (_syncLock)
            {
                return _random.Next(minValue, maxValue);
            }
        }

        public static decimal GetRandomDecimal(int minValue, int maxValue)
        {
            lock (_syncLock)
            {
                return (decimal)_random.NextDouble() * (maxValue - minValue) + minValue;
            }
        }
    }
}
