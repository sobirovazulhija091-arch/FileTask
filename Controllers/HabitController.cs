using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
[ApiController]
[Route("api/[controller]")]
public class HabitController(IHabitService habitService):ControllerBase
{
    private readonly  IHabitService _habitService=habitService;
    [HttpPost]
     public Response<string> AddHabitlog(Habit habit)
    {
        return _habitService.AddHabit(habit);
    }
    [HttpDelete("{habitid:int}")]
      public Response<string> DeleteHabit(int habitid)
    {
         return _habitService.DeleteHabit(habitid);
    }
    [HttpPut]
    public Response<string> UpdateHabit(Habit habit)
    {
        return _habitService.UpdateHabit(habit);
    }
    [HttpGet]
   public List<Habit> GetHabit()
    {
        return _habitService.GetHabit();
    }
    [HttpGet("{habitid:int}")]
      public Response<Habit?> GetHabitById(int habitid)
    {
         return _habitService.GetHabitById(habitid);
    }
}