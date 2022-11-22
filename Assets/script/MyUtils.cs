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

        public static GameObject[] BoxRange(Vector2 Button, Vector2 Size, float angle, Vector2 direction, float distance, int LayerMask, params string[] tags)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(Button, Size, angle, direction, distance, LayerMask);
            List<GameObject> result = new List<GameObject>();
            foreach (RaycastHit2D hit in hits)
                if (!hit.collider.isTrigger && FindInArray<string>(tags, hit.collider.gameObject.tag) && result.IndexOf(hit.collider.gameObject) < 0)
                    result.Add(hit.collider.gameObject);
            return result.ToArray();
        }

        private static bool FindInArray<T>(T[] arr,T find)
        {
            foreach(T val in arr)
            {
                if(val.Equals(find)) return true;
            }
            return false;
        }


        public static GameObject[] BoxRange(Vector2 Button, Vector2 Size, float angle, Vector2 direction, float distance, params string[] tag)
        {
            return BoxRange(Button,Size,angle,direction,distance,Int32.MaxValue,tag); 
        }

        public static bool GroundCheck(GameObject ground_sensor,float distance,int layerMask)
        {
            RaycastHit2D rch = Physics2D.Raycast(ground_sensor.transform.position, Vector2.down, distance, layerMask);
            return rch.collider;
        }

        public static bool GroundCheckBox(GameObject ground_sensor, float distance, int layerMask)
        {
            RaycastHit2D rch = Physics2D.BoxCast(ground_sensor.transform.position,ground_sensor.transform.localScale,ground_sensor.transform.rotation.z, Vector2.down, distance, layerMask);
            return rch.collider;
        }

        public static bool GroundCheckBox(GameObject ground_sensor, int layerMask)
        {
            return GroundCheckBox(ground_sensor, 0f, layerMask);
        }

    }




}
