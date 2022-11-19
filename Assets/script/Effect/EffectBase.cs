using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script
{
    public class EffectBase
    {
        public float max_tick { get; }
        private float tick = 0;
        private bool first = true;
        public EffectBase(float maxTick) {
            max_tick = maxTick;

        }

        public bool Update(entity_Instance EI) // return isEnd
        {
            if (first)
            {
                first = false;
                OnStart(EI);
            }
            else
            {
                OnUpdate(EI);
            }
            tick += Time.deltaTime;
            return tick >= max_tick;
        }

        public void End(entity_Instance EI)
        {
            OnEnd(EI);
            tick = max_tick;
        }

        public virtual void OnEnd(entity_Instance EI) { }
        public virtual void OnUpdate(entity_Instance EI) { }
        public virtual void OnStart(entity_Instance EI) { }
    }
}
