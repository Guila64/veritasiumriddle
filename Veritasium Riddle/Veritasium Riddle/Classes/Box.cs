using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veritasium_Riddle.Classes
{
    internal class Box
    {
        public Box(int id, int prisionerNumber)
        {
            Id = id;
            PrisionerNumber = prisionerNumber;
        }

        public int Id { get; }
        public int PrisionerNumber { get; set; }
    }
}
