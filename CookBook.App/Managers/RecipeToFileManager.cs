using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CookBook.App.Managers
{
    public class RecipeToFileManager : BaseManager
    {
        private MenuActionService _menuActionService;
        public RecipeToFileManager(IConsole console, MenuActionService menuActionService) : base(console)
        {
            _menuActionService = menuActionService;
        }

        public int RecipeToFileView(Recipe recipe)
        {
            if(recipe is null)
            {
                return 0;
            }

            var recipeToFileMenu = _menuActionService.GetMenuActionsByMenuName("SaveToFileMenu");
            Console.WriteLine("Please select file format:");
            for (int i = 0; i < recipeToFileMenu.Count; i++)
            {
                Console.WriteLine($"{recipeToFileMenu[i].Id}. {recipeToFileMenu[i].Name}");
            }
            var fileFormatId = IdHandling();

            var fileFormat = recipeToFileMenu.FirstOrDefault(f => f.Id == fileFormatId);
            if (fileFormat is null)
            {
                Console.WriteLine("Action not found");
                return 0;
            }

            return fileFormatId;
        }
        public void FileFormatSelection(Recipe recipe, int fileFormatId)
        {
            if(recipe is null)
            {
                return;
            }
            switch (fileFormatId)
            {
                case 1:
                    RecipeToJson(recipe);
                    break;
                case 2:
                    RecipeToXml(recipe);
                    break;
                default:
                    break;
            }
        }
        public int RecipeToFileIdView()
        {
            Console.WriteLine("Please enter recipe id which you want to save as file or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public void RecipeToJson(Recipe recipe)
        {
            var recipeName = recipe.Name;
            var jsonFileName = $"RecipeJson[{recipeName}].json";
            var jsonPath = $@"..\..\..\Jsons\{jsonFileName}";
            using StreamWriter sw = new StreamWriter(jsonPath);
            using JsonWriter writer = new JsonTextWriter(sw);

            var path = Path.GetFullPath(jsonPath);
            var directory = Path.GetDirectoryName(path);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, recipe);
            Console.WriteLine($"Recipe was saved as {jsonFileName} in {directory}");
        }
        public void RecipeToXml(Recipe recipe)
        {
            var recipeName = recipe.Name;
            var xmlFileName = $"RecipeXml[{recipeName}].xml";
            var xmlPath = $@"..\..\..\Xmls\{xmlFileName}";

            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "Recipe";
            root.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Recipe), root);

            var path = Path.GetFullPath(xmlPath);
            var directory = Path.GetDirectoryName(path);

            using StreamWriter sw = new StreamWriter(xmlPath);
            xmlSerializer.Serialize(sw, recipe);
            Console.WriteLine($"Recipe was saved as {xmlFileName} in {directory}");
        }
    }
}
