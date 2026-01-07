public class Habit
{
   public int Id{get;set;}
   public string Name{get;set;}=null!; 
   public string Frequency{get;set;}=null!;
//Daily / Weekly 
   public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
   public bool IsActive{get;set;}
}