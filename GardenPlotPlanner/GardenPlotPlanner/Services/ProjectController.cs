using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GardenPlotPlanner.Models;
using GardenPlotPlanner.Services;

namespace GardenPlotPlanner.Services
{
    public class ProjectController
    {
        public SortedDictionary<string, Project> Projects { get; }

        public ProjectController()
        {
            Projects = new SortedDictionary<string, Project>();
        }

        //Создать проект
        public bool CreateProject(string projectName)
        {
            if (!Projects.ContainsKey(projectName))
            {
                Project project = new Project()
                {
                    Name = projectName,
                    Description = "New ptoject",
                    DateCreated = DateTime.Now,
                    Author = "Guest",
                    ModelController = new ModelController()
                };

                Projects.Add(projectName, project);
                return true;
            } else
            {
                return false;
            }

            
        }

        //Найти проект в базе проектов
        private Project FindProject(string name)
        {
            return(Projects.ContainsKey(name)) ? Projects[name] : null;
        }

        //Показать информацию о проекте
        public void ShowInfo(string projectName)
        {
            Project project = FindProject(projectName);

            if (project != null)
            {
                var prop = typeof(Project).GetProperties();

                for (int i = 0; i < prop.Length; i++)
                {
                    if (prop[i].Name == "ModelController")
                    {
                        Console.WriteLine($"{prop[i].Name} => -----------");
                        project.ModelController.ShowTreeInfo("ground", null);

                    }
                    else
                    {
                        Console.WriteLine($"{prop[i].Name} => {prop[i].GetValue(project)}");
                    }
                }
            }
            Console.WriteLine("--------------------------");
        }

        //Сохранить проект в файл

        //Загрузить проект из файла
        public bool LoadProject(string projectName, ModelController models)
        {
            try
            {
                Project project = FindProject(projectName);
                Projects.Remove(projectName);
                project.ModelController = models;
                Projects.Add(projectName, project);
                return true; 
                   
            } catch
            {
                return false;
            }
        }

        //Удалить проект
        public Project DeleteProject(string projectName)
        {
            Project project = FindProject(projectName);

            try
            {
                Projects.Remove(project.Name);
                return project;
            }
            catch
            {
                return null;
            }
        }

        //Загрузить базу знаний

        //Сохранить базу знаний

        //Удалить базу знаний

        //Подключить модуль с объектами

        //Удалить модуль с объектами





























    }
}
