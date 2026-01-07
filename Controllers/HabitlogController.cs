using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebTask.Services;

namespace WebTask.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HabitlogController(IHabitlogService habitlogService):ControllerBase
{
    private readonly  IHabitlogService _habitlogService=habitlogService;
    [HttpPost]
     public Response<string> AddHabitlog(HabitLog habitlog)
    {
        return _habitlogService.AddHabitlog(habitlog);
    }
    [HttpDelete("{habitlogid:int}")]
      public Response<string> DeleteHabitlog(int habitlogid)
    {
         return _habitlogService.DeleteHabitlog(habitlogid);
    }
    [HttpPut]
    public Response<string> UpdateHabitlog(HabitLog habitlog)
    {
        return _habitlogService.UpdateHabitlog(habitlog);
    }
    [HttpGet]
   public List<HabitLog> GetHabitlog()
    {
        return _habitlogService.GetHabitlog();
    }
    [HttpGet("{habitlogid:int}")]
      public Response<HabitLog?> GetHabitByIdlog(int habitlogid)
    {
         return _habitlogService.GetHabitByIdlog(habitlogid);
    }
}