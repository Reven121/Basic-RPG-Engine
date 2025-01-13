using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Skill
    {
        public string skillName;
        public int mpCost;
        public int spCost;
        public int damageScaling;
        public int magicDamageScaling;

        public Skill(string skillName, int mpCost, int spCost, int damageScaling, int magicDamageScaling)
        {
            this.skillName = skillName;
            this.mpCost = mpCost;
            this.spCost = spCost;
            this.damageScaling = damageScaling;
            this.magicDamageScaling = magicDamageScaling;
        }
    }
}
