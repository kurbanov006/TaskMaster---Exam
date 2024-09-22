using Npgsql;
using Dapper;
using Infrastructure.Models.Requests;
using Users;
namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
    public async Task<bool> Create(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreate, user) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, user) > 0;
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

    public async Task<User?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<User>(SqlCommand.SqlGetById, new {Id = id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<User?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<User>(SqlCommand.SqlGetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    // Request 1
    public async Task<IEnumerable<GettingUsersWithTasks?>> GettingUsersWithTasks()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
            
                return await connection.QueryAsync<GettingUsersWithTasks>(SqlCommand.SqlGettingUsersWithTasks);
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
    public const string SqlCreate = "insert into users(id, username, email, passwordhash, createdat) values(@id, @username, @email, @passwordhash, @createdat)";
    public const string SqlUpdate = "update users set username=@username, email=@email, passwordhash=@passwordhash where id = @id";
    public const string SqlDeleteById = "delete from users where id=@id";
    public const string SqlGetById = "select * from users where id=@id";
    public const string SqlGetAll = "select * from users";
    // Request 1
    public const string SqlGettingUsersWithTasks = @"select u.id as userid, u.username, u.email, t.id as taskid, 
                                                   t.title, t.description, t.createdat
                                                    from users u
                                                    join tasks t on t.userid = u.id";
}





