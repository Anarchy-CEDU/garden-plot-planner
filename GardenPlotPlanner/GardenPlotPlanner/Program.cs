using GardenPlotPlanner.Models;
using GardenPlotPlanner.Services;
using System.Collections.Generic;
using System;
using GardenPlotPlanner.Views;
using System.Threading;
using System.Windows;

namespace GardenPlotPlanner
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new App());




            //Точка входа в приложение
            //Отосюда будет вызываться главное представление

            //ProjectController pc = new ProjectController();

            //pc.CreateProject("newProject");
            ////pc.CreateProject("newProject1");
            //pc.CreateModel("m1");
            //pc.CreateModel("m2");
            //pc.CreateModel("m3");
            //pc.CreateModel("m4");
            //pc.CreateModel("m5");
            //pc.CreateModel("m6");


            //pc.BindModels(new[] { "m2", "m3", "m4" });
            //pc.BindModels("m3", "m5");
            //pc.BindModels("m4", "m6");

            ////pc.ShowSelectedProject();
            ////pc.ShowProjectsList();
            //pc.SelectProject("newProject");
            ////pc.ShowSelectedProject();

            //pc.ShowInfo();

            //pc.UnbindModels("m4", "m6");

            ////pc.SelectProject("newProject1");
            //pc.ShowInfo();
            ////pc.DeleteProject();
            ////pc.ShowInfo();




        }
    }
}