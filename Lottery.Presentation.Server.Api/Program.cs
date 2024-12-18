using Lottery.Infrastructure.Injection;
using Lottery.Application.Injection;
using Scalar.AspNetCore;
using Serilog;

namespace Lottery.Presentation.Server.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader( )
                          .AllowAnyMethod( );
                });
            });

            var app = builder.Build();

            app.UseSerilogRequestLogging( );

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(opt =>
                {
                    opt.WithTitle("Lottery API")
                        .WithTheme(ScalarTheme.DeepSpace)
                        .WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Axios);
                });
            }

            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                Log.Information("Application is starting...");
                app.Run( );
            }
            catch ( Exception ex )
            {
                Log.Fatal(ex, "Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush( );
            }
        }
    }
}
