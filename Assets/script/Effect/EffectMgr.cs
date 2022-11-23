using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script
{
    public class EffectMgr
    {
        private List<EffectBase> effects = new List<EffectBase>();
        public delegate bool Executer(EffectBase effectBase);
        public Executer OnEffectAdd;
        public Executer OnEffectRemove;
        public EffectMgr() {
        }

        public EffectBase Add(EffectBase effect)
        {
            EffectBase tmp = getEffectInstance(effect.GetType());
            if (tmp != null) effects.Remove(tmp);
            if (OnEffectAdd != null)
                if (!OnEffectAdd(effect))
                    return null;
            effects.Add(effect);
            return effect;
        }
        public void Remove(EffectBase effect)
        {
            if (OnEffectRemove != null)
                if (!OnEffectRemove(effect))
                    return;
            effects.Remove(effect);
        }
        public void Clear()
        {
            effects.Clear();
        }
        public EffectBase getEffectInstance(Type type)
        {
            foreach (EffectBase efc in effects)
            {
                if(efc.GetType() == type) return efc;
            }
            return null;
        }

        public void Update(entity_Instance EI)
        {
            for(int i =0; i < effects.Count; i++)
            {
                if (effects[i].Update(EI))
                {
                    effects.Remove(effects[i]);
                }
            }
        }


    }
}
