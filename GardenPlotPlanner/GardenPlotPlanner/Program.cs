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

            ProjectController pc = new ProjectController();

            pc.CreateProject("newProject");

            //pc.ShowInfo();




            ModelController mc = new ModelController();

            mc.CreateModel("ground");
            mc.CreateModel("m2");
            mc.CreateModel("m3");
            mc.CreateModel("m4");
            mc.CreateModel("m5");
            mc.CreateModel("m6");


            mc.InsertInto("ground", new[] { "m2", "m3", "m4" });
            mc.InsertInto("m3", "m5");
            mc.InsertInto("m5", "m6");

            pc.LoadProject("newProject", mc);

            pc.ShowInfo("newProject");

            //mc.ShowTreeInfo("m3", "");

        }
    }
}