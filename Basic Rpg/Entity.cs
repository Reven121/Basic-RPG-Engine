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
        public List<Item> inventory;

        public Entity(
                int healthPoints,
                string entityName,
                int attack,
                bool isDefending,
                List<Item>? inventory = null)
        {
            this.healthPoints = healthPoints;
            this.entityName = entityName;
            this.attack = attack;
            this.isDefending = isDefending;
            if (inventory == null) {
                this.inventory = new List<Item>();
            }
            else {
                this.inventory = inventory;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            ModifyHealthPoints(-damage);
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

            // Remove the item from the inventory now that we've used it
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
        }

        public void ModifyAttack(int change) {
            attack += change;

            if (change < 0) {
                change  = 0;
            }
        }
    }
}
