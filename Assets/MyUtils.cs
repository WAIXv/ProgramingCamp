using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class MyUtils
    {
        public delegate void Executer();

        public class HashMap<K,V> : Hashtable
        {
            public HashMap()
            {
                //todo
            }

            public void Add(K key, V value)
            {
                this[key] = value;
            }

            public V Get(K key)
            {
                return (V)this[key];
            }

            public void Remove(K key)
            {
                this[key] = null;
            }
        }
    }



}
