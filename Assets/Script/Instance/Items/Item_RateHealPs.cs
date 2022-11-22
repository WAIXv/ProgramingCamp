using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script
{
    public class Item_RateHealPs : pickable_item_ctrl
    {
        [SerializeField]
        private float time = 3f;
        [SerializeField]
        private float rate = 12f;
        public override bool OnPick(entity_Instance EI)
        {
            if(EI.Obj.tag == "player_obj")
            {
                EI.EfcMgr.Add(new Effect.RateHealEffect(time, rate));
                return true;
            }
            return false;
        }
    }
}
