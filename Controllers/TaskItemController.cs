using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
[ApiController]
[Route("api/[controller]")]
public class TaskItemController(ITaskItemService taskItemService):ControllerBase
{
    private readonly  ITaskItemService _taskItemService=taskItemService;
    [HttpPost]
     public async Task<Response<string>> AddAsync(TaskItem taskItem)
    {
        return await _taskItemService.AddAsync(taskItem);
    }
    [HttpDelete("{taskitemid:int}")]
      public async Task<Response<string>> DeleteAsync(int taskitemid)
    {
         return  await _taskItemService.DeleteAsync(taskitemid);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(TaskItem taskItem)
    {
        return  await _taskItemService.UpdateAsync(taskItem);
    }
    [HttpGet]
   public async Task<List<TaskItem>> GetTaskItemsAsync()
    {
        return await _taskItemService.GetTaskItemsAsync();
    }
    [HttpGet("{taskitemid:int}")]
      public async Task<Response<TaskItem?>> GetTaskItemByIdAsync(int taskitemid)
    {
         return  await _taskItemService.GetTaskItemByIdAsync(taskitemid);
    }
  
   [HttpPut("{taskitemid}/name/{newDescription}")]
public async Task<Response<string>> UpdateDescriptionAsync(int taskitemid, string newDescription)
{
    return await _taskItemService.UpdateDescriptionAsync(taskitemid, newDescription);
}
}
