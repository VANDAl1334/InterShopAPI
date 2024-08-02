using System.Text;
using InterShopAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connection = "Host=localhost;Port=5432;Database=InterShop;Username=postgres";
builder.Configuration.GetConnectionString("DefaultConnection");
InterShopContext.ConnectionString = connection;
builder.Services.AddDbContext<InterShopContext>(options => options.UseNpgsql(connection));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ��������� �� ��, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            //������, �������������� ��������
            ValidIssuer = "ElectroWallet",

            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = "ElectroWalletClient",

            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Пересоздаем БД
InterShopContext context = new InterShopContext();

app.Run();

public class AuthOptions
{
    public const string ISSUER = "InterShop"; // �������� ������
    public const string AUDIENCE = "InterShopClient"; // ����������� ������
    const string KEY = "mysupersecret_secretkey!123";   // ���� ��� ��������
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}