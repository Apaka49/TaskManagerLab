using System;
using System.Collections.Generic;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskListDto> GetTasksForProject(Guid projectId);
        TaskDetailDto GetTaskDetails(Guid taskId);
    }
}