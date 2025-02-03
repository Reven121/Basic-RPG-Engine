using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Turn_Controller
    {

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

            enties = enties.OrderBy(entity => entity.speed.Value);

            entityTurnOrder = enties;

            return entityTurnOrder;
        }


        public void TurnOrderActions(List<Entity> entityOrder, List<Player> players, List<Enemy> enemies)
        {
            foreach (Entity entity in entityOrder)
            {
                if (entity.GetType() == typeof(Player))
                {
                    PlayerTurn(entity, enemies, players);
                }
                else if (entity is Enemy)
                {
                    EnemyTurn(entity, players, enemies);
                }
                else
                {
                    continue;
                }
            }
        }

        public void PlayerTurn(Entity player, List<Enemy> enemies, List<Player> players)
        {



            if(enemy.IsDead())
                //remove from list so will not act again
                //Is this an issue if done with an ongoing foreach loop?
                //How to do this if muliple enemies die at the same time?
            
            if (player.IsDead())
              //remove from list if died, maybe add entityTurnOrder list so it can also be removed from there?
              //when player dies in their own turn from dot or self damage etc.
        }

        public void EnemyTurn(Entity enemy, List<Player> players, List<Enemy> enemies)
        {
                var random = new Random();
                int index = random.Next(players.Count);

                enemy.Attack(players[index]);
                Console.WriteLine($"{enemy.entityName} did {enemy.damageDone} to {players[index].entityName}");

                //needs same IsDead stuff as Player turn
        }
    }
}
