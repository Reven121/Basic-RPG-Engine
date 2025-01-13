using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Player : Entity
    {
        public Player(int healthpoint, string name, int attack, bool isDefending, List<Item>? inventory = null, int defence, int maxMP, int maxSP, int currentMP, int currentSP) : base(healthpoint, name, attack, isDefending, inventory, defence, maxMP, maxSP, currentMP, currentSP) { }
    }
}
