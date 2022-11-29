using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class SkillBase
    {
        public string name;
        public float skillPoint = 0f;
        public float SkillTimeRate = 0f;
        public float skillPointBuffer { private set; get; } = 0f;
        public Texture2D Icon;
        public SkillBase(string name, Texture2D icon, float skillPoint) {
            this.skillPoint = skillPoint;
            this.Icon = icon;
            this.name=name;
        }
        public virtual void Update() {
            AddSkillPoint(Time.deltaTime);
        }
        public virtual void AddSkillPoint(float a)
        {
            skillPointBuffer += a;
            if(skillPointBuffer >= skillPoint) skillPointBuffer = skillPoint;
        }
        public virtual void ClearSkillPoint()
        {
            skillPointBuffer = 0f;
        }

        public virtual bool isAvailable()
        {
            return skillPointBuffer >= skillPoint;
        }
    }
}
