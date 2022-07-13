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
        public static void Seed(RecipeManager recipeManager)
        {
            string tagsJsonFile = File.ReadAllText(@"..\..\..\Jsons\TagsSeed.json");
            string recipesJsonFile = File.ReadAllText(@"..\..\..\Jsons\RecipesSeed.json");

            var tagsFromJson = JsonConvert.DeserializeObject<List<Tag>>(tagsJsonFile);
            var recipesFromJson = JsonConvert.DeserializeObject<List<Recipe>>(recipesJsonFile);

            recipeManager.TagsSeed(tagsFromJson);
            recipeManager.RecipesSeed(recipesFromJson);
        }
    }
}
