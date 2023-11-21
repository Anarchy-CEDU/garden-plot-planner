using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenPlotPlanner.Models;

namespace GardenPlotPlanner.Services
{
    public class ModelController
    {
        public SortedDictionary<string, Model> Models { get; }

        public ModelController()
        {
            Models = new SortedDictionary<string, Model>();
        }

        //Показать дерево моделей
        public void ShowTreeInfo(string startNode, string? gap = "")
        {
            Model node = FindModel(startNode);
            Console.WriteLine(gap + node.Name);
            try
            {
                if (node.InnerModels.Count > 0)
                {
                    gap += "-";
                    foreach (string innerNode in node.InnerModels)
                    {
                        ShowTreeInfo(innerNode, gap);
                    }
                }
            } catch
            {
                Console.WriteLine($"Не удалось найти узел {startNode}");
            }
            

        }

        //Показать информациою о выбранной модели
        public void ShowInfo(string modelName)
        {
            Model model = FindModel(modelName);

            if (model != null)
            {
                var prop = typeof(Model).GetProperties();

                for (int i = 0; i < prop.Length; i++)
                {
                    if (prop[i].Name == "InnerModels" && (prop[i].GetValue(model) != null))
                    {
                        Console.WriteLine($"{prop[i].Name} => -----------");
                        var innerModels = (List<string>)prop[i].GetValue(model);

                        foreach (string mod in innerModels)
                        {
                            Console.WriteLine($"\t{mod}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{prop[i].Name} => {prop[i].GetValue(model)}");
                    }

                }
            }
            else
            {
                Console.WriteLine($"{modelName} => Не существует!");
            }
            Console.WriteLine("--------------------------");


        }
        public void ShowInfo(string[] modelsName)
        {
            foreach (string model in modelsName)
            {
                ShowInfo(model);
            }
        }

        //Создать модель
        public string CreateModel(string[]? properties)
        {
            Model model;
            if (properties == null || properties.Length != typeof(Model).GetProperties().Length)
            {
                model = new()
                {
                    Type = "small_house",
                    Name = "smallHouse*",
                    SpriteName = "small_house_01",
                    Width = 25,
                    Length = 50,
                    X = 0,
                    Y = 0,
                    SelfLevel = 1,
                    LevelsCount = 1,
                    MaxLevel = 1,
                    ParentModel = string.Empty,
                    CanContain = false,
                    InnerModels = null,

                };
            }
            else
            {
                model = new()
                {
                    Type = properties[0],
                    Name = properties[1],
                    SpriteName = properties[2],
                    Width = int.Parse(properties[3]),
                    Length = int.Parse(properties[4]),
                    X = int.Parse(properties[5]),
                    Y = int.Parse(properties[6]),
                    SelfLevel = int.Parse(properties[7]),
                    LevelsCount = int.Parse(properties[8]),
                    MaxLevel = int.Parse(properties[9]),
                    ParentModel = null,
                    CanContain = bool.Parse(properties[11]),
                    InnerModels = null, //!Настроитъ парсер списка вложенных моделей

                };
            }

            Models.Add(model.Name, model);

            return model.Name;
        }
        public string CreateModel(string modelName)
        {
            Model model = new Model()
            {
                Type = (Models.Count == 0) ? "master-model" : "small_house",
                Name = modelName,
                SpriteName = "small_house_01",
                Width = 25,
                Length = 50,
                X = 0,
                Y = 0,
                SelfLevel = 1,
                LevelsCount = 1,
                MaxLevel = 1,
                ParentModel = string.Empty,
                CanContain = true,
                InnerModels = new List<string>(),
            };

            Models.Add(model.Name, model);
            return model.Name;
        }

        //Найти модель
        private Model FindModel(string name)
        {
            return (Models.ContainsKey(name)) ? Models[name] : null;
        }

        //Обновить модель

        //Удалить модель
        public List<Model> DeleteModel(string modelName, bool fullDelete = false)
        {
            List<Model> result = new List<Model>();
            Model model = FindModel(modelName);
            Model deletedModel = model;

            try
            {
                if (fullDelete)
                {
                    //Как полностью удалить ветку?
                }
                else
                {
                    ExstractFrom(model.ParentModel, model.Name);
                    result.Add(deletedModel);
                    Models.Remove(model.Name);
                }
                return result;
            }
            catch
            {
                return null;
            }

            

        }

        //Внедрить модель
        public string InsertInto(string masterName, string slaveName)
        {
            Model master = FindModel(masterName);
            Model slave = FindModel(slaveName);

            if (slave != null || master != null)
            {
                try
                {
                    if (!master.InnerModels.Contains(slave.Name))
                    {
                        master.InnerModels.Add(slave.Name);
                        slave.ParentModel = masterName;
                    }
                }
                catch
                {
                    return null;
                }
            }

            return slaveName;
        }
        public List<string> InsertInto(string masterName, string[] slavesNames)
        {
            List<string> result = new List<string>();

            foreach (string slaveName in slavesNames)
            {
                result.Add(InsertInto(masterName, slaveName));
            }

            return result;
        }
        private List<string> InsertInto(string masterName, List<string> slavesNames)
        {
            List<string> result = new List<string>();

            foreach (string slaveName in slavesNames)
            {
                result.Add(InsertInto(masterName, slaveName));
            }

            return result;
        }

        //Изъять модель
        public string ExstractFrom(string masterName, string slaveName)
        {
            Model master = FindModel(masterName);
            Model slave = FindModel(slaveName);

            try
            {
                if (master.InnerModels.Contains(slaveName))
                {
                    master.InnerModels.Remove(slaveName);
                    InsertInto(slave.ParentModel, slave.InnerModels);
                }
                return slaveName;
            }
            catch
            {
                return null;
            }
        }
        public List<string> ExstractFrom(string masterName, string[] slavesNames)
        {
            List<string> exstractModels = new List<string>();

            foreach (string slaveName in slavesNames)
            {
                exstractModels.Add(ExstractFrom(masterName, slaveName));
            }

            return exstractModels;
        }
        public List<string> ExstractFrom(string masterName, List<string> slavesNames)
        {
            List<string> exstractModels = new List<string>();

            foreach (string slaveName in slavesNames)
            {
                exstractModels.Add(ExstractFrom(masterName, slaveName));
            }

            return exstractModels;
        }

    }
}
