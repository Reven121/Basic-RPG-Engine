using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Basic_Rpg;

namespace Basic_Rpg
{
    internal abstract class Skill
    {
        public string skillName;
        public string skillDescription;
        public int mpCost;
        public int spCost;
        public int damageScaling;
        public int magicDamageScaling;

        public Skill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling)
        {
            this.skillName = skillName;
            this.skillDescription = skillDescription;
            this.mpCost = mpCost;
            this.spCost = spCost;
            this.damageScaling = damageScaling;
            this.magicDamageScaling = magicDamageScaling;
        }
        public abstract void UseAttackSkill(Entity target, Entity user);
        
    }

    internal class BasicSkill : Skill
    {
        public BasicSkill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling) { }

        public override void UseAttackSkill(Entity target, Entity user)
        {
            int damagedone = 0;

            if (!(damageScaling <= 0))
                damagedone = (damageScaling * user.attack) - target.defence;
            if (!(magicDamageScaling <= 0))
                damagedone = ((magicDamageScaling * user.magicAttack) - target.magicDefence) + damagedone;

            target.TakeDamage(damagedone);

            user.DamageDone(damagedone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }  
}
