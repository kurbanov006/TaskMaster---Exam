using Dapper;
using Npgsql;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services.Categories;

namespace Infrastructure.Services.Categories;

public class CategoriService : ICategoriService
{
    public async Task<bool> Create(Models.Categories categories)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreate, categories) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Models.Categories categories)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, categories) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlDelete, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Models.Categories?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<Models.Categories>(SqlCommand.SqlGetById,
                    new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Models.Categories?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Models.Categories>(SqlCommand.SqlGetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    // Request 2
    public async Task<IEnumerable<GettingCategoriesWithNumberOfTasks?>> GettingCategoriesWithNumberOfTasks()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GettingCategoriesWithNumberOfTasks>(SqlCommand.GettingCategoriesWithNumberOfTasks);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}



file class SqlCommand
{
    public const string SqlConnection = "Server=localhost; Port=5432; Database=taskmanagement_db; User Id=postgres; Password=12345";
    public const string SqlCreate = "insert into categories(id, name, createdat) values (@id, @name, @createdat);";
    public const string SqlUpdate = "update categories set name=@name, createdat=@createdat where id = @id";
    public const string SqlDelete = "delete from categories where id = @id";
    public const string SqlGetById = "select * from categories where id = @id";
    public const string SqlGetAll = "select * from categories";
    // Request 2
    public const string GettingCategoriesWithNumberOfTasks = @"select c.id as categoryid, c.name, c.createdat,
                                                        count(t.id) as counttask
                                                        from categories c 
                                                        join tasks t on c.id = t.categoryid
                                                        group by c.id, c.name";
}