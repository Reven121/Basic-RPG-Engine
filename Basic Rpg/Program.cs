// See https://aka.ms/new-console-template for more information
using Basic_Rpg;

Console.WriteLine("Hello, World!");

Player player = new Player(1000, "frank", 50);
Enemy enemy = new Enemy(150, "bob", 5);

while (!enemy.IsDead() && !player.IsDead())
{
    Console.WriteLine($"{player.entityName} HP: {player.healthPoints}, {enemy.entityName} HP: {enemy.healthPoints}");
    //player turnl
    Console.WriteLine("Press Enter To Attack");
    Console.ReadLine();
    player.Attack(enemy);
    Console.WriteLine($"{player.entityName} did {player.attack} to {enemy.entityName}");

    //enemy turn
    enemy.Attack(player);
    Console.WriteLine($"{enemy.entityName} did {enemy.attack} to {player.entityName}");
    
}

if (enemy.IsDead())
    Console.WriteLine($"{player.entityName} wins");
else
    Console.WriteLine($"{enemy.entityName} wins");