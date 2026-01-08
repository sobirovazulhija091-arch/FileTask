namespace WebTask.Services;
public interface IHabitlogService
{
      public Task<Response<string>> AddHabitlogAsync(HabitLog habitlog);
      public Task<Response<string>> UpdateHabitlogAsync(HabitLog habitlog);
      public Task<Response<string>> DeleteHabitlogAsync(int habitlogid);
      public Task<Response<HabitLog?>> GetHabitByIdlogAsync(int habitlogid);
      public Task<List<HabitLog>> GetHabitlogAsync();
      public  Task<int> CountCompletedLogsAsync(int habitId);
      public  Task<int> CountCompletedTrueOrFalseLogsAsync(bool Completed);
      

}