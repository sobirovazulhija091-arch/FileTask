using System.Net;
using Dapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
public class HabitService(ApplicationDbContext dbContext) : IHabitService
{
    private readonly ApplicationDbContext context=dbContext;
    public async Task<Response<string>> AddHabitAsync(Habit habit)
    {
       try
       {
           using var conn =context.Connection();
           var query="insert into habits(name,frequency,isActive) values(@name,@frequency,@isActive)";
            var res = await conn.ExecuteAsync(query ,new{name=habit.Name,frequency=habit.Frequency,isActive=habit.IsActive});
             return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not add habit")
             : new Response<string>(HttpStatusCode.OK,"Habit successfully added!");
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
             ? new Response<string>(HttpStatusCode.NotFound,"Can not delete habit")
             : new Response<string>(HttpStatusCode.OK,"Habit deleted  successfully!");
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
                  ? new Response<Habit?>(HttpStatusCode.NotFound,"Habit not found !")
                  : new Response<Habit?>(HttpStatusCode.OK, "Habit  found !", habit);
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
              var update="update habits set name=@Name,frequency=@Frequency,isActive=@IsActive where id=@Id";
              var res =await conn.ExecuteAsync(update, habit);
                return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not update habit")
             : new Response<string>(HttpStatusCode.OK,"habit successfully update");
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
    public async Task<int> GetCountOfhabitsAsync()
     {
               using var conn = context.Connection();
          var query = "select count(*) from habits";
          var count = await conn.ExecuteScalarAsync<int>(query);
        return count;
          }
     public async  Task<Response<string>> UpdateHabitNameAsync(int habitid , string newname)
     {
          try
          {
                using var conn= context.Connection();
                var query="update habits set name=@Newname where id=@Id";
                 var res = await conn.ExecuteAsync(query,new{Newname=newname,Id=habitid});
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
     }
