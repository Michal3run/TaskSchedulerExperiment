﻿using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCommon.Models;
using static TaskSchedulerData.Helpers.RandomHelper;

namespace TaskSchedulerData.Reading
{
    public class TaskReader
    {
        public IEnumerable<TaskModel> ReadAllTasks()
        {
            return GetMockTasks();
        }

        /// <summary>
        /// Mock implementation, replace with reading from file
        /// </summary>
        private IEnumerable<TaskModel> GetMockTasks() => Enumerable.Range(0, 100000).Select(GetTaskModel).ToList();

        private TaskModel GetTaskModel(int i)
        {
            return new TaskModel
            {
                CreateTime = GetRandomInt(0, 500000), 
                Duration = GetRandomInt(1, 8)
            };
        }
    }
}
