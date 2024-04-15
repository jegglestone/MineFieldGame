using Microsoft.Extensions.DependencyInjection;

namespace MinesweeperGame
{
    internal class Program
    {
        private static ServiceProvider CreateServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new Application())
                .BuildServiceProvider();

            return serviceProvider;
        }

        static void Main(string[] args)
        {
            ServiceProvider services = CreateServices();

            var app = services.GetRequiredService<Application>();
            app.PlayGame();              
        }  
    }
}
