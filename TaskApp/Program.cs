using Microsoft.EntityFrameworkCore;

using TaskAppDAL;
using TaskAppBL;
using TaskApp.Mappers;
using TaskAppBL.Mappers;
using TaskApp.Middlewares;
//using TaskApp.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.CustomSchemaIds(type => type.ToString())
);
builder.Services.AddDbContext<TaskAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskDbConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddAutoMapper(typeof(MappingProfile1));
builder.Services.AddAutoMapper(typeof(BLMapper));
builder.Services.AddAutoMapper(typeof(ApiMapper));

builder.Services.AddScoped<TaskAppDbContext>();
builder.Services.AddScoped<TaskOperations>();
builder.Services.AddScoped<TasksLogic>();

//builder.Services.AddScoped<TaskAppDatabaseConnection>();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<UserOperations>();
builder.Services.AddScoped<UsersLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
