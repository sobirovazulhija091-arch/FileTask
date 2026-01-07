public interface IHabitService
{
      public Response<string> AddHabit(Habit habit);
      public Response<string> UpdateHabit(Habit habit);
      public Response<string> DeleteHabit(int habitid);
      public Response<Habit?> GetHabitById(int habitid);
      public List<Habit> GetHabit();
}