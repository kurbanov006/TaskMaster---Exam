using Dapper;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Npgsql;
namespace Infrastructure.Services.ComentsService;

public class CommentService : IComentService
{
    public async Task<bool> Create(Comment comment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlCreate, comment) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Comment comment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, comment) > 0;
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

    public async Task<Comment?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryFirstOrDefaultAsync<Comment?>(SqlCommand.SqlGetById, new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Comment?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<Comment?>(SqlCommand.SqlGetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    
    // Request 7
    public async Task<IEnumerable<GettingCommentsOnATaskFilteredByUser?>> GettingCommentsOnATaskFilteredByUser(Guid taskId, Guid userId)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GettingCommentsOnATaskFilteredByUser?>(SqlCommand.GettingCommentsOnATaskFilteredByUser, new {TaskId = taskId, UserId = userId});
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
    public const string SqlCreate = "insert into comments(id, taskid, userid, content, createdat) values(@id, @taskid, @userid, @content, @createdat)";
    public const string SqlUpdate = "update comments set taskid = @taskid, userid = @userid, content=@content, createdat=@createdat where id = @id";
    public const string SqlDeleteById = "delete from comments where id=@id";
    public const string SqlGetById = "select * from comments where id=@id";
    public const string SqlGetAll = "select * from comments";

    public const string GettingCommentsOnATaskFilteredByUser = @"select id, taskid, userid, content, createdat
                                                            from comments
                                                            where taskid = @taskid and userid = @userid
                                                            order by createdat";
}