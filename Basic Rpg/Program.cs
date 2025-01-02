// See https://aka.ms/new-console-template for more information
using Basic_Rpg;

Console.WriteLine("Hello, World!");

Player player = new Player(1000, "frank", 50, false);
Enemy enemy = new Enemy(150, "bob", 5, false);

while (!enemy.IsDead() && !player.IsDead())
{
    Console.WriteLine($"{player.entityName} HP: {player.healthPoints}, {enemy.entityName} HP: {enemy.healthPoints}");
    //player turnl
    Console.WriteLine("Type A To Attack Or D To Defend.");
    string user_input = Console.ReadLine();
    if (user_input == "a")
    {
        player.Attack(enemy);
        Console.WriteLine($"{player.entityName} did {player.attack} to {enemy.entityName}");
    }
    else if(user_input == "d")
    {
        player.Defend(player);
    }
    else
    {
        Console.WriteLine("Invalid Command");
        continue;
    }

    //enemy turn
    enemy.Attack(player);
    Console.WriteLine($"{enemy.entityName} did {enemy.attack} to {player.entityName}");
    
}

if (enemy.IsDead())
    Console.WriteLine($"{player.entityName} wins");
else
    Console.WriteLine($"{enemy.entityName} wins");