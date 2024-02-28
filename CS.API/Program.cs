using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.BL.Services;
using CS.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IDialogService, DialogService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<ICustomMapper, CustomMapper>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowMyOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}