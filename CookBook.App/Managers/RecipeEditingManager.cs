using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.App.Managers
{
    public class RecipeEditingManager : RecipePropertiesManager
    {
        private IService<Recipe> _recipeService;
        private TagManager _tagManager;

        public RecipeEditingManager(IService<Recipe> recipeService, IConsole console, TagManager tagManager) : base(console)
        {
            _recipeService = recipeService;
            _tagManager = tagManager;
        }
       
        public int EditRecipeView()
        {
            Console.WriteLine("Please enter id of recipe you want to edit or enter 'n' to go back:");
            var recipeid = IdHandling();
            return recipeid;
        }
        public void EditRecipe(Recipe editedRecipe)
        {
            if (editedRecipe is null)
            {
                return;
            }
            _recipeService.UpdateItem(editedRecipe);
        }
        public Recipe EditRecipeProperties(Recipe recipe)
        {
            if (recipe is null)
            {
                return null;
            }
            var editPropertyDecision = true;
            while (editPropertyDecision == true)
            {
                Console.WriteLine("Please enter property number that you want to change or enter 'n' to go back:");
                string propertyNumber = _console.ReadLine();
                switch (propertyNumber)
                {
                    case "1":
                        Console.WriteLine("You can,'t change the id");
                        break;
                    case "2":
                        Console.WriteLine($"Name: {recipe.Name}");
                        var newName = AddRecipeName();
                        recipe.Name = newName;
                        break;
                    case "3":
                        Console.WriteLine($"Preparation time: {recipe.PreparationTime}");
                        var newTime = AddRecipePreparationTime();
                        recipe.PreparationTime = newTime;
                        break;
                    case "4":
                        Console.WriteLine($"Difficulty level: {recipe.DifficultyLevel.ToString()}");
                        var difficultyLevel = AddRecipeDifficultyLevel();
                        recipe.DifficultyLevel = difficultyLevel;
                        break;
                    case "5":
                        Console.WriteLine($"Number of portions: {recipe.NumberOfPortions}");
                        var numberOfPortions = AddRecipeNumberOfPortions();
                        recipe.NumberOfPortions = numberOfPortions;
                        break;
                    case "6":
                        Console.WriteLine($"Description: \n{recipe.Description}");
                        var description = AddRecipeDescription();
                        recipe.Description = description;
                        break;
                    case "7":
                        Console.WriteLine($"Is favourite: {recipe.IsFavourite}");
                        Console.WriteLine("Do you want to change this? Please enter y/n:");
                        var decision = _console.ReadKeyChar();
                        if (decision == 'y')
                        {
                            recipe.IsFavourite = recipe.IsFavourite ? false : true;
                        }
                        break;
                    case "8":
                        Console.WriteLine($"Is recipe for today: {recipe.IsTodaysRecipe}");
                        Console.WriteLine("Do you want to change this? Please enter y/n:");
                        decision = _console.ReadKeyChar();
                        if (decision == 'y')
                        {
                            recipe.IsTodaysRecipe = recipe.IsTodaysRecipe ? false : true;
                        }
                        break;
                    case "9":
                        Console.WriteLine("Ingredients:");
                        for (int i = 0; i < recipe.Ingredients.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1}. Name: {recipe.Ingredients[i].Name}, Amount: {recipe.Ingredients[i].Amount}");
                        }
                        recipe = EditRecipeIngredients(recipe);
                        break;
                    case "10":
                        Console.WriteLine("Tags:");
                        foreach (var tag in recipe.Tags)
                        {
                            Console.Write($"\"{tag.Name}\", ");
                        }
                        Console.WriteLine();
                        recipe = _tagManager.EditRecipeTags(recipe);
                        break;
                    case "n":
                        editPropertyDecision = false;
                        break;
                    default:
                        Console.WriteLine("Entered number is invalid");
                        break;
                }
            }
            return recipe;
        }
        public Recipe EditRecipeIngredients(Recipe recipe)
        {
            var operationDecision = true;
            while (operationDecision == true)
            {
                Console.WriteLine("What would you like to do? Please enter 1 for add new, enter 2 to remove or enter n to go back");
                var decision = _console.ReadKeyChar();
                Console.WriteLine();
                switch (decision)
                {
                    case '1':
                        var ingredients = AddIngredients();
                        recipe.Ingredients.AddRange(ingredients);
                        break;
                    case '2':
                        Console.WriteLine("Please enter ingredient number:");
                        int ingredientNumber;
                        var enteredNumber = _console.ReadKeyChar();
                        Console.WriteLine();
                        Int32.TryParse(enteredNumber.ToString(), out ingredientNumber);
                        if (ingredientNumber > recipe.Ingredients.Count)
                        {
                            Console.WriteLine("Ingredient not found");
                            break;
                        }
                        var ingredientToRemove = recipe.Ingredients[ingredientNumber - 1];
                        recipe.Ingredients.Remove(ingredientToRemove);
                        break;
                    case 'n':
                        operationDecision = false;
                        break;
                    default:
                        Console.WriteLine("Wrong key");
                        break;
                }
            }
            return recipe;
        }

        public int RemoveRecipeView()
        {
            Console.WriteLine("Please enter id of recipe you want to remove or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public void RemoveRecipe(Recipe recipeToRemove)
        {
            if (recipeToRemove is null)
            {
                return;
            }
            _recipeService.RemoveItem(recipeToRemove);
        }
    }
}
