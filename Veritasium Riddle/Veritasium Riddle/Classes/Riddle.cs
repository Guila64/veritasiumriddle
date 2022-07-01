using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veritasium_Riddle.Classes;

namespace Veritasium_Riddle.Classes
{
    internal class Riddle
    {
        public int NumberOfPrisionersAndBoxes { get; set; } = 100;
        private int AttemptsPerPrisioner { get => NumberOfPrisionersAndBoxes / 2; }

        Dictionary<int, Box> _boxes;
        List<Prisioner> _prisioners;

        public Riddle(int numberOfPrisionersAndBoxes)
        {
            NumberOfPrisionersAndBoxes = numberOfPrisionersAndBoxes;

            _boxes = new Dictionary<int, Box>(NumberOfPrisionersAndBoxes);
            _prisioners = new List<Prisioner>(NumberOfPrisionersAndBoxes);
        }

        public async Task<bool> Run()
        {
            List<int> availableNumbers = Enumerable.Range(0, NumberOfPrisionersAndBoxes).ToList();

            var random = new Random();

            for (var i = 0; i < NumberOfPrisionersAndBoxes; i++)
            {
                _prisioners.Add(new Prisioner(i));

                var availablesIndex = random.Next(0, availableNumbers.Count);
                var priosionerNumber = availableNumbers[availablesIndex];
                _boxes.Add(i, new Box(i, priosionerNumber));
                availableNumbers.RemoveAt(availablesIndex);
            }

            var results = await Task.WhenAll(_prisioners.Select(prisioner => prisioner.Try(AttemptsPerPrisioner, _boxes)));
            var failed = results.Any(o => !o);
            
            return !failed;
        }

        public string PrintBoxes()
        {
            StringBuilder sb = new StringBuilder();
            var columns = 10;
            var rows = (NumberOfPrisionersAndBoxes / columns) + 1;
            foreach (var i in Enumerable.Range(0, rows))
            {
                sb.AppendLine($"| {String.Join(" | ", _boxes.Skip(i * columns).Take(columns).Select(box => $" {box.Value.Id:D2} {box.Value.PrisionerNumber:D2}"))} |");
            }

            return sb.ToString();
        }

        public string PrintPrisioners()
        {
            StringBuilder sb = new StringBuilder();
            var columns = 10;
            var rows = (NumberOfPrisionersAndBoxes / columns) + 1;
            foreach (var i in Enumerable.Range(0, rows))
            {
                sb.AppendLine($"| {String.Join(" | ", _prisioners.Skip(i * columns).Take(columns).Select(prisioner => $"{prisioner.Id}: Attempts: {prisioner.Attempts}, found at: {(prisioner.FoundAtBox.HasValue ? prisioner.FoundAtBox.Value.ToString() : "Failed")}"))} |");
            }

            return sb.ToString();
        }
    }
}
