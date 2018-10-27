using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerGenerator.TaskIO
{
    class CsvSaver : ISaver
    {
        IConfiguration Configuration;

        public CsvSaver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Save(IEnumerable<TaskModel> tasks)
        {
            using (var writer = new StreamWriter(Configuration.OutputPath))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(tasks);
            }
        }
    }
}
