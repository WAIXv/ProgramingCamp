using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class texes_Instance : entity_Instance
{
    public override void KnockBack(Vector2 vec)
    {
        texes_ctrl ic = Obj.GetComponent<texes_ctrl>();
        ic.walk_state = 3; ic.stun_tick = ic.max_stun_tick;
        ic.setMove_v(ic.move_v + vec.x);
        ic.jump_state = 1;
        ic.rb.velocity += vec;
    }
}