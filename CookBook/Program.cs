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
            TagManager tagManager = new TagManager(tagService, consoleWrapper);
            RecipeGettingManager recipeGettingManager = new RecipeGettingManager(recipeService, consoleWrapper);
            RecipeAddingManager recipeAddingManager = new RecipeAddingManager(recipeService, consoleWrapper, tagManager);
            RecipeEditingManager recipeEditingManager = new RecipeEditingManager(recipeService, consoleWrapper, tagManager);
            RecipeToFileManager recipeToFileManager = new RecipeToFileManager(consoleWrapper, actionService);
            
            Console.WriteLine("Welcome to Cookbook app!");
            RecipesSeed.Seed(recipeAddingManager);
            TagsSeed.Seed(tagManager);
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
                        var todaysRecipes = recipeGettingManager.RecipesForTodayList();
                        recipeGettingManager.ItemsListView(todaysRecipes);
                        var recipeToShowId = recipeGettingManager.RecipeToShowIdView();
                        var recipeToShow = recipeGettingManager.RecipeDetailsView(todaysRecipes, recipeToShowId);
                        recipeGettingManager.RecipeToConsole(recipeToShow);
                        break;
                    case '2':
                        var tag = tagManager.RecipesByTagNameView();
                        var recipesToShow = recipeGettingManager.RecipesByTagList(tag);
                        recipeGettingManager.ItemsListView(recipesToShow);
                        recipeToShowId = recipeGettingManager.RecipeToShowIdView();
                        recipeToShow = recipeGettingManager.RecipeDetailsView(recipesToShow, recipeToShowId);
                        recipeGettingManager.RecipeToConsole(recipeToShow);
                        break;
                    case '3':
                        var favouriteRecipes = recipeGettingManager.FavouriteRecipesList();
                        recipeGettingManager.ItemsListView(favouriteRecipes);
                        recipeToShowId = recipeGettingManager.RecipeToShowIdView();
                        recipeToShow = recipeGettingManager.RecipeDetailsView(favouriteRecipes, recipeToShowId);
                        recipeGettingManager.RecipeToConsole(recipeToShow);
                        break;
                    case '4':
                        var recipeToAdd = recipeAddingManager.AddNewRecipeView();
                        var createdRecipeId = recipeAddingManager.AddNewRecipe(recipeToAdd);
                        break;
                    case '5':
                        var removeId = recipeEditingManager.RemoveRecipeView();
                        var recipeToRemove = recipeGettingManager.GetRecipeById(removeId);
                        recipeEditingManager.RemoveRecipe(recipeToRemove);
                        break;
                    case '6':
                        var tags = tagManager.AllTags();
                        recipeGettingManager.ItemsListView(tags);
                        var tagName = tagManager.RecipesByTagIdView(tags);
                        var recipesByTagList = recipeGettingManager.RecipesByTagList(tagName);
                        recipeGettingManager.ItemsListView(recipesByTagList);
                        recipeToShowId = recipeGettingManager.RecipeToShowIdView();
                        recipeToShow = recipeGettingManager.RecipeDetailsView(recipesByTagList, recipeToShowId);
                        recipeGettingManager.RecipeToConsole(recipeToShow);
                        break;
                    case '7':
                        var recipes = recipeGettingManager.AllRecipes();
                        recipeGettingManager.ItemsListView(recipes);
                        var editId = recipeEditingManager.EditRecipeView();
                        var recipeToEdit = recipeGettingManager.GetRecipeById(editId);
                        recipeGettingManager.RecipeToConsole(recipeToEdit);
                        var editedRecipe = recipeEditingManager.EditRecipeProperties(recipeToEdit);
                        recipeEditingManager.EditRecipe(editedRecipe);
                        break;
                    case '8':
                        recipes = recipeGettingManager.AllRecipes();
                        recipeGettingManager.ItemsListView(recipes);
                        recipeToShowId = recipeGettingManager.RecipeToShowIdView();
                        recipeToShow = recipeGettingManager.RecipeDetailsView(recipes, recipeToShowId);
                        recipeGettingManager.RecipeToConsole(recipeToShow);
                        break;
                    case '9':
                        recipes = recipeGettingManager.AllRecipes();
                        recipeGettingManager.ItemsListView(recipes);
                        var recipeToFileId = recipeToFileManager.RecipeToFileIdView();
                        var recipeToFile = recipeGettingManager.GetRecipeById(recipeToFileId);
                        var fileFormatId = recipeToFileManager.RecipeToFileView(recipeToFile);
                        recipeToFileManager.FileFormatSelection(recipeToFile, fileFormatId);
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
