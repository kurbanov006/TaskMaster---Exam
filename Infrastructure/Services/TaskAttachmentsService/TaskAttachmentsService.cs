using Dapper;
using Infrastructure.Models;
using Npgsql;
namespace Infrastructure.Services.TaskAttachmentsService;

public class TaskAttachmentsService : ITaskattAchmentsService
{
    public async Task<bool> Create(TaskAttachments attachments)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreate, attachments) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(TaskAttachments attachments)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, attachments) > 0;
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

                return await connection.ExecuteAsync(SqlCommand.SqlDeleteById, new {Id = id}) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<TaskAttachments?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<TaskAttachments>(SqlCommand.SqlGetById,
                    new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TaskAttachments?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskAttachments>(SqlCommand.SqlGetAll);
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
    public const string SqlCreate = "insert into taskattachments(id, taskid, filepath, createdat) values(@id, @taskid, @filepath, @createdat);";
    public const string SqlUpdate = "update taskattachments set taskid = @taskid, filepath = @filepath, createdat = @createdat where id = @id";
    public const string SqlDeleteById = "delete from taskattachments where id = @id";
    public const string SqlGetById = "select * from taskattachments where id = @id";
    public const string SqlGetAll = "select * from taskattachments";
}

