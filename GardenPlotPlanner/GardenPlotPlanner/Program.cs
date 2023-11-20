using GardenPlotPlanner.Models;
using GardenPlotPlanner.Services;
using System.Collections.Generic;
using System;


namespace GardenPlotPlanner
{
    internal class Program
    {
        static void Main()
        {
            //Точка входа в приложение
            //Отосюда будет вызываться главное представление

            ModelController mc = new ModelController();

            mc.CreateModel("m1");
            mc.CreateModel("m2");
            mc.CreateModel("m3");
            mc.CreateModel("m4");
            mc.CreateModel("m5");
            mc.CreateModel("m6");


            mc.InsertInto("m1", new[] { "m2", "m3", "m4" });
            mc.InsertInto("m3", "m5");
            mc.InsertInto("m5", "m6");

            //mc.ShowInfo(new[] { "m4", "m5", "m6"});

            //mc.DeleteModel("m5");
    
            //mc.ShowInfo(new[] { "m4", "m5", "m6" });

            mc.ShowTreeInfo("m1", "");


            //mc.ShowInfo(new[] { "m1", "m2", "m3", "m4" });

        }
    }
}