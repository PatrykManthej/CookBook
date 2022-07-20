using CookBook.App.Managers;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CookBook
{
    public class RecipesSeed
    {
        public static void Seed(RecipeAddingManager recipeAddingManager)
        {
            string recipesJsonFile = File.ReadAllText(@"..\..\..\Jsons\RecipesSeed.json");

            var recipesFromJson = JsonConvert.DeserializeObject<List<Recipe>>(recipesJsonFile);

            recipeAddingManager.RecipesSeed(recipesFromJson);
        }
    }
}
