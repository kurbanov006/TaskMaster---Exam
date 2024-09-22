using Infrastructure.Models;
using Infrastructure.Models.Requests;

namespace Infrastructure.Services.TaskHistoryService;

public interface ITaskHistoryService
{
    Task<bool> Create(TaskHistory taskHistory);
    Task<bool> Update(TaskHistory taskHistory);
    Task<bool> Delete(Guid id);
    Task<TaskHistory?> GetById(Guid id);
    Task<IEnumerable<TaskHistory?>> GetAll();
    Task<IEnumerable<GettingTaskChangeHistoryById?>> GettingTaskChangeHistoryById(Guid taskid);
}