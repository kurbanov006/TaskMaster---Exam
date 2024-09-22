using Infrastructure.Models.Requests;

namespace Infrastructure.Services.TaskService;
using Infrastructure.Models;


public interface ITaskService
{
    Task<bool> Create(Task task);
    Task<bool> Update(Task task);
    Task<bool> Delete(Guid id);
    Task<Task?> GetById(Guid id);
    Task<IEnumerable<Task>> GetAll();
    Task<IEnumerable<GettingTasksByPriority?>> GetTasksByPriority(int n);
    Task<IEnumerable<GettingTasksWithUserAndCategory?>> GetTasksWithUserAndCategory();
    Task<IEnumerable<GetTasksSortedByDueDate?>> GetTasksSortedByDueDate();
    Task<IEnumerable<GettingTasksFilteredByCompletionDate?>> GettingTasksFilteredByCompletionDate();

    Task<IEnumerable<RetrievingTasksBasedOnCompletionStatusAndPriority?>>
        RetrievingTasksBasedOnCompletionStatusAndPriority(bool isCompleted, int priority);
}