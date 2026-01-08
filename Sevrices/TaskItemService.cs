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
             var insert = @"insert into taskitems(title,description,isCompleted)
             values(@title,@description,@iscompleted)";
             var res = await conn.ExecuteAsync(insert,new{title=taskItem.Title,description=taskItem.Description,iscompleted=taskItem.IsCompleted,});
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
             ? new Response<string>(HttpStatusCode.NotFound,"Can not delete task")
             : new Response<string>(HttpStatusCode.OK,"Task deleted  successfully!");
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
                  ? new Response<TaskItem?>(HttpStatusCode.NotFound,"Company not found !")
                  : new Response<TaskItem?>(HttpStatusCode.OK, "Company  found !", selectByid);
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
              var update="update taskitems set title=@Title, description=@Description where id=@Id";
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

    public async Task<Response<string>> UpdateDescriptionAsync(int taskitemid, string newDescription)
     {
          try
          {
                using var conn= context.Connection();
                var query="update taskitems set description=@Newdes where id=@Id";
                 var res = await conn.ExecuteAsync(query,new{Newdes=newDescription,Id=taskitemid});
           return res==0
             ? new Response<string>(HttpStatusCode.NotFound,"Can not update task")
             : new Response<string>(HttpStatusCode.OK,"Task update  successfully!");
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex);
                 return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
            }
     }
    public async Task<Response<string>>  UpdateAsTitleAsync(int taskitemid ,bool iscompleted)
     {
           try
          {
                using var conn= context.Connection();
                var query="update taskitems set  isCompleted=@Iscompleted where id=@Id";
                 var res = await conn.ExecuteAsync(query,new{Iscompleted=iscompleted,Id=taskitemid});
           return res==0
             ? new Response<string>(HttpStatusCode.NotFound,"Can not found update task")
             : new Response<string>(HttpStatusCode.OK,"Task update  successfully!");
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex);
                 return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
            }
     }
       public async Task<int> CountTask()
     {
           using var conn = context.Connection();
           var query="select count(*) from taskitems";
           var count=await conn.ExecuteScalarAsync<int>(query);
           return count;
     }
     }
     
