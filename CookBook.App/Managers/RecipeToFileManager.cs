using CookBook.App.Abstract;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CookBook.App.Managers
{
    public class RecipeToFileManager : BaseManager
    {
        public RecipeToFileManager(IConsole console) : base(console)
        {
        }
        public int RecipeToJsonIdView()
        {
            Console.WriteLine("Please enter recipe id which you want to save as json or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public int RecipeToXMLIdView()
        {
            Console.WriteLine("Please enter recipe id which you want to save as xml or enter 'n' to go back:");
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
        public void RecipeToXML(Recipe recipe)
        {

        }
    }
}
