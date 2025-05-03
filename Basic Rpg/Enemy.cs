using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Enemy: Entity
    {
        public Enemy(int healthpoint,
                    int maxHealthPoints,
                    string name,
                    int attack,
                    int magicAttack,
                    bool isDefending,
                    bool isFled,
                    int defence,
                    int magicDefence,
                    int maxMP,
                    int maxSP,
                    int currentMP,
                    int currentSP)
                    : base(healthpoint,
                            maxHealthPoints,
                            name,
                            attack,
                            magicAttack,
                            isDefending,
                            isFled,
                            defence,
                            magicDefence,
                            maxMP,
                            maxSP,
                            currentMP,
                            currentSP) { }


    }
}
