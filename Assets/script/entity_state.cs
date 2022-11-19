using System.Collections.Generic;
using UnityEngine;
using static Assets.MyUtils;

namespace Assets
{
    public static class EntityStates
    {
        static EntityStates()
        {
        }
        public interface IState
        {
            public void OnUpdate();
            public void OnEnter();
            public void OnLeave();
        }



        public class EntityStateMgr
        {
            List<EntityState> t = new List<EntityState>();
            public EntityStateMgr()
            {
            }

            public EntityState Add(EntityState es)
            {
                t.Add(es);
                return es;
            }
            public void Remove(EntityState es)
            {
                t.Remove(es);
            }

            //public delegate bool FuncEs_B(EntityState entityState);
            public delegate void FuncEs_V(EntityState entityState);
            public void Iter(FuncEs_V OnEach)
            {
                foreach (EntityState entityStates in t)
                {
                    OnEach(entityStates);
                }
            }
        }



        public class EntityState
        {
            protected bool Active = false;
            public Executer_Gobj_V OnEnter;
            public Executer_Gobj_V OnExit;
            public Executer_Gobj_V OnUpdate;

            public EntityState()
            {
                this.OnEnter = (Gobj) => { };
                this.OnExit = (Gobj) => { };
                this.OnUpdate = (Gobj) => { };
            }

            public EntityState(bool active, Executer_Gobj_V onEnter, Executer_Gobj_V onUpdate, Executer_Gobj_V onExit)
            {   
                this.OnEnter = onEnter;
                this.OnExit = onExit;
                this.OnUpdate = onUpdate;
                Active = active;
            }

            public EntityState(Executer_Gobj_V onEnter, Executer_Gobj_V onUpdate, Executer_Gobj_V onExit)
            {
                this.OnEnter = onEnter;
                this.OnExit = onExit;
                this.OnUpdate = onUpdate;
            }

            public void SetActive(GameObject gObj, bool active)
            {
                if(active != Active)
                {
                    if (active)
                        OnEnter(gObj);
                    else
                        OnExit(gObj);
                }
                Active = active;
            }
            public void SetActive(bool active)
            {
                if (active != Active)
                {
                    if (active)
                        OnEnter(null);
                    else
                        OnExit(null);
                }
                Active = active;
            }


            public bool isActive()
            {
                return Active;
            }


        }
    }
}
