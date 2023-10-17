// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat.Server;

WebHost.CreateDefaultBuilder(args).UseStartup<Startup>()
    .UseUrls("http://localhost:6000")
    .Build()
    .Run();

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) { Configuration = configuration; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app)
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)); //Corregir esta configuración en producción

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/chat");
        });
    }
}
