using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class EntityStates
    {
        static EntityStates()
        {
        }


        public enum Player
        {
            IDLE, WALK, JUMP
        }
    }
}
