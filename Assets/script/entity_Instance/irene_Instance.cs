using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class irene_Instance : entity_Instance
{
    public override void KnockBack(Vector2 vec)
    {
        irene_ctrl ic = Obj.GetComponent<irene_ctrl>();
        ic.walk_state = 3; ic.stun_tick = ic.max_stun_tick;
        ic.setMove_v(ic.move_v + vec.x);
        ic.jump_state = 1;
        ic.rb.velocity += vec;
    }
}
