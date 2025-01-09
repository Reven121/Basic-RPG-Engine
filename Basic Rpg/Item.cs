using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Rpg
{
    internal class Item
    {

        public int hpRestored;

        public Item (int hpRestored)
        {
            this.hpRestored = hpRestored;
        }

        public virtual void ApplyItem()
        {

        }
    }
}
