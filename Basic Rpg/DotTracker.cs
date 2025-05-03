using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class DotTracker
    {
        int dotDuration;
        //int damagePerTurn;



        if(Player.isPlayerTurn)
            dotDuration = dotDuration - 1;

        if(dotDuration <= 0)
            RemoveDot();

        public void RemoveDot()
        {

        }
    }
}
