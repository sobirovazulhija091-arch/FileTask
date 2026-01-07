namespace WebTask.Services;
public interface IHabitlogService
{
      public Response<string> AddHabitlog(HabitLog habitlog);
      public Response<string> UpdateHabitlog(HabitLog habitlog);
      public Response<string> DeleteHabitlog(int habitlogid);
      public Response<HabitLog?> GetHabitByIdlog(int habitlogid);
      public List<HabitLog> GetHabitlog();
}