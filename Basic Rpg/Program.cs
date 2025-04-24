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

Player player1 = new Player(1000, "frank", 50, 5, false, false, 1, 10, 20, 4, 20, 0, playerInventory, playerSkills);
Enemy enemy1 = new Enemy(150, "bob", 5, 10, false, false, 2, 5, 10, 5, 10, 0);

List<Entity> entityList = new List<Entity> { player1, enemy1 };
List<Player> playerList = new List<Player> { player1 };
List<Enemy> enemyList = new List<Enemy> { enemy1 };
List<Entity> entityTurnOrder = new List<Entity>();

bool arePlayersAlive = false;
bool areEnemiesAlive = false;


Turn_Controller controller = new Turn_Controller(entityTurnOrder);

while (true)
{
    controller.DecideInitialEntityOrder(entityList);

    controller.TurnOrderActions(playerList, enemyList);

    areEnemiesAlive = false;

    foreach (Enemy enemy in enemyList)
    {
        if (!enemy.IsDead())
        {
            areEnemiesAlive = true;
            break;
        }
    }

    arePlayersAlive = false;

    foreach (Player player in playerList)
    {
        if (!player.IsDead())
        {
            arePlayersAlive = true;
            break;
        }
    }

    if (playerList.Any(player => player.isFled == true))
        break;

    if (!arePlayersAlive)
        break;

    if (!areEnemiesAlive)
        break;
}

if (!areEnemiesAlive)
    Console.WriteLine($"Allies wins");
else if (!arePlayersAlive)
    Console.WriteLine($"Enemies wins");
else
    Console.WriteLine("The Party has escaped combat!");