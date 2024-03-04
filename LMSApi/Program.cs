using Lms.Core.Api;
using Lms.CoursesApi;
using Lms.User.Api;
using LMSApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.OperationFilter<MyHeaderFilter>();
});
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CourseController).Assembly);

builder.Services
    .AddCoreModule(builder.Configuration)
    .AddUsersModule(builder.Configuration)
    .AddCourseModule(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
    }));

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