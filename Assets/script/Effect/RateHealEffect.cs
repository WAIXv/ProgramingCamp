using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script.Effect
{
    public class RateHealEffect : EffectBase
    {
        private float rate_ps;
        public RateHealEffect(float maxTick, float rate_ps) : base(maxTick)
        {
            this.rate_ps = rate_ps;
        }
        public override void OnUpdate(entity_Instance EI)
        {
            EI.Heal(this.rate_ps / 100f * EI.getMaxHealth() * Time.deltaTime);
        }
    }
}
