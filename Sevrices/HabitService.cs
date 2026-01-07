using System.Net;
using Dapper;
public class HabitService(ApplicationDbContext dbContext) : IHabitService
{
    private readonly ApplicationDbContext context=dbContext;
    public async Task<Response<string>> AddHabitAsync(Habit habit)
    {
       try
       {
           using var conn =context.Connection();
           var query="insert into habits(name,frequency,createdAt,isActive) values(@name,@frequency,@createdAt,@isActive)";
            var res = await conn.ExecuteAsync(query ,new{name=habit.Name,frequency=habit.Frequency,createdAt=habit.CreatedAt,isActive=habit.IsActive});
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

    public async Task<Response<string>> DeleteHabitAsync(int habitid)
    {
          try
            {
                 using var conn = context.Connection();
             var delete ="delete from habits where id=@Id";
             var res = await conn.ExecuteAsync(delete,new{Id=habitid});
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

    public async Task<Response<Habit?>> GetHabitByIdAsync(int habitid)
    {
        try
          {
               using var conn=context.Connection();
               var query="select * from habits where id=@Id";
               var habit= await conn.QueryFirstOrDefaultAsync<Habit>(query,new{Id=habitid});
                 return habit==null
                  ? new Response<Habit?>(HttpStatusCode.InternalServerError,"Company not found !")
                  : new Response<Habit?>(HttpStatusCode.InternalServerError, "Company  found !", habit);
          }
          catch (System.Exception ex)
          {
             Console.WriteLine(ex);
             return new Response<Habit?>(HttpStatusCode.InternalServerError,"Internal Server Error");
          }
    }

    public async Task<Response<string>> UpdateHabitAsync(Habit habit)
    {
         try
         {
              using var conn=context.Connection();
              var update="update habits set name=@name,frequency=@frequency,createdAt=@createdA,isActive=@isActive where id=@Id";
              var res =await conn.ExecuteAsync(update, habit);
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
    public async Task<List<Habit>> GetHabitAsync()
    {
         using var conn=context.Connection();
             var select="select * from habits";
             var res = await conn.QueryAsync<Habit>(select);
             return res.ToList();
    }
}