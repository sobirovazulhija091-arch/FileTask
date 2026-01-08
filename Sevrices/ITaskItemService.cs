public interface ITaskItemService
{
    public Task<Response<string>> AddAsync(TaskItem taskItem);
    public Task<Response<string>> UpdateAsync(TaskItem taskItem);
    public Task<Response<string>> DeleteAsync(int taskitemid);   
    public Task<Response<TaskItem?>> GetTaskItemByIdAsync(int taskitemid);
    public Task<List<TaskItem>> GetTaskItemsAsync();
    public  Task<Response<string>> UpdateDescriptionAsync(int taskitemid, string newDescription);
    public  Task<Response<string>>  UpdateAsTitleAsync(int taskitemid ,bool iscompleted);
}