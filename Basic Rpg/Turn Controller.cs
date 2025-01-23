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
        
        public List<Entity> entityTurnOrder = new List<Entity>();

        public Turn_Controller()
        {

        }


        public List<Entity> DecideInitialEntityOrder(List<Entity> enties)
        {

            enties = enties.OrderBy(entity => entity.speed.Value);

            entityTurnOrder = enties;

            return entityTurnOrder;
        }


        public void TurnOrderActions(List<Entity> entityOrder)
        {
            foreach (Entity entity in entityOrder)
            {
                if (entity.GetType() == typeof(Player))
                {
                    PlayerTurn(entity);
                }
                else if (entity is Enemy)
                {
                    EnemyTurn(entity);
                }
                else
                {
                    continue;
                }
            }
        }

        public void PlayerTurn(Entity player)
        {

        }

        public void EnemyTurn(Entity enemy)
        {

        }
    }
}
