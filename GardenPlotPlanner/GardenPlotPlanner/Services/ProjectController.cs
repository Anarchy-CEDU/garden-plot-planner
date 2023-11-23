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
        public Project SelectedProject { get; set; }
        private ModelController SPModelController { get; set; }
        public ProjectController()
        {
            Projects = new SortedDictionary<string, Project>();
            SelectedProject = new Project();
            SPModelController = new ModelController();
        }



        //Создать проект
        public bool CreateProject(string projectName)
        {
            if (!Projects.ContainsKey(projectName))
            {
                Project project = new Project()
                {
                    Name = projectName,
                    Description = "New project",
                    DateCreated = DateTime.Now,
                    Author = "Guest",
                    ModelController = new ModelController()
                };

                SelectedProject = project;
                SPModelController = project.ModelController;
                SPModelController.CreateModel("ground");


                Projects.Add(projectName, project);


                return true;
            } else
            {
                return false;
            }

            
        }
        //Выбрать проект
        public string SelectProject(string projectName)
        {
            Project project = FindProject(projectName);
            if (project != null)
            {
                SelectedProject = project;
                SPModelController = project.ModelController;
                return project.Name;
            } else
            {
                return string.Empty;
            }
        }

        //Создать модель в проекте
        public string CreateModel(string modelName)
        {
            return SPModelController.CreateModel(modelName);
        }

        //Связать модели в проекте
        public string BindModels(string slaveName)
        {
            return SPModelController.InsertInto(slaveName);
        }
        public string BindModels(string masterName, string slaveName)
        {
            return SPModelController.InsertInto(masterName, slaveName);
        }
        public List<string> BindModels(string[] slavesNames)
        {
            return SPModelController.InsertInto(slavesNames);
        }

        //Удалить связь между моделями
        public string UnbindModels(string slaveName)
        {
            return SPModelController.ExstractFrom("ground",slaveName);
        }
        public string UnbindModels(string masterName, string slaveName)
        {
            return SPModelController.ExstractFrom(masterName, slaveName);
        }
        public List<string> UnbindModels(string masterName, string[] slavesNames)
        {
            return SPModelController.ExstractFrom(masterName, slavesNames);
        }

        //Найти проект в базе проектов
        private Project FindProject(string name)
        {
            return(Projects.ContainsKey(name)) ? Projects[name] : null;
        }

        //Показать информацию о проекте
        public void ShowInfo()
        {

            if (SelectedProject != null)
            {
                var prop = typeof(Project).GetProperties();

                for (int i = 0; i < prop.Length; i++)
                {
                    if (prop[i].Name == "ModelController")
                    {
                        Console.WriteLine($"{prop[i].Name} => -----------");
                        SPModelController.ShowTreeInfo("ground", null);

                    }
                    else
                    {
                        Console.WriteLine($"{prop[i].Name} => {prop[i].GetValue(SelectedProject)}");
                    }
                }
            }
            Console.WriteLine("--------------------------");
        }
        public void ShowSelectedProject()
        {
            Console.WriteLine($"Текущий проект => {SelectedProject.Name}");
            Console.WriteLine("--------------------------");
        }
        public void ShowProjectsList()
        {
            Console.WriteLine("Список созданных проектов:");
            foreach(string pName in Projects.Keys)
            {
                Console.WriteLine($"{pName}");
            }
            Console.WriteLine("--------------------------");
        }

        //Сохранить проект в файл
        public string SaveProject(string projectName)
        {
            string filename = string.Empty;

            //Реализовать алгоритм записи в файл
            try
            {
                //Находим проект по имени

                //Записываем данные проекта

                return filename;
            }
            catch
            {
                return "Error";
            }
        }

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
        public bool LoadProject(string[] properties)
        {
            //Реализовать алгоритм загрузки из файла

            //Создаём новый проект из распарсенных данных
            //Смотрим на имя получившегося объекта

            try
            {
                if (true)//Если проект с таким именем уже существует
                {
                    //Удалить существующий из списка
                    //Добавить новый под тем же именем
                }
                else //Если проекта с таким именем нет
                {
                    //Добавить в список новый проект
                }

                return true;
            } catch
            {
                return false;
            }
        }

        //Удалить проект
        public Project DeleteProject()
        {
            Project project = SelectedProject;

            try
            {
                Projects.Remove(project.Name);
                SelectedProject = null;
                SPModelController = null;
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
