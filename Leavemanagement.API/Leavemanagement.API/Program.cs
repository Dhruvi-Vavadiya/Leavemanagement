using Leavemanagement.Repository.Entity;
using Leavemanagement.Repository.Repository;
using Leavemanagement.Service.Service;
using Microsoft.EntityFrameworkCore;
//using StudentManagement.Repository.Domains;
//using StudentManagement.Repository.Repository;
//using StudentManagement.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<LeaveManagementsDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//builder.Services.Configure<EmailSettings>(
//    builder.Configuration.GetSection("EmailSettings"));
// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeSerivce>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ✅ Enable Swagger middleware
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = "swagger"; // default
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();


//SELECT* from Employees;
//select* from leaveBalances;
//select* from leaveRequests;
//select* from proofMappings;
//select* from roles;
//select* from usermappings;
