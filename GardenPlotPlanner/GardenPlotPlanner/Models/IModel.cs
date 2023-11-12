using System;
using System.Collections.Generic;
using System.Text;

namespace GardenPlotPlanner.Models
{
    public interface IModel
    {
        string? Type { get; set; }
        int[]? Size { get; set; }
        int[]? Coordinates { get; set; }
        int? Level { get; set; }
        int? LevelsCount { get; set; }
        int? MaxLevel { get; set; }
        bool? CanContain {  get; set; }
        bool? IsContained { get; set; }
    }
}
