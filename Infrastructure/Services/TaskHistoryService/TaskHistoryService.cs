using Dapper;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services.TaskHistoryService;
using Npgsql;
namespace Infrastructure.Services.TaskHistoryService;

public class TaskHistoryService : ITaskHistoryService
{
    public async Task<bool> Create(TaskHistory taskHistory)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreate, taskHistory) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(TaskHistory taskHistory)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, taskHistory) > 0;
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

                return await connection.ExecuteAsync(SqlCommand.SqlDeleteById, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<TaskHistory?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<TaskHistory?>(SqlCommand.SqlGetById, new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TaskHistory?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskHistory?>(SqlCommand.SqlGetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    // Required 6
    public async Task<IEnumerable<GettingTaskChangeHistoryById?>> GettingTaskChangeHistoryById(Guid taskid)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GettingTaskChangeHistoryById>(
                    SqlCommand.GettingTaskChangeHistoryById);
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
    public const string SqlCreate = "insert into taskhistory(id, taskid, changedescription, changeat) values(@id, @taskid, @changedescription, @changeat)";
    public const string SqlUpdate = "update taskhistory set taskid = @taskid, changedescription = @changedescription, changeat = @changeat where id = @id";
    public const string SqlDeleteById = "delete from taskhistory where id = @id";
    public const string SqlGetById = "select * from taskhistory where id = @id";
    public const string SqlGetAll = "select * from taskhistory";
    
    // Request 6
    public const string GettingTaskChangeHistoryById = @"select id, taskid, changedescription, changeat
                                                            from taskhistory
                                                            where taskid = @taskid
                                                            order by changeat desc
                                                                                    ";
}