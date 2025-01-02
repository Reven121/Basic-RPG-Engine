using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Entity
    {
        public int healthPoints;
        public string entityName;
        public int attack;
        public bool isDefending;

        public Entity(int healthPoints, string entityName, int attack, bool isDefending)
        {
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.attack = attack;
            this.isDefending = isDefending;
        }

        public virtual void TakeDamage(int damage)
        {
            healthPoints = healthPoints - damage;
        }

        public virtual void Attack(Entity target)
        {
            if (target.isDefending)
            {
                target.TakeDamage(attack / 2);
            }
            else
            {
                target.TakeDamage(attack);
            }

            target.isDefending = false;
        }

        public bool IsDead()
        {
            return healthPoints <= 0;
        }

        public virtual void Defend(Entity target)
        {
            target.isDefending = true;
        }
    }
}
