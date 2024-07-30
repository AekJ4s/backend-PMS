# Enable Cross-Origin Requests (CORS) in ASP.NET Core

https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

//TODO: Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://www.example.com")
              .WithHeaders("Content-Type", "Authorization")
              .WithMethods("POST", "GET", "PUT", "DELETE");
    });
});

builder.Services.AddDbContext<ActivityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularApp");  //TODO: Enable CORS middleware
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```
