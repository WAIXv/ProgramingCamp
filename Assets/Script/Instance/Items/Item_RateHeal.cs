using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script
{
    public class Item_RateHeal : pickable_item_ctrl
    {
        [SerializeField]
        private float rate = 12;
        public override bool OnPick(entity_Instance EI)
        {
            if(EI.Obj.tag == "player_obj")
            {
                EI.Heal(rate / 100f * EI.getMaxHealth());
                return true;
            }
            return false;
        }
    }
}
