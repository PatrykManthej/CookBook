using CookBook.App.Concrete;
using CookBook.App.Managers;
using System;

namespace CookBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            RecipeService recipeService = new RecipeService();
            TagService tagService = new TagService();
            ConsoleWrapper consoleWrapper = new ConsoleWrapper();
            RecipeManager recipeManager = new RecipeManager(actionService,recipeService,tagService, consoleWrapper);
            
            Console.WriteLine("Welcome to Cookbook app!");
            RecipesSeed.Seed(recipeManager);
            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                var mainMenu = actionService.GetMenuActionsByMenuName("Main");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}");
                }
                var operation = Console.ReadKey();
                Console.WriteLine();
                switch (operation.KeyChar)
                {
                    case '1':
                        var todaysRecipes = recipeManager.RecipesForTodayList();
                        recipeManager.ItemsListView(todaysRecipes);
                        var recipeToShowId = recipeManager.RecipeToShowIdView();
                        var recipeToShow = recipeManager.RecipeDetailsView(todaysRecipes, recipeToShowId);
                        recipeManager.RecipeToConsole(recipeToShow);
                        break;
                    case '2':
                        var tag = recipeManager.RecipesByTagNameView();
                        var recipesToShow = recipeManager.RecipesByTagList(tag);
                        recipeManager.ItemsListView(recipesToShow);
                        recipeToShowId = recipeManager.RecipeToShowIdView();
                        recipeToShow = recipeManager.RecipeDetailsView(recipesToShow, recipeToShowId);
                        recipeManager.RecipeToConsole(recipeToShow);
                        break;
                    case '3':
                        var favouriteRecipes = recipeManager.FavouriteRecipesList();
                        recipeManager.ItemsListView(favouriteRecipes);
                        recipeToShowId = recipeManager.RecipeToShowIdView();
                        recipeToShow = recipeManager.RecipeDetailsView(favouriteRecipes, recipeToShowId);
                        recipeManager.RecipeToConsole(recipeToShow);
                        break;
                    case '4':
                        var recipeToAdd = recipeManager.AddNewRecipeView();
                        var createdRecipeId = recipeManager.AddNewRecipe(recipeToAdd);
                        break;
                    case '5':
                        var removeId = recipeManager.RemoveRecipeView();
                        var recipeToRemove = recipeManager.GetRecipeById(removeId);
                        recipeManager.RemoveRecipe(recipeToRemove);
                        break;
                    case '6':
                        var tags = recipeManager.AllTags();
                        recipeManager.ItemsListView(tags);
                        var tagName = recipeManager.RecipesByTagIdView(tags);
                        var recipesByTagList = recipeManager.RecipesByTagList(tagName);
                        recipeManager.ItemsListView(recipesByTagList);
                        recipeToShowId = recipeManager.RecipeToShowIdView();
                        recipeToShow = recipeManager.RecipeDetailsView(recipesByTagList, recipeToShowId);
                        recipeManager.RecipeToConsole(recipeToShow);
                        break;
                    case '7':
                        var recipes = recipeManager.AllRecipes();
                        recipeManager.ItemsListView(recipes);
                        var editId = recipeManager.EditRecipeView();
                        var recipeToEdit = recipeManager.GetRecipeById(editId);
                        recipeManager.RecipeToConsole(recipeToEdit);
                        var editedRecipe = recipeManager.EditPropertiesMethod(recipeToEdit);
                        recipeManager.EditRecipe(editedRecipe);
                        break;
                    case '8':
                        recipes = recipeManager.AllRecipes();
                        recipeManager.ItemsListView(recipes);
                        recipeToShowId = recipeManager.RecipeToShowIdView();
                        recipeToShow = recipeManager.RecipeDetailsView(recipes, recipeToShowId);
                        recipeManager.RecipeToConsole(recipeToShow);
                        break;
                    default:
                        break;

                }
            }
        }

        
    }
}
