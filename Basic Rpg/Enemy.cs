using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Enemy: Entity
    {
        public Enemy(EntityStats stats,
                    int healthpoint,
                    string name,
                    bool isDefending,
                    bool isFled,
                    int currentMP,
                    int currentSP)
                    : base(stats,
                           healthpoint,
                           name,
                           isDefending,
                           isFled,
                           currentMP,
                           currentSP) { }


    }
}
