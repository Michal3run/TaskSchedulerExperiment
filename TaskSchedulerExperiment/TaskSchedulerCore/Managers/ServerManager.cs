using System;
using System.Collections.Generic;
using TaskSchedulerCommon;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;
using TaskSchedulerData.Reading;

namespace TaskSchedulerCore.Managers
{
    public class ServerManager : IDisposable
    {
        private readonly IQueueManager _queueManager;
        private readonly ITaskScheduler _taskScheduler;
        private readonly Timer _timer;

        public ServerManager(ServerParameters parameters)
        {
            _taskScheduler = GetTaskScheduler(parameters.SchedulerType);
            _timer = GetTimer();
            _queueManager = GetQueueManager();
        }

        public ProcessingOutput GetProcessingOutput()
        {
            RunTasks();
            return _taskScheduler.GetProcessingOutput();
        }

        private void RunTasks()
        {
            while (TasksInQueueOrBeginProcessed)
            {
                var currentTasks = _queueManager.GetTasksToProcess(_timer.CurrentTime);
                _taskScheduler.AddNewTasks(currentTasks);
                _taskScheduler.Process(_timer.CurrentTime);
                _timer.Tick();
            }
        }

        private bool TasksInQueueOrBeginProcessed => !_queueManager.NoTasksToProcess || !_taskScheduler.AllCurrentTasksAreDone;

        private ITaskScheduler GetTaskScheduler(ESchedulerType type) => SchedulerManager.GetTaskScheduler(type);

        private Timer GetTimer() => new Timer();

        private IQueueManager GetQueueManager()
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
