using System;
using System.Collections.Generic;
using System.Text;

namespace GardenPlotPlanner.Models
{
    public class Ground : IModel
    {
        public string? Type { get; set ; }
        public int[]? Size { get; set ; }
        public int[]? Coordinates { get; set; }
        public int? Level { get; set; }
        public int? LevelsCount { get; set; }
        public int? MaxLevel { get; set; }
        public bool? CanContain { get; set; }
        public bool? IsContained { get; set; }
    }
}
