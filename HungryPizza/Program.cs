using HungryPizza.Api.Auth;
using HungryPizza.Api.Services;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.Infrastructure.Interface;
using HungryPizza.Infra.Repositories;
using HungryPizza.Infra.Repositories.Interface;
using HungryPizza.Infra.UnitOfWork;
using HungryPizza.Infra.UnitOfWork.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Token"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<JwtManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<DbSession>();
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IPizzaFlavorService, PizzaFlavorService>();
    services.AddTransient<IConnectionFactory, ConnectionFactory>();
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddTransient<IOrderRepository, OrderRepository>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
    services.AddTransient<IPizzaFlavorRepository, PizzaFlavorRepository>();
    services.AddTransient<IProductRepository, ProductRepository>();
}
