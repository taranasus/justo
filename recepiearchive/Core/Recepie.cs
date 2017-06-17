using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepieScraper.Core
{
    public class Recepie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RecepieUrl { get; set; }
        public string Thubmnail { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }
        public DateTimeOffset? DateAdded { get; set; }
        public Dictionary<string, Ingredient> Ingredients { get; set; }
        public int Calories { get; set; }
        public int PrepTime { get; set; }
        public string Cuisine { get; set; }        
        public int Rating { get; set; }
        public byte[] ThumbnailImageBytes { get; set; }
        public bool IgnoreProductCompletely { get; set; }
        public decimal Carbs { get; set; }
    }
}
