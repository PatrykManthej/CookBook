using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Common;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.App.Managers
{
    public class RecipeGettingManager : BaseManager
    {
        private IService<Recipe> _recipeService;
            
        public RecipeGettingManager(IService<Recipe> recipeService, IConsole console) : base(console)
        {
            _recipeService = recipeService;
        }

        
        public List<Recipe> RecipesForTodayList()
        {
            var recipes = _recipeService.GetAllItems();
            var todaysRecipes = recipes.Where(r => r.IsTodaysRecipe is true).ToList();
            return todaysRecipes;
        }
        public List<Recipe> FavouriteRecipesList()
        {
            var recipes = _recipeService.GetAllItems();
            var favouriteRecipes = recipes.Where(r => r.IsFavourite is true).ToList();
            return favouriteRecipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            if (recipeId == 0)
            {
                return null;
            }
            var recipe = _recipeService.GetItemById(recipeId);
            if (recipe is null)
            {
                Console.WriteLine("Recipe not found");
                return null;
            }
            return recipe;
        }
        public List<Recipe> AllRecipes()
        {
            var recipes = _recipeService.GetAllItems();
            return recipes;
        }

        public List<Recipe> RecipesByTagList(string recipeTag)
        {
            if(recipeTag is null)
            {
                Console.WriteLine("Tag not found");
                return null;
            }
            Console.WriteLine($"List of recipes with tag \"{recipeTag}\"");
            var recipes = _recipeService.GetAllItems();
            var recipesToShow = recipes.Where(r => r.Tags.Any(t => t.Name == recipeTag)).ToList();
            return recipesToShow;
        }


        public void ItemsListView(IEnumerable<BaseEntity> items)
        {
            if(items is null)
            {
                return;
            }
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}. Name: {item.Name}");
            }
        }
        public int RecipeToShowIdView()
        {
            Console.WriteLine("Please enter recipe id to show details or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public Recipe RecipeDetailsView(List<Recipe> recipes, int recipeId)
        {
            if(recipes is null)
            {
                return null;
            }
            if (recipeId == 0)
            {
                return null;
            }
            var recipeToShow = recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipeToShow is null)
            {
                Console.WriteLine("Recipe not found");
                return null;
            }
            return recipeToShow;
        }

        public void RecipeToConsole(Recipe recipe)
        {
            if(recipe is null)
            {
                return;
            }
            Console.WriteLine($"1. Id: {recipe.Id}");
            Console.WriteLine($"2. Name: {recipe.Name}");
            Console.WriteLine($"3. Preparation time: {recipe.PreparationTime}");
            Console.WriteLine($"4. Difficulty level: {recipe.DifficultyLevel.ToString()}");
            Console.WriteLine($"5. Number of portions: {recipe.NumberOfPortions}");
            Console.WriteLine($"6. Description: \n{recipe.Description}");
            Console.WriteLine($"7. Is favourite: {recipe.IsFavourite}");
            Console.WriteLine($"8. Is recipe for today: {recipe.IsTodaysRecipe}");
            Console.WriteLine("9. Ingredients:");
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}. Name: {recipe.Ingredients[i].Name}, Amount: {recipe.Ingredients[i].Amount}");
            }
            Console.WriteLine("10. Tags:");
            foreach (var tag in recipe.Tags)
            {
                Console.Write($"\"{tag.Name}\", ");
            }
            Console.WriteLine();
        }


    }
}
