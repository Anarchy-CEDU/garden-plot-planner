using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GardenPlotPlanner.Models
{
    public class Model
    {
        //Основыне данные
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? SpriteName { get; set; }

        //Размеры
        public int? Width { get; set; }
        public int? Length { get; set; }
        //--

        //Координаты
        public int? X { get; set; }
        public int? Y { get; set; }
        //--

        //Высота
        public int? SelfLevel { get; set; }
        public int? LevelsCount { get; set; }
        public int? MaxLevel { get; set; }

        //Родительская модель
        public string? ParentModel { get; set; } 
        //Может ли объект содержать другие
        public bool? CanContain {  get; set; }
        //Список вложенных моделей
        public List<string>? InnerModels { get; set; }


    }
}
