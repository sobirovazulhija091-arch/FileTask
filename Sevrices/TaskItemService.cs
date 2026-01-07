using System.Net;
using Dapper;
using Npgsql;

public class TaskItemService(ApplicationDbContext dbContext) : ITaskItemService
{
    private readonly ApplicationDbContext context=dbContext;
    public Response<string> Add(TaskItem taskItem)
    {
        try
        {
             using var conn=context.Connection();
             var insert = @"insert into taskitems(title,description,isCompleted,dueDate,createdAt)
             values(@title,@description,@iscompleted,@duedate,createdat)";
             var res=conn.Execute(insert,new{title=taskItem.Title,description=taskItem.Description,iscompleted=taskItem.IsCompleted,
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

    public Response<string> Delete(int taskitemid)
    {
            try
            {
                 using var conn = context.Connection();
             var delete ="delete from taskitems where id=@Id";
             var res =conn.Execute(delete,new{Id=taskitemid});
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

    public Response<TaskItem?> GetTaskItemById(int taskitemid)
    {
          try
          {
               using var conn=context.Connection();
               var query="select * from taskitems where id=@Id";
               var selectByid=conn.QueryFirstOrDefault<TaskItem>(query,new{Id=taskitemid});
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

    public List<TaskItem> GetTaskItems()
    {
             using var conn=context.Connection();
             var select="select * from taskitems";
             var res = conn.Query<TaskItem>(select).ToList();
             return res;
    }

    public Response<string> Update(TaskItem taskItem)
    {
         try
         {
              using var conn=context.Connection();
              var update="update taskitems set title=@New, description=@Newdes where id=@Id";
              var res =conn.Execute(update, taskItem);
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