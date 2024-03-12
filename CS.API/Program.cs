using CS.API.Helpers;
using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.BL.Services;
using CS.DAL.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CS.Api.BackgroundServices;
using CS.API.Helpers.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDialogService, DialogService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDetailsService, DetailsService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ICustomMapper, CustomMapper>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<BackgroundNotificationService>();

builder.Services.AddValidatorsFromAssemblyContaining<TicketCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TicketSolveDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TicketCloseDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SendMessageDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbString"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowMyOrigins", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth:Audience"],

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Auth:SecretKey"])),
        };
    });

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.SeedAllData();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowMyOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();