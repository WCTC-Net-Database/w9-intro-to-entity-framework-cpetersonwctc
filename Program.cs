using W9_assignment_template.Data;
using W9_assignment_template.Services;

namespace W9_assignment_template;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new GameContext())
        {
            // Seed the data if necessary
            context.Seed();

            // Initialize GameEngine and Menu
            var gameEngine = new GameEngine(context);
            var menu = new Menu(gameEngine);

            // Show the menu
            menu.Show();
        }
    }
}


//dotnet ef database update
//dotnet ef migrations add InitialCreate
//dotnet ef database update 0
//dotnet ef migrations remove