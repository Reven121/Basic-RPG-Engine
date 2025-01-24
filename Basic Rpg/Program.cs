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

// Skills for Player or Enemy
BasicSkill physicalAttack = new BasicSkill("Three Pronged Strike", "A Series Of Sweeping Blows: Deals 3x Physical Damage To Target Enemy", 0, 3, 3, 0);
BasicSkill magicAttack = new BasicSkill("Crimson Burst", "Creates A Explosion Of Fire: Deals 2x Magic Damage To Target Enemy", 5, 0, 0, 2);
List<Skill> playerSkills = new List<Skill> { physicalAttack, magicAttack };

Player player = new Player(1000, "frank", 50, 5, false, 1, 10, 20, 4, 20, 30, playerInventory, playerSkills);
Enemy enemy = new Enemy(150, "bob", 5, 10, false, 2, 5, 10, 5, 10, 0);

while (!enemy.IsDead() && !player.IsDead())
{
    Console.WriteLine($"{player.entityName} HP: {player.healthPoints} MP {player.currentMP} SP {player.currentSP}, {enemy.entityName} HP: {enemy.healthPoints}");
    //player turnl
    Console.WriteLine("(1) ATTACK");
    Console.WriteLine("(2) DEFEND");
    Console.WriteLine("(3) SHOW SKILLS");
    Console.WriteLine("(4) USE SKILL");
    Console.WriteLine("(5) SHOW INVENTORY");
    Console.WriteLine("(6) USE ITEM");

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
        for (int i = 0; i < player.skills.Count; i++)
        {
            Skill skill = player.skills[i];
            Console.WriteLine($"SKILL {i}");
            Console.WriteLine($"NAME: {skill.skillName}");
            Console.WriteLine($"DESCRIPTION: {skill.skillDescription}");
            Console.WriteLine($"MP COST: {skill.mpCost}");
            Console.WriteLine($"SP COST: {skill.spCost}");
        }
        continue;
    }
    else if(user_input == "4")
    {
        Console.WriteLine("WHICH SKILL SHOULD BE USED?");
        int? index = ReadIntFromConsole();
        if (index == null)
        {
            Console.WriteLine("INVALID INPUT");
            continue;
        }

        if (index.Value < 0 || index.Value >= player.skills.Count) {
            Console.WriteLine("NO SUCH SKILL FOUND. TRY AGAIN.");
            continue;
        }

        Skill skill_to_use = player.skills[index.Value];

        if (skill_to_use.spCost > player.currentSP)
        {
            Console.WriteLine("NOT ENOUGH SP. TRY ANOTHER SKILL");
            continue;
        }
        else if (skill_to_use.mpCost > player.currentMP)
        {
            Console.WriteLine("NOT ENOUGH MP. TRY ANOTHER SKILL");
            continue;
        }
        else {
            skill_to_use.UseAttackSkill(enemy, player);
        }
    }
    else if(user_input == "5")
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
    else if (user_input == "6") {
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


    if (enemy.IsDead()) {
        // enemy is dead so they don't get to attack again
        break;
    }

    //enemy turn
    enemy.Attack(player);
    Console.WriteLine($"{enemy.entityName} did {enemy.damageDone} to {player.entityName}");
    
}

if (enemy.IsDead())
    Console.WriteLine($"{player.entityName} wins");
else
    Console.WriteLine($"{enemy.entityName} wins");
