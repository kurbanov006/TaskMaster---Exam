namespace Infrastructure.Services.TaskAttachmentsService;
using Infrastructure.Models;
public interface ITaskattAchmentsService
{
    Task<bool> Create(TaskAttachments attachments);
    Task<bool> Update(TaskAttachments attachments);
    Task<bool> Delete(Guid id);
    Task<TaskAttachments?> GetById(Guid id);
    Task<IEnumerable<TaskAttachments?>> GetAll();
}