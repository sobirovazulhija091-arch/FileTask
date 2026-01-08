public interface IHabitService
{
      public Task<Response<string>> AddHabitAsync(Habit habit);
      public Task<Response<string>> UpdateHabitAsync(Habit habit);
      public Task<Response<string>> DeleteHabitAsync(int habitid);
      public Task<Response<Habit?>> GetHabitByIdAsync(int habitid);
      public Task<List<Habit>> GetHabitAsync();
      public Task<int> GetCountOfhabitsAsync();
      public Task<Response<string>> UpdateHabitNameAsync(int habitid , string newname);

}