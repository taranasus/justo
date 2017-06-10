using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepieScraper.Core
{
    public enum QuantityType
    {
        g=0,
        l=1,
        ml=5,
        Tbsp =2,
        Tsp=3,
        Unit =4
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public QuantityType MeasurmentType { get; set; }
        public float Quantity { get; set; }
    }
}
