using System.Net;
using Dapper;
using Npgsql;

public class TaskItemService(ApplicationDbContext dbContext) : ITaskItemService
{
    private readonly ApplicationDbContext context=dbContext;
    public async Task<Response<string>> AddAsync(TaskItem taskItem)
    {
        try
        {
             using var conn=context.Connection();
             var insert = @"insert into taskitems(title,description,isCompleted,dueDate,createdAt)
             values(@title,@description,@iscompleted,@duedate,createdat)";
             var res = await conn.ExecuteAsync(insert,new{title=taskItem.Title,description=taskItem.Description,iscompleted=taskItem.IsCompleted,
             duedate=taskItem.DueDate,createda=taskItem.CreatedAt});
             return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not add Task")
             : new Response<string>(HttpStatusCode.OK,"Task successfully added!");


        }
        catch (System.Exception ex)
        {
            
            Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
        
    }

    public async Task<Response<string>> DeleteAsync(int taskitemid)
    {
            try
            {
                 using var conn = context.Connection();
             var delete ="delete from taskitems where id=@Id";
             var res = await conn.ExecuteAsync(delete,new{Id=taskitemid});
             return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not delete task")
             : new Response<string>(HttpStatusCode.InternalServerError,"Task deleted  successfully!");
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex);
                 return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
            }
    }

    public async Task<Response<TaskItem?>> GetTaskItemByIdAsync(int taskitemid)
    {
          try
          {
               using var conn=context.Connection();
               var query="select * from taskitems where id=@Id";
               var selectByid= await conn.QueryFirstOrDefaultAsync<TaskItem>(query,new{Id=taskitemid});
                 return selectByid==null
                  ? new Response<TaskItem?>(HttpStatusCode.InternalServerError,"Company not found !")
                  : new Response<TaskItem?>(HttpStatusCode.InternalServerError, "Company  found !", selectByid);
          }
          catch (System.Exception ex)
          {
             Console.WriteLine(ex);
             return new Response<TaskItem?>(HttpStatusCode.InternalServerError,"Internal Server Error");
          }
    }

    public async Task<List<TaskItem>> GetTaskItemsAsync()
    {
             using var conn=context.Connection();
             var select="select * from taskitems";
             var res = await conn.QueryAsync<TaskItem>(select);
             return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(TaskItem taskItem)
    {
         try
         {
              using var conn=context.Connection();
              var update="update taskitems set title=@New, description=@Newdes where id=@Id";
              var res =await conn.ExecuteAsync(update, taskItem);
                return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not update Task")
             : new Response<string>(HttpStatusCode.OK,"Task successfully update");
         }
         catch (System.Exception ex)
         {
             Console.WriteLine(ex);
              return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Errorr");
              

         }
    }
}