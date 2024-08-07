using Microsoft.Extensions.DependencyInjection;
using RPG.Services.GameService;
using RPGGame.Services.Hosting;

namespace RPG
{
    public class StartUp
    {

        static void Main(string[] args)
        {
            //Before using the app, please change the connection string in Data\Config.cs. with your database connection string!
            
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
