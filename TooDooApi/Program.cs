using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;
using Validate;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://2doo-alpha.vercel.app", "https://localhost:5000");
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
#region PayOS
PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("can not find Environment"),
                        configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("can not find Environment"),
                        configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("can not find Environment")
                        );
#endregion

builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("local")), ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AddScope
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISubcriptionRepository, SubcriptionRepository>();
builder.Services.AddScoped<ISubcriptionService, SubcriptionService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<SubcriptionValidate>();
builder.Services.AddScoped<AccountValidate>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();
