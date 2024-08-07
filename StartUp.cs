using Microsoft.Extensions.DependencyInjection;
using RPG.Data;
using RPG.Services.GameService;
using RPGGame.Services.Hosting;

namespace RPG
{
    public class StartUp
    {

        static void Main(string[] args)
        {

            var host = HostBuilderExtensions.CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var gameService = services.GetRequiredService<IGameService>();
                gameService.RunGame();
            }
           
        }

    }
    
}
