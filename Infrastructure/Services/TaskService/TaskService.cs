using Dapper;
using Infrastructure.Models.Requests;
using Task = Infrastructure.Models.Task;
using Npgsql;
namespace Infrastructure.Services.TaskService;

public class TaskService : ITaskService
{
    public async Task<bool> Create(Task task)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreate, task) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Task task)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlUpdate, task) > 0;
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

    public async Task<Task?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<Task?>(SqlCommand.SqlGetById, new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Task>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Task>(SqlCommand.SqlGetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    // Requery 3
    public async Task<IEnumerable<GettingTasksByPriority>> GetTasksByPriority(int n)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GettingTasksByPriority>(SqlCommand.GettingTasksByPriority,
                    new { N = n });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GettingTasksWithUserAndCategory?>> GetTasksWithUserAndCategory()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GettingTasksWithUserAndCategory?>(SqlCommand.GettingTasksWithUserAndCategory);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GetTasksSortedByDueDate?>> GetTasksSortedByDueDate()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GetTasksSortedByDueDate>(SqlCommand.GetTasksSortedByDueDate);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    
    // Requery 9
    public async Task<IEnumerable<GettingTasksFilteredByCompletionDate?>> GettingTasksFilteredByCompletionDate()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GettingTasksFilteredByCompletionDate>(SqlCommand.GettingTasksFilteredByCompletionDate);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    // Requery 10
    public async Task<IEnumerable<RetrievingTasksBasedOnCompletionStatusAndPriority?>> RetrievingTasksBasedOnCompletionStatusAndPriority(bool isCompleted, int priority)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnection))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<RetrievingTasksBasedOnCompletionStatusAndPriority>(SqlCommand.RetrievingTasksBasedOnCompletionStatusAndPriority,
                    new { IsCompleted = isCompleted, Priority = priority });
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
    public const string SqlCreate = "insert into tasks(id, title, description, iscompleted, duedate, userid, categoryid, priority, createdat) values(@id, @title, @description, @iscompleted, @duedate, @userid, @categoryid, @priority, @createdat)";
    public const string SqlUpdate = "update tasks set title=@title, description=@description, iscompleted=@iscompleted, duedate=@duedate, userid=@userid, categoryid=@categoryid, priority=@priority, createdat=@createdat where id = @id";
    public const string SqlDeleteById = "delete from tasks where id=@id";
    public const string SqlGetById = "select * from tasks where id=@id";
    public const string SqlGetAll = "select * from tasks";
    // Requery 3
    public const string GettingTasksByPriority = @"select id, title, description, iscompleted, duedate, userid,
                               categoryid, priority, createdat
                                from tasks
                                where priority = @n";

    // Requery 4
    public const string GettingTasksWithUserAndCategory = @"select t.id as taskid, t.title, t.description,
                                                         u.id as userid, u.username,
                                                         c.id as categoryid,
                                                        c.name as categoryname
                                                        from tasks t
                                                        join users u on t.userid = u.id
                                                        join categories c on t.categoryid = c.id
                                                        order by t.createdat;";

    // Require 5
    public const string GetTasksSortedByDueDate = @"select id, title, description, iscompleted, 
                               duedate, userid, categoryid, priority, createdat
                                from tasks
                                order by duedate";
    
    // Require 9
    public const string GettingTasksFilteredByCompletionDate =
        @"select id, title, description, iscompleted, duedate, userid, categoryid, priority, createdat
            from tasks
            order by duedate";
    
    // Require 10
    public const string RetrievingTasksBasedOnCompletionStatusAndPriority = @"select id, title, description, iscompleted, duedate, userid, categoryid, priority, createdat
                                from tasks
                                where iscompleted = @iscompleted and priority = @priority
                                order by createdat";

}