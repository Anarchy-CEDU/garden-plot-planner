using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenPlotPlanner.Models;

namespace GardenPlotPlanner.Models
{
    public class Project
    {
        //Продумать реализацию
        //Как проект будет запоминать расположение объектов внутри себя
        //до момента импорта в JSON?
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Author { get; set; }

        //Словарь всех моделей, которые есть в проекте
        public Model? StartModel { get; set; }

        public void Info()
        {
            var prop = typeof(Project).GetProperties();

            for(int i = 0; i< prop.Length; i++)
            {
                Console.WriteLine($"{prop[i].Name} => {prop[i].GetValue(this)}");
            }
            Console.WriteLine("--------------------------");

        }
    }
}
