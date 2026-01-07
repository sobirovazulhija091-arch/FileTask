using System.Net;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebTask.Services;
public class HabitlogService(ApplicationDbContext dbContext) :  IHabitlogService
{
    private readonly ApplicationDbContext context=dbContext;
     public Response<string> AddHabitlog(HabitLog habitlog)
     {
           try
       {
           using var conn =context.Connection();
           var query="insert into habitlogs(date,isCompleted) values(@date,@isCompleted)";
            var res =conn.Execute(query ,new{date=habitlog.Date,isCompleted=habitlog.IsCompleted});
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
      public Response<string> UpdateHabitlog(HabitLog habitlog)
      {
           try
         {
              using var conn=context.Connection();
              var update="update habitlogs set date=@date,isCompleted=@isCompleted where id=@Id";
              var res =conn.Execute(update, habitlog);
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
      public Response<string> DeleteHabitlog(int habitlogid)
      {
            try
            {
                 using var conn = context.Connection();
             var delete ="delete from habitlogs where id=@Id";
             var res =conn.Execute(delete,new{Id=habitlogid});
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
      public Response<HabitLog?>  GetHabitByIdlog(int habitlogid)
      {
          try
          {
               using var conn=context.Connection();
               var query="select * from habitlogs where id=@Id";
               var habitlog=conn.QueryFirstOrDefault<HabitLog>(query,new{Id=habitlogid});
                 return habitlog==null
                  ? new Response<HabitLog?>(HttpStatusCode.InternalServerError,"Company not found !")
                  : new Response<HabitLog?>(HttpStatusCode.InternalServerError, "Company  found !", habitlog);
          }
          catch (System.Exception ex)
          {
             Console.WriteLine(ex);
             return new Response<HabitLog?>(HttpStatusCode.InternalServerError,"Internal Server Error");
          }
      }
      public List<HabitLog> GetHabitlog()
      {
          using var conn=context.Connection();
             var select="select * from habitlogs";
             var res = conn.Query<HabitLog>(select).ToList();
             return res;
      }
}

