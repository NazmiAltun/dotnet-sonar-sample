using Dotnet.Sonar.Sample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMathService, MathService>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
public partial class Program { }
