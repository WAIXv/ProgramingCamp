using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class MyUtils
    {
        public delegate void Executer_Gobj_V(GameObject gameObject);
        public delegate void Executer_FFF_V(float a, float b, float c);
        public delegate void Executer();

        public static GameObject[] BoxRange(Vector2 Button, Vector2 Size, float angle, Vector2 direction, float distance, string tag)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(Button, Size, angle, direction, distance);
            List<GameObject> result = new List<GameObject>();
            foreach (RaycastHit2D hit in hits)
                if (!hit.collider.isTrigger && hit.collider.gameObject.tag == tag && result.IndexOf(hit.collider.gameObject) < 0)
                    result.Add(hit.collider.gameObject);
            return result.ToArray();
        }
    }




}
