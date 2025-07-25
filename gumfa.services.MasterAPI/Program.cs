using gumfa.services.MasterAPI;
using gumfa.services.MasterAPI.Data;
using gumfa.services.MasterAPI.Mapper;
using gumfa.services.MasterAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR LOGGER CONFIGURATION

//Install - Package Serilog.AspNetCore
//Install - Package Serilog.Sinks.Console
//Install - Package Serilog.Sinks.File
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var logger = new LoggerConfiguration()
    .MinimumLevel.Is(env == "Development" ? LogEventLevel.Debug : LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File($"Logs/log-{env}-.txt",
    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = logger;  // Make Serilog the default logger

//// BELLOW IS IS CONFIGURATION FOR STORE LOGIG IN EMAIL AND SQL SERVER DATABASE 
//.WriteTo.MSSqlServer("your_connection", new MSSqlServerSinkOptions { TableName = "ErrorLogs", AutoCreateSqlTable = true })
//.WriteTo.Email(new EmailConnectionInfo
//{
//    FromEmail = "your-api@domain.com",
//    ToEmail = "admin@domain.com",
//    MailServer = "smtp.domain.com",
//    Port = 587,
//    EnableSsl = true,
//    NetworkCredentials = new NetworkCredential("username", "password"),
//    EmailSubject = "Critical Error in Gumfa API"
//}, restrictedToMinimumLevel: LogEventLevel.Fatal)

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR DB CONFIGURATION
builder.Services.AddDbContext<MasterDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR ARCHIVE LOGS MAKE ZIP FOR ALL LOGS FILE WHICH ARE CREATED IN LAST MONTH
builder.Services.AddHostedService<MonthlyLogArchiver>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

// Add services to the container.
builder.Services.AddControllers();

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR REPOSITORY CONFIGURATION (CONFIGURE MICRO SERVICE)
builder.Services.AddScoped<IProductService, ProductService>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR AUTOMAPPER CONFIGURATION (CONFIGURE AUTOMAPPER SERVICE)
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR AUTHENTICATION AND AUTHORIZATION WITH JWT TOKEN CONFIGURATION (CONFIGURE JWT TOKEN SERVICE)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // 👈 Set the default scheme
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"]!)
            )
        };
    });

builder.Services.AddAuthorization(); // Optional but good practice


//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR AUTOMAPPER CONFIGURATION (CONFIGURE AUTOMAPPER SERVICE)
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

var app = builder.Build();

app.UseSerilogRequestLogging();  // Logs HTTP request info

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<MasterDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
