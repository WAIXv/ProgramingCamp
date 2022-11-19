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

        public void Add(EffectBase effect)
        {
            if (OnEffectAdd != null)
                if (!OnEffectAdd(effect))
                    return;
            effects.Add(effect);
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
        public EffectBase getEffectInstance<T>()
        {
            foreach (EffectBase efc in effects)
            {
                if(efc is T) return efc;
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
