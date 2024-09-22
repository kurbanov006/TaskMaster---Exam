using Infrastructure.Models.Requests;

namespace Infrastructure.Services.Categories;
using Infrastructure.Models;


public interface ICategoriService
{
    Task<bool> Create(Categories categories);
    Task<bool> Update(Categories categories);
    Task<bool> Delete(Guid id);
    Task<Categories?> GetById(Guid id);
    Task<IEnumerable<Categories?>> GetAll();
    Task<IEnumerable<GettingCategoriesWithNumberOfTasks?>> GettingCategoriesWithNumberOfTasks();
}