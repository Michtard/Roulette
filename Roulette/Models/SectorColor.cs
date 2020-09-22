using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Models
{
    class SectorColor
    {     
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public string Color { get; set; }
        
        public SectorColor(int startValue, int endValue, string color)
        {
            StartValue = startValue;
            EndValue = endValue;
            Color = color;
        }

        public SectorColor()
        {
        }
    }
}
