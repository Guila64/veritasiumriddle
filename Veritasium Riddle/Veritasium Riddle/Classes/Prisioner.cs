using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veritasium_Riddle.Classes
{
    internal class Prisioner
    {
        public Prisioner(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int? FoundAtBox { get; set; }
        public int Attempts { get; set; }
        public bool Succeed { get => FoundAtBox.HasValue; }

        public Task<bool> Try(int attemptsPerPrisioner, Dictionary<int, Box> boxes)
        {
            var boxToOpen = Id;
            for (int i = 0; i < attemptsPerPrisioner; i++)
            {
                Attempts++;
                var currentBox = boxes[boxToOpen];
                if (currentBox.PrisionerNumber == Id)
                {
                    FoundAtBox = currentBox.Id;
                    break;
                }
                boxToOpen = currentBox.PrisionerNumber;
            }
            return Task.FromResult(Succeed);
        }
    }
}
