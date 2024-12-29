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

        public Entity(int healthPoints, string entityName, int attack)
        {
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.attack = attack;
        }

        public virtual void TakeDamage(int damage)
        {
            healthPoints = healthPoints - damage;
        }

        public virtual void Attack(Entity target)
        {
            target.TakeDamage(attack);
        }

        public bool IsDead()
        {
            return healthPoints <= 0;
        }
    }
}
