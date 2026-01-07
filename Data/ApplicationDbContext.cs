using Npgsql;

public class ApplicationDbContext
{
   
      private readonly string _connectionString="Host=localhost;Port=5432;Database=filewebnew;Username=postgres;Password=1234";
     public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
