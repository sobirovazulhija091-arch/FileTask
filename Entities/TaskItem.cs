public class TaskItem
{
    public int Id{get;set;}
    public string Title{get;set;}=null!;
    public string Description{get;set;}=null!;
    public bool IsCompleted{get;set;}
    public DateTime DueDate{get;set;}=DateTime.UtcNow;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}