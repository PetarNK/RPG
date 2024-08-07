using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RPG.Data;
using RPG.Services.CharacterService;
using RPG.Services.GameService;

namespace RPGGame.Services.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddDbContext<GameContext>(options =>
                            options.UseSqlServer(Config.ConnectionString))
                        .AddScoped<ICharacterService, CharacterService>()
                        .AddScoped<IGameService, GameService>());

    }
}