using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;

namespace TaskSchedulerData.Reading
{
    public class CsvReader : IReader
    {
        private readonly string _path;

        public CsvReader(string path)
        {
            _path = path;
        }

        public IEnumerable<TaskModel> Read()
        {
            using (var reader = new StreamReader(_path))
            {
                var csv = new CsvHelper.CsvReader(reader);
                var result = csv.GetRecords<TaskModel>().ToList();
                return result;
            }
        }
    }
}
