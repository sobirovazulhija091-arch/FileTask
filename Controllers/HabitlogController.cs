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
     public async Task<Response<string>> AddHabitlogAsync(HabitLog habitlog)
    {
        return await _habitlogService.AddHabitlogAsync(habitlog);
    }
    [HttpDelete("{habitlogid:int}")]
      public async Task<Response<string>> DeleteHabitlogAsync(int habitlogid)
    {
         return  await _habitlogService.DeleteHabitlogAsync(habitlogid);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateHabitlogAsync(HabitLog habitlog)
    {
        return await _habitlogService.UpdateHabitlogAsync(habitlog);
    }
    [HttpGet]
   public async Task<List<HabitLog>> GetHabitlogAsync()
    {
        return await _habitlogService.GetHabitlogAsync();
    }
    [HttpGet("{habitlogid:int}")]
      public async Task<Response<HabitLog?>> GetHabitByIdlogAsync(int habitlogid)
    {
         return await _habitlogService.GetHabitByIdlogAsync(habitlogid);
    }
}