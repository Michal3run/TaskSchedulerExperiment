using System;
using System.Collections.Generic;
using TaskSchedulerCommon.Interfaces;
using TaskSchedulerCommon.Models;
using TaskSchedulerData.Reading;

namespace TaskSchedulerCore.Managers
{
    public class ServerManager : IDisposable
    {
        private readonly IQueueManager _queueManager;
        private readonly ITaskScheduler _taskScheduler;
        private readonly Timer _timer;  //beter debugging that with ITimer..  

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
            while (TasksInQueueOrBeginProcessed)
            {
                var currentTasks = _queueManager.GetTasksToProcess(_timer.CurrentTime);
                _taskScheduler.AddNewTasks(currentTasks);
                _taskScheduler.Process(_timer.CurrentTime);
                _timer.Tick();
            }
        }

        private bool TasksInQueueOrBeginProcessed => !_queueManager.NoTasksToProcess || !_taskScheduler.AllCurrentTasksAreDone;

        private Timer GetTimer(int totalWorkingTime) => new Timer(totalWorkingTime);

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
