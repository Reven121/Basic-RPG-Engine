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
EntityStats player_stats = new EntityStats(
    maxHealthPoints: 1000,
    attack: 50, 
    magicAttack: 5,
    defence: 1,
    magicDefence: 10,
    speed: 20,
    maxMP: 4,
    maxSP: 20
);
Player player1 = new Player(
    stats: player_stats,
    healthpoint: 1000, 
    name: "frank",
    isDefending: false,
    isFled: false,
    currentMP: 0, 
    currentSP: 8,
    inventory: playerInventory,
    skills: playerSkills
);
Player player2 = new Player(
    stats: player_stats,
    healthpoint: 1000, 
    name: "alice",
    isDefending: false,
    isFled: false,
    currentMP: 0, 
    currentSP: 8,
    inventory: playerInventory,
    skills: playerSkills
);

// Enemy Definitions 
EntityStats enemy_stats = new EntityStats(
    maxHealthPoints: 150,
    attack: 5,
    magicAttack: 10,
    defence: 2,
    magicDefence: 5,
    speed: 5,
    maxMP: 10,
    maxSP: 5
);
Enemy enemy1 = new Enemy(
    stats: enemy_stats,
    healthpoint: 150,
    name: "bob",
    isDefending: false,
    isFled: false,
    currentMP: 10,
    currentSP:0
);

List<Entity> entityList = new List<Entity> { player1, player2, enemy1 };
List<Player> playerList = new List<Player> { player1, player2 };
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
