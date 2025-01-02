using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Enemy: Entity
    {
        public Enemy(int healthpoint, string name, int attack, bool isDefending) : base(healthpoint, name, attack, isDefending) { }


    }
}
