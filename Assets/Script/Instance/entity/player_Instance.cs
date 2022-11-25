using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class player_Instance : entity_Instance
{
    public SkillBase Skill;
    public override void Update()
    {
        if(Skill != null && !death) Skill.Update();
        base.Update();
    }

}
