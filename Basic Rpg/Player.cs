using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Player : Entity
    {
        public Player(int healthpoint,
                      EntityStats stats,
                      string name,
                      bool isDefending,
                      bool isFled,
                      int currentMP,
                      int currentSP,
                      List<Item>? inventory = null,
                      List<Skill>? skills = null)
                      : base(stats,
                            healthpoint,
                            name,
                            isDefending,
                            isFled,
                            currentMP,
                            currentSP,
                            inventory,
                            skills) { }
    }
}
