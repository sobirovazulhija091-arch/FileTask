using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
[ApiController]
[Route("api/[controller]")]
public class HabitController(IHabitService habitService):ControllerBase
{
    private readonly  IHabitService _habitService=habitService;
    [HttpPost]
     public async Task<Response<string>> AddHabitAsync(Habit habit)
    {
        return await _habitService.AddHabitAsync(habit);
    }
    [HttpDelete("{habitid:int}")]
      public async Task<Response<string>> DeleteHabitAsync(int habitid)
    {
         return await _habitService.DeleteHabitAsync(habitid);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateHabitAsync(Habit habit)
    {
        return await _habitService.UpdateHabitAsync(habit);
    }
    [HttpGet]
   public async Task<List<Habit>> GetHabitAsync()
    {
        return await _habitService.GetHabitAsync();
    }
    [HttpGet("{habitid:int}")]
      public async Task<Response<Habit?>> GetHabitByIdAsync(int habitid)
    {
         return await _habitService.GetHabitByIdAsync(habitid);
    }
    [HttpGet("count")]
       public async Task<int> GetCountOfhabitsAsync()
  {
        return await _habitService.GetCountOfhabitsAsync();
  }
   [HttpPut("{habitid:int}/name/{newname}")]
   public async  Task<Response<string>> UpdateHabitNameAsync(int habitid , string newname)
  {
      return await _habitService.UpdateHabitNameAsync(habitid,newname);
  }
}