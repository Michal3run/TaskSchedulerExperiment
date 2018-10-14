using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerGenerator.TaskIO
{
    interface ISaver
    {
        void Save(IEnumerable<TaskModel> tasks);
    }
}
