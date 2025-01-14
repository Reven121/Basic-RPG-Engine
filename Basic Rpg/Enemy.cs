using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Enemy: Entity
    {
        public Enemy(int healthpoint, string name, int attack, int magicAttack, bool isDefending, int defence, int magicDefence, int maxMP, int maxSP, int currentMP, int currentSP) : base(healthpoint, name, attack, magicAttack, isDefending, defence, magicDefence, maxMP, maxSP, currentMP, currentSP) { }


    }
}
