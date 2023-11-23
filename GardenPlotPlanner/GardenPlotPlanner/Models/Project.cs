using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenPlotPlanner.Models;
using GardenPlotPlanner.Services;

namespace GardenPlotPlanner.Models
{
    public class Project
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Author { get; set; }

        //Контроллер моделей проекта
        public ModelController? ModelController { get; set; }

    }
}
