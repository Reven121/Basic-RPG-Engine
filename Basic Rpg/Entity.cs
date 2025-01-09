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
        public int magicAttack;
        public int defence;
        public int magicDefence;
        public int speed;
        public bool isDefending;
        public bool isAttackUp;
        public int maxMP;
        public int maxSP;
        public int currentMP;
        public int currentSP;
        public int currentLevel;
        public int startingLevel;
        public int maxlevel;

        public int damageDone;

        public Entity(int healthPoints, string entityName, int attack, bool isDefending, int defence, int maxMP, int maxSP, int currentMP, int currentSP)
        {
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.attack = attack;
            this.isDefending = isDefending;
            this.defence = defence;
            this.maxMP = maxMP;
            this.maxSP = maxSP;
            this.currentMP = currentMP;
            this.currentSP = currentSP;
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
                damageDone = (attack / 2) - target.defence;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
            }
            else
            {
                damageDone = attack - target.defence;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
            }

            currentSP = currentSP + 1;
            CurrentSpCheck(currentSP, maxSP);
            target.isDefending = false;
        }

        public int CurrentSpCheck(int currentSp, int maxSP)
        {
            if (currentSP >= maxSP)
                currentSP = maxSP;
            if (currentSP < 0)
                currentSP = 0;
            return currentSP;
        }

        public int CurrentMPCheck(int currentMP, int maxMP)
        {
            if (currentMP >= maxMP)
                currentMP = maxMP;
            if (currentMP < 0)
                currentMP = 0;
            return currentMP;
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
