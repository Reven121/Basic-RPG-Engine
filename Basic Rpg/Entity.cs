using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Entity
    {
        public EntityStats stats;
        public int healthPoints;
        public string entityName;
        public bool isDefending;
        public bool isFled;
        public int currentMP;
        public int currentSP;
        public int currentLevel;
        public int startingLevel;
        public int maxlevel;

        public int damageDone;
        public int healingAmount;

        public List<Item> inventory;
        public List<Skill> skills;

        public Entity(
                EntityStats stats,
                int healthPoints,
                string entityName,
                bool isDefending,
                bool isFled,
                int currentMP,
                int currentSP,
                List<Item>? inventory = null,
                List<Skill>? skills = null)
        {
            this.stats = stats;
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.isDefending = isDefending;
            this.isFled = isFled;
            this.currentMP = currentMP;
            this.currentSP = currentSP;

            if (inventory == null) {
                this.inventory = new List<Item>();
            }
            else {
                this.inventory = inventory;
            }

            if (skills == null)
            {
                this.skills = new List<Skill>();
            }
            else
            {
                this.skills = skills;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            ModifyHealthPoints(-damage);
        }

        public virtual void HealDamage(int healing)
        {
            ModifyHealthPoints(+healing);
        }

        public virtual int DamageDone(int damage)
        {
            return damageDone = damage;
        }

        public virtual int HealingDone(int healing)
        {
            return healingAmount = healing;
        }

        public virtual void Attack(Entity target)
        {
            if (target.isDefending)
            {
                damageDone = (stats.attack / 2) - target.stats.defence;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
            }
            else
            {
                damageDone = stats.attack - target.stats.defence;
                target.TakeDamage(damageDone);
                DamageDone(damageDone);
            }

            currentSP = currentSP + 1;
            CurrentSpCheck(currentSP, stats.maxSP);
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

        public bool doneFled()
        {
            return isFled == true;
        }

        public virtual void Defend(Entity target)
        {
            target.isDefending = true;
        }

        public Item? UseItem(int index) {
            // Check if the index points to an item within the inventory
            // if it's out of the range of the inventory, return null
            if (index >= inventory.Count) {
                return null;
            }
            
            // Get (a reference to) the item we wish to use
            Item item = inventory[index];
            
            // All the Active function of the item passing ourself as the target
            // We could target an enemy and have it effect them but consider that
            // an exercise for the reader ;3
            item.Activate(this);

            item.ItemUsed(item);

            // Remove the item from the inventory now that we've used it
            if (item.numRemaining <= 0)
                inventory.RemoveAt(index);

            // Return the item used to caller in case they wish to know
            // exactly which item actually got used
            return item;
        }

        public void ModifyHealthPoints(int change) {
            healthPoints += change; // Equivalent to healthPoints  = healthPoints + change

            // Set the health points to 0 if they're negative as we don't wish
            // health points to ever be negative.
            if (healthPoints < 0) {
                healthPoints = 0;
            }

            if (healthPoints > stats.maxHealthPoints)
            {
                healthPoints = stats.maxHealthPoints;
            }
        }

        public void ModifyAttack(int change) {
            stats.attack += change;

            if (change < 0) {
                change  = 0;
            }
        }
    }

    class EntityStats
    {
        public int maxHealthPoints;
        public int attack;
        public int magicAttack;
        public int defence;
        public int magicDefence;
        public int speed;
        public int maxMP;
        public int maxSP;

        public EntityStats(
                int maxHealthPoints,
                int attack,
                int magicAttack,
                int defence,
                int magicDefence,
                int speed,
                int maxMP,
                int maxSP)
        {
            this.maxHealthPoints = maxHealthPoints;
            this.attack = attack;
            this.magicAttack = magicAttack;
            this.defence = defence;
            this.magicDefence = magicDefence;
            this.speed = speed;
            this.maxMP = maxMP;
            this.maxSP = maxSP;
        }


    }
}
