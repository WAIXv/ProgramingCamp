using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SkillPoint : Item_PickableBase
{

    [SerializeField]
    private float skillPointRestored = 12f;
    public override bool OnPick(entity_Instance EI)
    {
        if(EI is player_Instance)
        {
            player_Instance PI = (player_Instance)EI;
            PI.Skill.AddSkillPoint(skillPointRestored);
            return true;
        } 
        else return false;
    }




}
