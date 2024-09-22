using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services.Categories;
using Infrastructure.Services.TaskService;
using Infrastructure.Services.UserService;
using Users;
using Task = Infrastructure.Models.Task;
using Infrastructure.Services.ComentsService;
using Infrastructure.Services.TaskAttachmentsService;
using Infrastructure.Services.TaskHistoryService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region Users
UserService userService = new UserService();
app.MapPost("/users", async (User user) =>
{
    bool res = await userService.Create(user);
    if (res == false)
    {
        return Results.NotFound(new {message = "User not Created!"});
    }

    return Results.Ok(new {message = "User Created!"});
});

app.MapPut("/users", async (User user) =>
{
    bool res = await userService.Update(user);
    if (res == false)
    {
        return Results.NotFound(new { message = "User not Updated!" });
    }
    return Results.Ok(new { message = "User Updated!" });
});

app.MapDelete("/users/{id}", async (Guid id) =>
{
    bool res = await userService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new { message = "User not Deleted!" });
    }
    return Results.Ok(new { message = "User Deleted!" });
});

app.MapGet("/users/{id}", async (Guid id) =>
{
    User? user = await userService.GetById(id);
    if (user is null)
    {
        return Results.NotFound(new { message = "User not!" });
    }
    return Results.Ok(user);
});

app.MapGet("/users", async () =>
{
    IEnumerable<User?> users = await userService.GetAll();
    if (users == null!)
    {
        return Results.NotFound( new { message = "Users not!" });
    }
    return Results.Ok(users);
});

// Request 1
app.MapGet("/users/gettinguserswithtasks", async () =>
{
    IEnumerable<GettingUsersWithTasks?> users = await userService.GettingUsersWithTasks();
    if (users == null!)
    {
        return Results.NotFound(new { message = "Users not found!" });
    }
    return Results.Ok(users);
});

#endregion

#region Categori

CategoriService categoriService = new CategoriService();
app.MapPost("/categories", async (Categories categories) =>
{
    bool res = await categoriService.Create(categories);
    if (res == false)
    {
        return Results.NotFound(new { message = "Category not created!" });
    }
    return Results.Ok(new {message = "Create category!"});
});

app.MapPut("/categories", async (Categories categories) =>
{
    bool res = await categoriService.Update(categories);
    if (res == false)
    {
        return Results.NotFound(new { message = "Category not updated!" });
    }

    return Results.Ok(new { message = "Category updated!" });
});

app.MapDelete("/categories/{id}", async (Guid id) =>
{
    bool res = await categoriService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new { message = "Categories not deleted" });
    }

    return Results.Ok(new { message = "Categories deleted!" });
});

app.MapGet("/categories/{id}", async (Guid id) =>
{
    Categories? categories = await categoriService.GetById(id);
    if (categories == null)
    {
        return Results.NotFound(new { message = "Category not" });
    }

    return Results.Ok(categories);
});

app.MapGet("/categories", async () =>
{
    IEnumerable<Categories?> categories = await categoriService.GetAll();
    if (categories == null!)
    {
        return Results.NotFound(new { message = "Category not"});
    }

    return Results.Ok(categories);
});

// Requery 2
app.MapGet("/categories/GettingCategoriesWithNumberOfTasks", async () =>
{
    IEnumerable<GettingCategoriesWithNumberOfTasks?> categories =
        await categoriService.GettingCategoriesWithNumberOfTasks();
    if (categories == null!)
    {
        return Results.NotFound(new { message = "Не удалось получить!" });
    }
    return Results.Ok(categories);
});
#endregion

#region Task
TaskService taskService = new TaskService();
app.MapPost("/tasks", async (Task task) =>
{
    bool res = await taskService.Create(task);
    if (res == false)
    {
        return Results.NotFound(new {message="Не получилось добавить!"});
    }
    return Results.Ok(new {message="Задача добавлена!"});
});

app.MapPut("/tasks", async (Task task) =>
{
    bool res = await taskService.Update(task);
    if (res == false)
    {
        return Results.NotFound(new {message="Не получилось обновить!"}); 
    }
    return Results.Ok(new {message="Задача обновлена!"});
});

app.MapDelete("/tasks/{id}", async (Guid id) =>
{
    bool res = await taskService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new {message="Задача не удалена"});  
    }
    return Results.Ok(new {message="Задача удалена!"});
});

app.MapGet("/tasks/{id}", async (Guid id) =>
{
    Task? task = await taskService.GetById(id);
    if (task == null)
    {
        return Results.NotFound(new {message="Задача не найдена"});
    }
    return Results.Ok(task);
});

app.MapGet("/tasks", async () =>
{
    IEnumerable<Task?> tasks = await taskService.GetAll();
    if (tasks == null!)
    {
        return Results.NotFound(new {message="Задачи не найдены!"});
    }
    return Results.Ok(tasks);
});

// Requery 3
app.MapGet("/tasks/GettingTasksByPriority", async (int n) =>
{
    IEnumerable<GettingTasksByPriority?> tasks = await taskService.GetTasksByPriority(n);
    if (tasks == null!)
    {
        return Results.NotFound(new {message="Задачи не найдены по приоритету"});
    }
    return Results.Ok(tasks);
});
// Requery 4
app.MapGet("/tasks/GettingTasksWithUserAndCategory", async () =>
{
    IEnumerable<GettingTasksWithUserAndCategory?> task = await taskService.GetTasksWithUserAndCategory();
    if (task == null!)
    {
        return Results.NotFound(new {message="Задачи не найдены по пользователю и категории"});
    }   
    return Results.Ok(task);
});

// Requery 5
app.MapGet("/tasks/GetTasksSortedByDueDate", async () =>
{
    IEnumerable<GetTasksSortedByDueDate?> task = await taskService.GetTasksSortedByDueDate();
    if (task == null!)
    {
        return Results.NotFound(new {message="Не удалось получить задачи!"});
    }
    return Results.Ok(task);
});

// Requery 9
app.MapGet("/tasks/GettingTasksFilteredByCompletionDate", async () =>
{
    IEnumerable<GettingTasksFilteredByCompletionDate?> task = await taskService.GettingTasksFilteredByCompletionDate();
    if (task == null!)
    {
        return Results.NotFound(new {message="Не удалось получить задачи"});
    }
    return Results.Ok(task);
});

// Requery 10
app.MapGet("/tasks/RetrievingTasksBasedOnCompletionStatusAndPriority", async (bool isCompleted, int priority) =>
{
    IEnumerable<RetrievingTasksBasedOnCompletionStatusAndPriority?> task = await taskService.RetrievingTasksBasedOnCompletionStatusAndPriority(isCompleted, priority);
    if (task == null!)
    {
        return Results.NotFound(new {message="Не удалось получить задачи"});
    }
    return Results.Ok(task);
});
#endregion

#region Comment
CommentService commentService = new CommentService();
app.MapPost("/comments", async (Comment comment) =>
{
    bool res = await commentService.Create(comment);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось добавить"});
    }
    return Results.Ok(new {message="Комментарий добавлен"});
});

app.MapPut("comments", async (Comment comment) =>
{
    bool res = await commentService.Update(comment);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось обновить коментарий"});
    }
    return Results.Ok(new {message="Комментарий обновлён"});
});

app.MapDelete("/comments/{id}", async (Guid id) =>
{
    bool res = await commentService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new {message="Комментарий не удалён"});
    }
    return Results.Ok(new {message="Комментарий удалён"});
});

app.MapGet("/comments/{id}", async (Guid id) =>
{
    Comment? comment = await commentService.GetById(id);
    if (comment == null)
    {
        return Results.NotFound(new {message="Комментарий не найден"});
    }
    return Results.Ok(comment);
});

app.MapGet("/comments", async () =>
{
    IEnumerable<Comment?> comments = await commentService.GetAll();
    if (comments == null!)
    {
        return Results.NotFound(new {message="Комментарии не найдены"});
    }
    return Results.Ok(comments);
});
#endregion

#region TaskAttachments
TaskAttachmentsService taskAttachmentsService = new TaskAttachmentsService();
app.MapPost("/attachments", async (TaskAttachments taskAttachments) =>
{
    bool res = await taskAttachmentsService.Create(taskAttachments);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось добавить!"});
    }

    return Results.Ok(new {message="Успешно добавлено!"});
});

app.MapPut("/attachments", async (TaskAttachments taskAttachment) =>
{
    bool res = await taskAttachmentsService.Update(taskAttachment);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось обновить!"});
    }

    return Results.Ok(new {message="Обновлено успешно!"});
});

app.MapDelete("/attachments/{id}", async (Guid id) =>
{
    bool res = await taskAttachmentsService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось удалить!"});
    }

    return Results.Ok(new {message="Удалено успешно!"});
});

app.MapGet("/attachments/{id}", async (Guid id) =>
{
    TaskAttachments? taskAttachment = await taskAttachmentsService.GetById(id);
    if (taskAttachment == null)
    {
        return Results.NotFound(new {message="Файл не найден!"});
    }

    return Results.Ok(taskAttachment);
});

app.MapGet("/attachments", async () =>
{
    IEnumerable<TaskAttachments?> tasks = await taskAttachmentsService.GetAll();
    if (tasks == null!)
    {
        return Results.NotFound(new {message="Файлы не найдены!"});
    }
    return Results.Ok(tasks);
});
#endregion

#region TaskHistory
TaskHistoryService taskHistoryService = new TaskHistoryService();
app.MapPost("/taskHistory", async (TaskHistory taskHistory) =>
{
    bool res = await taskHistoryService.Create(taskHistory);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось добавить!"}); 
    }
    return Results.Ok(new {message="Успешно добавлена!"});
});

app.MapPut("/taskHistory", async (TaskHistory taskHistory) =>
{
    bool res = await taskHistoryService.Update(taskHistory);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось обновить!"}); 
    }
    return Results.Ok(new {message="Обновлена успешно!"});
});

app.MapDelete("/taskHistory/{id}", async (Guid id) =>
{
    bool res = await taskHistoryService.Delete(id);
    if (res == false)
    {
        return Results.NotFound(new {message="Не удалось удалить!"}); 
    }
    return Results.Ok(new {message="Удалено успешно!"});
});

app.MapGet("/taskHistory/{id}", async (Guid id)=>
{
    TaskHistory? taskHistory = await taskHistoryService.GetById(id);
    if (taskHistory == null)
    {
        return Results.NotFound(new {message="История не найдена!"});
    }
    return Results.Ok(taskHistory);
});

app.MapGet("/taskHistory", async () =>
{
    IEnumerable<TaskHistory?> taskHistories = await taskHistoryService.GetAll();
    if (taskHistories == null!)
    {
        return Results.NotFound(new {message="Истории не найдены!"});
    }
    return Results.Ok(taskHistories);
});

app.MapGet("/taskHistory/GettingTaskChangeHistoryById", async (Guid taskid) =>
{
    IEnumerable<GettingTaskChangeHistoryById?> taskHistory = await taskHistoryService.GettingTaskChangeHistoryById(taskid);
    if (taskHistory == null!)
    {
        return Results.NotFound(new {message="Не удалось получить"});
    }
    return Results.Ok(taskHistory);
});
#endregion















app.Run();
