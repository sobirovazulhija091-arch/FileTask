public interface ITaskItemService
{
    public Response<string> Add(TaskItem taskItem);
    public Response<string> Update(TaskItem taskItem);
    public Response<string> Delete(int taskitemid);   
    public Response<TaskItem?> GetTaskItemById(int taskitemid);
    public List<TaskItem> GetTaskItems();
}