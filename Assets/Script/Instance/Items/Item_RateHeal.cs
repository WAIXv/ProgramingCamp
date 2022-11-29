using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script
{
    public class Item_RateHeal : Item_PickableBase
    {
        [SerializeField]
        private float rate = 12;
        public override bool OnPick(entity_Instance EI)
        {
            if(EI is player_Instance)
            {
                EI.Heal(rate / 100f * EI.getMaxHealth());
                return true;
            }
            return false;
        }
    }
}
