using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Turn_Controller
    {
        int? ReadIntFromConsole()
        {
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

        public List<Entity> entityTurnOrder;
        

     

        public Turn_Controller(List<Entity>? entityTurnOrder = null)
        {
            if (entityTurnOrder == null)
            {
                this.entityTurnOrder = new List<Entity>();
            }
            else
            {
                this.entityTurnOrder = entityTurnOrder;
            }
        }


        public List<Entity> DecideInitialEntityOrder(List<Entity> enties)
        {

            enties = enties.OrderBy(entity => -entity.speed).ToList();

            entityTurnOrder = enties;

            return entityTurnOrder;
        }


        public void TurnOrderActions(List<Player> players, List<Enemy> enemies)
        {
            foreach (Entity entity in this.entityTurnOrder)
            {
                if (players.Any(player => player.isFled == true))
                {
                    break;
                }
                if (entity.GetType() == typeof(Player) && !entity.IsDead())
                {
                    PlayerTurn(entity, enemies, players);
                }
                else if (entity.GetType() == typeof(Enemy) && !entity.IsDead())
                {
                    EnemyTurn(entity, players, enemies);
                }
                else
                {
                    continue;
                }
            }
        }

        public void PlayerTurn(Entity CurrentPlayer, List<Enemy> enemies, List<Player> players)
        {
            bool isPlayerTurn = true;
            

            while (isPlayerTurn) {

                Console.WriteLine($"{CurrentPlayer.entityName} HP: {CurrentPlayer.healthPoints} MP {CurrentPlayer.currentMP} SP {CurrentPlayer.currentSP}");
                //player turn
                Console.WriteLine("(0) Ally and Enemy Stats");
                Console.WriteLine("(1) ATTACK");
                Console.WriteLine("(2) DEFEND");
                Console.WriteLine("(3) SHOW SKILLS");
                Console.WriteLine("(4) USE SKILL");
                Console.WriteLine("(5) SHOW INVENTORY");
                Console.WriteLine("(6) USE ITEM");
                Console.WriteLine("(7) Escape");

                string? user_input = Console.ReadLine();

                if (user_input == "0")
                {
                    Console.WriteLine("Allies");
                    foreach (Player player in players)
                    {
                        string indicator = "-";
                        if (player == CurrentPlayer)
                            indicator = "*";
                        if (player.IsDead())
                            indicator = "#";
                        Console.WriteLine($"{indicator} {player.entityName} HP: {player.healthPoints} MP {player.currentMP} SP {player.currentSP}");
                    }
                    Console.WriteLine("Enemies");
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].IsDead())
                            continue;

                        Console.WriteLine($"{i} {enemies[i].entityName} HP: {enemies[i].healthPoints}");
                    }
                }
                if (user_input == "1")
                {
                    //add check to ask them which enemy if there is more than one
                    //add check here so player cannot attack already dead enemy
                    Console.WriteLine("Select Enemy To Target For Attack");
                    int? index = ReadIntFromConsole();
                    if (index == null)
                    {
                        Console.WriteLine("INVALID INPUT");
                        continue;
                    }

                    if (index.Value < 0 || index.Value >= enemies.Count)
                    {
                        Console.WriteLine("NO SUCH ENEMY FOUND. TRY AGAIN.");
                        continue;
                    }

                    Enemy enemy = enemies[index.Value];

                    if (enemy.IsDead())
                    {
                        Console.WriteLine("NO SUCH ENEMY FOUND. TRY AGAIN.");
                        continue;
                    }

                    CurrentPlayer.Attack(enemy);

                    Console.WriteLine($"{CurrentPlayer.entityName} did {CurrentPlayer.damageDone} to {enemy.entityName}");
                }
                else if (user_input == "2")
                {
                    CurrentPlayer.Defend(CurrentPlayer);
                }
                else if (user_input == "3")
                {
                    for (int i = 0; i < CurrentPlayer.skills.Count; i++)
                    {
                        Skill skill = CurrentPlayer.skills[i];
                        Console.WriteLine($"SKILL {i}");
                        Console.WriteLine($"NAME: {skill.skillName}");
                        Console.WriteLine($"DESCRIPTION: {skill.skillDescription}");
                        Console.WriteLine($"MP COST: {skill.mpCost}");
                        Console.WriteLine($"SP COST: {skill.spCost}");
                    }
                    continue;
                }
                else if (user_input == "4")
                {
                    Console.WriteLine("WHICH SKILL SHOULD BE USED?");
                    int? skillIndex = ReadIntFromConsole();
                    if (skillIndex == null)
                    {
                        Console.WriteLine("INVALID INPUT");
                        continue;
                    }

                    if (skillIndex.Value < 0 || skillIndex.Value >= CurrentPlayer.skills.Count)
                    {
                        Console.WriteLine("NO SUCH SKILL FOUND. TRY AGAIN.");
                        continue;
                    }

                    Skill skill_to_use = CurrentPlayer.skills[skillIndex.Value];

                    if (skill_to_use.spCost > CurrentPlayer.currentSP)
                    {
                        Console.WriteLine("NOT ENOUGH SP. TRY ANOTHER SKILL");
                        continue;
                    }
                    else if (skill_to_use.mpCost > CurrentPlayer.currentMP)
                    {
                        Console.WriteLine("NOT ENOUGH MP. TRY ANOTHER SKILL");
                        continue;
                    }
                    //add if statement to see if skill targets enemies or allies then act accordingly
                    Console.WriteLine("Select Enemy To Target For Attack");
                    int? enemyIndex = ReadIntFromConsole();
                    if (enemyIndex == null)
                    {
                        Console.WriteLine("INVALID INPUT");
                        continue;
                    }

                    if (enemyIndex.Value < 0 || enemyIndex.Value >= enemies.Count)
                    {
                        Console.WriteLine("NO SUCH ENEMY FOUND. TRY AGAIN.");
                        continue;
                    }

                    Enemy enemy = enemies[enemyIndex.Value];

                    if (enemy.IsDead())
                    {
                        Console.WriteLine("NO SUCH ENEMY FOUND. TRY AGAIN.");
                        continue;
                    }

                    skill_to_use.UseSkill(enemy, CurrentPlayer);
                }
                else if (user_input == "5")
                {
                    if (CurrentPlayer.inventory.Count == 0)
                    {
                        Console.WriteLine("INVENTORY IS EMPTY T^T");
                        continue;
                    }
                    for (int i = 0; i < CurrentPlayer.inventory.Count; i++)
                    {
                        Item item = CurrentPlayer.inventory[i];
                        Console.WriteLine($"ITEM {i}, NAME: {item.itemName}, DESCRIPTION: {item.effectDescription}, Remaining Uses: {item.numRemaining}");
                    }
                    continue;
                }
                else if (user_input == "6")
                {
                    Console.WriteLine("WHICH ITEM DO YOU WANT TO USE?");
                    int? index = ReadIntFromConsole();
                    if (index == null)
                    {
                        Console.WriteLine("INVALID INPUT");
                        continue;
                    }

                    Item? item = CurrentPlayer.UseItem(index.Value);
                    if (item == null)
                    {
                        Console.WriteLine("ITEM DOES NOT EXIST");
                        continue;
                    }
                    Console.WriteLine($"USED {item.itemName} to {item.effectDescription}");
                }
                else if (user_input == "7")
                {
                    Console.WriteLine("You try to escape....");
                    CurrentPlayer.isFled = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                    continue;
                }

                break;
            }
        }

        public void EnemyTurn(Entity CurrentEnemy, List<Player> players, List<Enemy> enemies)
        {
            bool playersAreAlive = false;

            foreach(Player player in players)
            {
                if (!player.IsDead())
                {
                    playersAreAlive = true;
                    break;
                }
            }

            if (!playersAreAlive)
                return;

            int index = DecideEnemyTarget(players);

            CurrentEnemy.Attack(players[index]);
            Console.WriteLine($"{CurrentEnemy.entityName} did {CurrentEnemy.damageDone} to {players[index].entityName}");

        }

        public int DecideEnemyTarget(List<Player> players)
        {
            var random = new Random();
            int index = -1;
            while (true)
            {
                index = random.Next(players.Count);
                if (!players[index].IsDead())
                {
                    break;
                }
            }
            
            return index;
        }
    }
}
