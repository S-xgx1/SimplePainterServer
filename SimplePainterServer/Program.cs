using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MainDateBase).Assembly);
builder.Services.AddDbContext<MainDateBase>(opt =>
    opt.UseMySql(
        builder.Environment.IsDevelopment()
            ? "server=localhost;database=SimplePainter;user=root;password=1104;"
            : "server=119.3.163.38;database=SimplePainter;user=root;password=1104;",
        new MariaDbServerVersion(new Version(11, 4, 2))));
InitDir();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
var provider = new FileExtensionContentTypeProvider
{
    Mappings =
    {
        [".data"] = "application/octet-stream"
    }
};

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
app.Run();
return;

void InitDir()
{
    Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "Image"));
}