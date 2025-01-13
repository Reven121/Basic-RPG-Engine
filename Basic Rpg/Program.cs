using Basic_Rpg;

int? ReadIntFromConsole()  {
    // Read input from the console
    string input = Console.ReadLine();
    
    try
    {
        // Convert the input to an integer
        int number = int.Parse(input);
        return number;
    }
    catch (Exception)
    {
        return null;
    }
}

// We create a couple of items here and add them to the inventory
SimpleItem healingPotion = new SimpleItem("Soothing Balm", "heal 50 HP", 2, 50, 0);
SimpleItem attackPotion = new SimpleItem("Stim Pack", "increase attack by 10 AP at the cost of 50 HP",4, -50, 10);
List<Item> playerInventory = new List<Item>{ healingPotion, attackPotion };

Player player = new Player(1000, "frank", 50, false, 1, 20, 4, 20, 0, playerInventory);
Enemy enemy = new Enemy(150, "bob", 5, false, 2, 10, 5, 10, 0);

while (!enemy.IsDead() && !player.IsDead())
{
    Console.WriteLine($"{player.entityName} HP: {player.healthPoints} MP {player.currentMP} SP {player.currentSP}, {enemy.entityName} HP: {enemy.healthPoints}");
    //player turnl
    Console.WriteLine("(1) ATTACK");
    Console.WriteLine("(2) DEFEND");
    Console.WriteLine("(3) SHOW INVENTORY");
    Console.WriteLine("(4) USE ITEM");

    string? user_input = Console.ReadLine();
    
    if (user_input == "1")
    {
        player.Attack(enemy);
        Console.WriteLine($"{player.entityName} did {player.damageDone} to {enemy.entityName}");
    }
    else if(user_input == "2")
    {
        player.Defend(player);
    }
    else if(user_input == "3")
    {
        if (player.inventory.Count == 0) {
            Console.WriteLine("INVENTORY IS EMPTY T^T");
            continue;
        }
        for (int i = 0; i < player.inventory.Count; i++) {
            Item item = player.inventory[i];
            Console.WriteLine($"ITEM {i}, NAME: {item.itemName}, DESCRIPTION: {item.effectDescription}, Remaining Uses: {item.numRemaining}");
        }
        continue;
    }
    else if (user_input == "4") {
        Console.WriteLine("WHICH ITEM DO YOU WANT TO USE?");
        int? index = ReadIntFromConsole();
        if (index == null) {
            Console.WriteLine("INVALID INPUT");
            continue;
        }

        Item? item = player.UseItem(index.Value);
        if (item == null) {
            Console.WriteLine("ITEM DOES NOT EXIST");
            continue;
        }
        Console.WriteLine($"USED {item.itemName} to {item.effectDescription}");
    }
    else
    {
        Console.WriteLine("Invalid Command");
        continue;
    }

    //enemy turn
    enemy.Attack(player);
    Console.WriteLine($"{enemy.entityName} did {enemy.damageDone} to {player.entityName}");
    
}

if (enemy.IsDead())
    Console.WriteLine($"{player.entityName} wins");
else
    Console.WriteLine($"{enemy.entityName} wins");
