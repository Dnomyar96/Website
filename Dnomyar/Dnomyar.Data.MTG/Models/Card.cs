using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnomyar.Data.MTG.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Colors { get; set; }

        public string Rarity { get; set; }

        public string Set { get; set; }

        public string SetName { get; set; }

        public string Type { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string Loyalty { get; set; }

        public string ManaCost { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string Flavor { get; set; }
    }
}
