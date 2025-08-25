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
	public bool isEnemyTargeted;

        public Skill(
	    string skillName,
	    string skillDescription,
	    int mpCost,
	    int spCost,
	    bool isEnemyTargeted
        ) {
            this.skillName = skillName;
            this.skillDescription = skillDescription;
            this.mpCost = mpCost;
            this.spCost = spCost;
	    this.isEnemyTargeted = isEnemyTargeted;
        }

        public abstract void UseSkill(Entity target, Entity user);
    }


    internal class BuffSkill : Skill {
	public Buff referenceBuff;

	public BuffSkill(
	    string skillName,
	    string skillDescription,
	    int mpCost,
	    int spCost,
	    Buff buff,
	    bool isEnemyTargeted
	) : base(skillName, skillDescription, mpCost, spCost, isEnemyTargeted) {
	    this.referenceBuff = buff;
	}

        public override void UseSkill(Entity target, Entity user) {
	    target.ApplyBuff(
		new Buff(
		    this.referenceBuff.bufName,
		    user.entityName,
		    this.referenceBuff.targetStat,
		    this.referenceBuff.buffAmount,
		    this.referenceBuff.buffPercent,
		    this.referenceBuff.maxBuffDuration
		)
	    );
	}
    }

    internal class BasicSkill : Skill
    {
        public int damageScaling;
        public int magicDamageScaling;

        public BasicSkill(
	    string skillName,
	    string skillDescription,
	    int mpCost,
	    int spCost,
	    int damageScaling,
	    int magicDamageScaling
        ) : base(skillName, skillDescription, mpCost, spCost, true) {
            this.damageScaling = damageScaling;
            this.magicDamageScaling = magicDamageScaling;
	}

        public override void UseSkill(Entity target, Entity user)
        {
            int damageDone = 0;

	    // basic damage
            if (damageScaling != 0) {
                damageDone = (damageScaling * user.stats.attack) - target.stats.defence;
	    }

	    // magic damage
            if (magicDamageScaling != 0) {
                damageDone = ((magicDamageScaling * user.stats.magicAttack) - target.stats.magicDefence) + damageDone;
	    }

	    if (damageDone < 0) {
		damageDone = 0;
	    }

            target.TakeDamage(damageDone);

            user.DamageDone(damageDone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }


    internal class HealSkill : Skill
    {
        public int damageScaling;
        public int magicDamageScaling;
        public double healthPercentage;
        
        public HealSkill(
	    string skillName,
            string skillDescription,
            int mpCost,
            int spCost,
            int damageScaling,
            int magicDamageScaling,
            double healthPercentage
        ) : base(skillName, skillDescription, mpCost, spCost, false) {
	    if (damageScaling != 0 && magicDamageScaling != 0) {
		throw new ArgumentException(
		    "Tried to create a HealSkill with both magic and ordinary damage scaling"
		);
	    }
	    
            this.damageScaling = damageScaling;
            this.magicDamageScaling = magicDamageScaling;
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
