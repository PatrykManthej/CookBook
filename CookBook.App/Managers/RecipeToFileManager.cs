using CookBook.App.Abstract;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CookBook.App.Managers
{
    public class RecipeToFileManager : BaseManager
    {
        public RecipeToFileManager(IConsole console) : base(console)
        {
        }
        public int RecipeToJsonIdView()
        {
            Console.WriteLine("Please enter recipe id which you want to save as json file or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public int RecipeToXmlIdView()
        {
            Console.WriteLine("Please enter recipe id which you want to save as xml file or enter 'n' to go back:");
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
