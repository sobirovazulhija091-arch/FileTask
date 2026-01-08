using System.Net;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace WebTask.Services;
public class HabitlogService(ApplicationDbContext dbContext) :  IHabitlogService
{
    private readonly ApplicationDbContext context=dbContext;
     public async Task<Response<string>> AddHabitlogAsync(HabitLog habitlog)
     {
           try
       {
           using var conn =context.Connection();
           var query="insert into habitlogs(habitid,isCompleted) values(@HabitId,@IsCompleted)";
            var res = await conn.ExecuteAsync(query ,new{habitid=habitlog.HabitId,isCompleted=habitlog.IsCompleted});
             return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not add habitlog")
             : new Response<string>(HttpStatusCode.OK,"habitlog successfully added!");
       }
       catch (System.Exception ex)
       {
          Console.WriteLine(ex);
          return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
       }
     }
      public async Task<Response<string>> UpdateHabitlogAsync(HabitLog habitlog)
      {
           try
         {
              using var conn=context.Connection();
              var update="update habitlogs set isCompleted=@isCompleted where id=@Id";
              var res =await conn.ExecuteAsync(update, habitlog);
                return res==0
             ? new Response<string>(HttpStatusCode.InternalServerError,"Can not update habitlog")
             : new Response<string>(HttpStatusCode.OK,"Habitlog successfully update");
         }
         catch (System.Exception ex)
         {
             Console.WriteLine(ex);
              return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Errorr");
         }
      }
      public async Task<Response<string>> DeleteHabitlogAsync(int habitlogid)
      {
            try
            {
                 using var conn = context.Connection();
             var delete ="delete from habitlogs where id=@Id";
             var res = await conn.ExecuteAsync(delete,new{Id=habitlogid});
             return res==0
             ? new Response<string>(HttpStatusCode.NotFound,"Can not delete habitlog")
             : new Response<string>(HttpStatusCode.OK,"Habitlog deleted  successfully!");
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex);
                 return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
            }
      }
      public async Task<Response<HabitLog?>>  GetHabitByIdlogAsync(int habitlogid)
      {
          try
          {
               using var conn=context.Connection();
               var query="select * from habitlogs where id=@Id";
               var habitlog= await conn.QueryFirstOrDefaultAsync<HabitLog>(query,new{Id=habitlogid});
                 return habitlog==null
                  ? new Response<HabitLog?>(HttpStatusCode.NotFound,"Habitlog not found !")
                  : new Response<HabitLog?>(HttpStatusCode.OK, "Habitlog  found !", habitlog);
          }
          catch (System.Exception ex)
          {
             Console.WriteLine(ex);
             return new Response<HabitLog?>(HttpStatusCode.InternalServerError,"Internal Server Error");
          }
      }
      public async Task<List<HabitLog>> GetHabitlogAsync()
      {
          using var conn=context.Connection();
             var select="select * from habitlogs";
             var res =await conn.QueryAsync<HabitLog>(select);
             return res.ToList();
      }
       public async Task<int> CountCompletedLogsAsync(int habitId)
    {
        using var conn = context.Connection();
        var query = "SELECT COUNT(*) FROM habitlogs WHERE habitId = @HabitId AND isCompleted = true";
        return await conn.ExecuteScalarAsync<int>(query, new { HabitId = habitId });
    }
}

