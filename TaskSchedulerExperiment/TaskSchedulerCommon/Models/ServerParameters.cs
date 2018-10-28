﻿using TaskSchedulerCommon.Interfaces;

namespace TaskSchedulerCommon.Models
{
    public class ServerParameters
    {
        public ESchedulerType SchedulerType { get; set; }
        public string TasksFilePath { get; set; }
    }
}
