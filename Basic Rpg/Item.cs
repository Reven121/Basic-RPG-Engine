using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    // Item is the abstract base class that all other times of items will inherit from.
    // As an abstract base class it will never be directly instantiated, and only
    // exists to define a common interface that it's children will implement.
    internal abstract class Item
    {
        public string itemName; // Name that will be displayed to the user
        public string effectDescription; // Textual description of the items effect
        public int numRemaining;

        public Item(string itemName, string effectDescription, int numRemaining) {
            this.itemName = itemName;
            this.effectDescription = effectDescription;
            this.numRemaining = numRemaining;
        }
       
        // The activate function will be called by an entity when they use the Item.
        // It takes the entity that the item will act upon as a target.
        // As an abstract method it has no implementation in the base class Item.
        // Instead the children of the Item class MUST override it and provider their
        // own implementation.
        public abstract void Activate(Entity target);

        public virtual void ItemUsed(Item item)
        {
            item.numRemaining = item.numRemaining - 1;
            if (item.numRemaining < 0)
                item.numRemaining = 0;
        }
    }

    // SimpleItem is an Item which simply applies adds or (or subtracts) a value
    // from an entities attack and/or defence
    internal class SimpleItem : Item
    {
        public int healthEffect; // How much to change the health up or down
        public int attackEffect; // How much to change the attack up or down

        public SimpleItem(
            string itemName, 
            string effectDescription,
            int numRemaining,
            int healthEffect,
            int attackEffect
        ) :  base(itemName, effectDescription, numRemaining) {
            this.healthEffect = healthEffect;
            this.attackEffect = attackEffect;
        }

        // Override Active so that we just modify the health and attack
        // by the prescribed amount 
        public override void Activate(Entity target) {
            target.ModifyHealthPoints(healthEffect);
            target.ModifyAttack(attackEffect);
        }
    }
}
