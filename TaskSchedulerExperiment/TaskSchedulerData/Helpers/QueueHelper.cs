using System.Collections.Generic;

namespace TaskSchedulerData.Helpers
{
    public static class QueueHelper
    {
        public static void AddRange<T>(this Queue<T> queue, IEnumerable<T> enu)
        {
            foreach (var obj in enu)
                queue.Enqueue(obj);
        }
    }
}
