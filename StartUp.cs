using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RPG.Data;
using RPG.Menu;
using RPG.Models.CharacterInfo;
using RPG.Models.Monster;
using RPG.Models.Player;
using RPG.Models.Races;
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
