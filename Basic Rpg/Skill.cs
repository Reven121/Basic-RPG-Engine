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
                    int damageScaling,
                    int magicDamageScaling
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, true) { }

        public override void UseSkill(Entity target, Entity user)
        {
            int damagedone = 0;

            if (!(damageScaling <= 0))
                damagedone = (damageScaling * user.stats.attack) - target.stats.defence;
            if (!(magicDamageScaling <= 0))
                damagedone = ((magicDamageScaling * user.stats.magicAttack) - target.stats.magicDefence) + damagedone;

            target.TakeDamage(damagedone);

            user.DamageDone(damagedone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }


    internal class HealSkill : Skill
    {
        
        public HealSkill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, false) { }

        public override void UseSkill(Entity target, Entity user)
        {
            int damagedone = 0;

            if (!(damageScaling <= 0))
                damagedone = damageScaling * (int)Math.Ceiling(user.stats.maxHealthPoints * 0.1);
            if (!(magicDamageScaling <= 0))
                damagedone = magicDamageScaling * (int)Math.Ceiling(user.stats.magicAttack * 0.1);

            target.HealDamage(damagedone);

            user.HealingDone(damagedone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }

    internal class BuffSkill : Skill
    {
        private int buffSetAmount;
        private double buffPercent;
        private int buffDuration;

        public BuffSkill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling,
                    int buffSetAmount,
                    double buffPercent,
                    int buffDuration
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, false) {
                    this.buffDuration = buffDuration;
                    this.buffSetAmount = buffSetAmount; 
                    this.buffPercent = buffPercent;
                    }

        public override void UseSkill(Entity target, Entity user)
        {
            int buffValue = 0;

            if (buffSetAmount > 0)
                buffValue = buffValue + buffSetAmount;
            if (buffPercent > 0)
                buffValue = (int)Math.Ceiling(buffValue * buffPercent);

            target.ModifyAttack(buffValue);

            //user.HealingDone(damagedone);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }

    internal class DotSkill : Skill
    {
        public DotSkill(string skillName,
                    string skillDescription,
                    int mpCost,
                    int spCost,
                    int damageScaling,
                    int magicDamageScaling
                    ) : base(skillName, skillDescription, mpCost, spCost, damageScaling, magicDamageScaling, true) { }

        public override void UseSkill(Entity target, Entity user)
        {
            int damageDonePerTick = 0;

            //int dotDuration = 0;

            //if (!(damageScaling <= 0))
                //damagedone = (damageScaling * user.attack) - target.defence;
            //if (!(magicDamageScaling <= 0))
                //damagedone = ((magicDamageScaling * user.magicAttack) - target.magicDefence) + damagedone;

            //target.TakeDamage(damageDonePerTick);

            user.DamageDone(damageDonePerTick);

            user.currentSP = user.currentSP - spCost;
            user.currentMP = user.currentMP - mpCost;
        }
    }
}
