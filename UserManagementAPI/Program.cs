using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ��������� DbContext � ���� ������ � ������
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("UserDb"));

// ��������� SignalR
builder.Services.AddSignalR();

// ��������� ����������� � Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHub<UserHub>("/userHub");

app.Run();
