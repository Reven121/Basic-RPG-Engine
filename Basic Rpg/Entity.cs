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
        public bool isAttackUp;

        public int damageDone;

        public Entity(int healthPoints, string entityName, int attack, bool isDefending, bool isAttackUp)
        {
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.attack = attack;
            this.isDefending = isDefending;
            this.isAttackUp = isAttackUp;
        }

        public virtual void TakeDamage(int damage)
        {
            healthPoints = healthPoints - damage;
        }

        public virtual int DamageDone(int damage)
        {
            return damageDone = damage;
        }

        public virtual void Attack(Entity target)
        {
            if (target.isDefending)
            {
                damageDone = attack / 2;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
            }
            else
            {
                damageDone = attack;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
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

        public virtual void Skill()
        { 
                    
        }
    }
}
