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
     public async Task<Response<string>> AddHabitlogAsync(Habit habit)
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
      public async Task<Response<Habit?>> GetHabitById(int habitid)
    {
         return await _habitService.GetHabitByIdAsync(habitid);
    }
}