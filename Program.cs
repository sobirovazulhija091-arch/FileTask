using WebTask.Services;

var builder=WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITaskItemService,TaskItemService>();
builder.Services.AddScoped<IHabitService,HabitService>();
builder.Services.AddScoped<IHabitlogService, HabitlogService>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();  
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.Run();