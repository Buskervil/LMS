using Lms.Core.Api;
using Lms.Courses.Infrastructure.DataAccess;
using Lms.CoursesApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CourseController).Assembly);
builder.Services
    .AddCourseModule()
    .AddCoreModule();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
    }));

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<CoursesContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();