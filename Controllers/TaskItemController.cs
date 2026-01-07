using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
[ApiController]
[Route("api/[controller]")]
public class TaskItemController(ITaskItemService taskItemService):ControllerBase
{
    private readonly  ITaskItemService _taskItemService=taskItemService;
    [HttpPost]
     public Response<string> Add(TaskItem taskItem)
    {
        return _taskItemService.Add(taskItem);
    }
    [HttpDelete("{taskitemid:int}")]
      public Response<string> Delete(int taskitemid)
    {
         return _taskItemService.Delete(taskitemid);
    }
    [HttpPut]
    public Response<string> Update(TaskItem taskItem)
    {
        return _taskItemService.Update(taskItem);
    }
    [HttpGet]
   public List<TaskItem> GetTaskItems()
    {
        return _taskItemService.GetTaskItems();
    }
    [HttpGet("{taskitemid:int}")]
      public Response<TaskItem?> GetTaskItemById(int taskitemid)
    {
         return _taskItemService.GetTaskItemById(taskitemid);
    }
}