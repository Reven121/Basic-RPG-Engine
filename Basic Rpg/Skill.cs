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
        public bool isAttack;

        public Skill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling,
                    bool isAttack
                    )
        {
            this.skillName = skillName;
            this.skillDescription = skillDescription;
            this.mpCost = mpCost;
            this.spCost = spCost;
            this.damageScaling = damageScaling;
            this.magicDamageScaling = magicDamageScaling;
            this.isAttack = isAttack;
        }
        public abstract void UseSkill(Entity target, Entity user);
        
    }

    internal class BasicSkill : Skill
    {
        public BasicSkill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    uint damageScaling,
                    uint magicDamageScaling
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, true) { }

        public override void UseSkill(Entity target, Entity user)
        {
            int damagedone = 0;

	    // basic damage
            if (damageScaling != 0) {
                damagedone = (damageScaling * user.stats.attack) - target.stats.defence;
	    }

	    // magic damage
            if (magicDamageScaling != 0) {
                damagedone = ((magicDamageScaling * user.stats.magicAttack) - target.stats.magicDefence) + damagedone;
	    }

	    if (damageDone < 0) {
		damageDone = 0;
	    }

            target.TakeDamage(damagedone);

            user.DamageDone(damagedone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }


    internal class HealSkill : Skill
    {
        public double healthPercentage;
        
        public HealSkill(string skillName,
            string skillDescription,
            int mpCost,
            int spCost,
            uint damageScaling,
            uint magicDamageScaling,
            double healthPercentage,
        ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, false) { 
            this.healthPercentage = healthPercentage;
        }

        public override void UseSkill(Entity target, Entity user)
        {
            int healthHealed = 0;

            if (damageScaling != 0) {
                healthHealed = damageScaling * (int)Math.Ceiling(
                    user.stats.maxHealthPoints * this.healthPercentage
                );
            }
            if (magicDamageScaling != 0) {
                healthHealed = magicDamageScaling * (int)Math.Ceiling(
                    user.stats.magicAttack * this.healthPercentage
                );
            }

            if (healthHealed < 0) {
                healthHealed = 0;
            }

            target.HealDamage(healthHealed);

            user.HealingDone(healthHealed);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }
}
