using System.Collections.Generic;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerCommon.Interfaces
{
    public interface IReader
    {
        IEnumerable<TaskModel> Read();
    }
}
