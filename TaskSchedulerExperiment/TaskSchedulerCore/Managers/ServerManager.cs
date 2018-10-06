using System;
using System.Collections.Generic;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;
using TaskSchedulerData.Reading;

namespace TaskSchedulerCore.Managers
{
    public class ServerManager : IDisposable
    {
        private readonly ITaskScheduler _taskScheduler;
        private readonly ITimer _timer;
        private readonly QueueManager _queueManager;

        public ServerManager(ServerParameters parameters)
        {
            _taskScheduler = parameters.TaskScheduler;
            _timer = GetTimer(parameters.TotalWorkingTime);
            _queueManager = GetQueueManager();
        }

        public ProcessingOutput GetProcessingOutput()
        {
            RunTasks();
            return _taskScheduler.GetProcessingOutput();
        }

        private void RunTasks()
        {
            while (_timer.IsActive || !_taskScheduler.AllCurrentTasksAreDone) //we have to wait for all tasks to be finished (different solution? change IsActive ?)
            {
                var currentTasks = _queueManager.GetTasksToProcess(_timer.CurrentTime);
                _taskScheduler.AddNewTasks(currentTasks);
                _taskScheduler.Process(_timer.CurrentTime);
                _timer.Tick();
            }
        }

        private ITimer GetTimer(int totalWorkingTime) => new Timer(totalWorkingTime);

        private QueueManager GetQueueManager()
        {
            var tasks = GetTasks();
            return new QueueManager(tasks);
        }

        private IEnumerable<TaskModel> GetTasks()
        {
            var reader = new TaskReader();
            return reader.ReadAllTasks();
        }

        public void Dispose()
        {
        }
    }
}
