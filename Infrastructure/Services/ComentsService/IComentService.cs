using Infrastructure.Models;
using Infrastructure.Models.Requests;

namespace Infrastructure.Services.ComentsService;

public interface IComentService
{
    Task<bool> Create(Comment comment);
    Task<bool> Update(Comment comment);
    Task<bool> Delete(Guid id);
    Task<Comment?> GetById(Guid id);
    Task<IEnumerable<Comment?>> GetAll();
    Task<IEnumerable<GettingCommentsOnATaskFilteredByUser?>> GettingCommentsOnATaskFilteredByUser(Guid taskId, Guid userId);
}