using Infrastructure.Models.Requests;

namespace Infrastructure.Services.UserService;
using Users;
public interface IUserService
{
    Task<bool> Create(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(Guid id);
    Task<User?> GetById(Guid id);
    Task<IEnumerable<User?>> GetAll();
    Task<IEnumerable<GettingUsersWithTasks?>> GettingUsersWithTasks();
}