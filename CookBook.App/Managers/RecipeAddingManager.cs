using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.App.Managers
{
    public class RecipeAddingManager : RecipePropertiesManager
    {
        private IService<Recipe> _recipeService;
        private TagManager _tagManager;

        public RecipeAddingManager(IService<Recipe> recipeService, IConsole console, TagManager tagManager) : base(console)
        {
            _recipeService = recipeService;
            _tagManager = tagManager;
        }
        
        public Recipe AddNewRecipeView()
        {
            var recipeName = AddRecipeName();
            var tags = _tagManager.AddTags();
            var preparationTime = AddRecipePreparationTime();
            var level = AddRecipeDifficultyLevel();
            var numberOfPortions = AddRecipeNumberOfPortions();
            Console.WriteLine("Please add ingredients:");
            var ingredients = AddIngredients();
            var description = AddRecipeDescription();

            Console.WriteLine("Do you want to add recipe to favourite list? Please enter y/n:");
            var decision = _console.ReadKeyChar();
            Console.WriteLine();
            bool isFavouriteDecision = false;
            if (decision == 'y')
            {
                isFavouriteDecision = true;
            }

            Console.WriteLine("Do you want to add recipe to recipes for today? Please enter y/n:");
            decision = _console.ReadKeyChar();
            Console.WriteLine();
            bool isTodaysDecision = false;
            if (decision == 'y')
            {
                isTodaysDecision = true;
            }
            int lastId = _recipeService.GetLastId();
            Recipe recipe = new Recipe(lastId + 1, recipeName, preparationTime, level, numberOfPortions, description, isFavouriteDecision, isTodaysDecision, ingredients, tags);
            return recipe;
        }
        public int AddNewRecipe(Recipe recipe)
        {
            _recipeService.AddItem(recipe);
            Console.WriteLine();

            return recipe.Id;
        }

        public void RecipesSeed(List<Recipe> recipes)
        {
            _recipeService.Items.AddRange(recipes);
        }

    }
}
