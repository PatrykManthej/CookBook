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
    public class RecipeManager
    {
        private readonly MenuActionService _actionService;
        private IService<Recipe> _recipeService;
        private IService<Tag> _tagService;
        private IConsole _console
            ;
        public RecipeManager(MenuActionService actionService, IService<Recipe> recipeService, IService<Tag> tagService, IConsole console)
        {
            _actionService = actionService;
            _recipeService = recipeService;
            _tagService = tagService;
            _console = console;
        }

        public int AddNewRecipe()
        {
            var recipeName = AddRecipeName();
            var tags = AddTags();
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
            _recipeService.AddItem(recipe);
            Console.WriteLine();

            return recipe.Id;
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

        public int RemoveRecipeView()
        {
            Console.WriteLine("Please enter id of recipe you want to remove or enter 'n' to go back:");
            var recipeId = IdHandling();
            return recipeId;
        }
        public void RemoveRecipe(Recipe recipeToRemove)
        {
            if(recipeToRemove is null)
            {
                return;
            }
            _recipeService.RemoveItem(recipeToRemove);
        }

        public List<Tag> AllTags()
        {
            var tags = _tagService.GetAllItems();
            return tags;
        }
        public string RecipesByTagIdView(List<Tag> tags)
        {
            Console.WriteLine("Please enter tag id to show recipes or enter 'n' to go back:");
            var id = IdHandling();
            if (id == 0)
            {
                return null;
            }
            var selectedTag = tags.FirstOrDefault(t => t.Id == id);
            if (selectedTag is null)
            {
                return null;
            }
            return selectedTag.Name;
        }
        public string RecipesByTagNameView()
        {
            Console.WriteLine("Please enter tag for recipes you want to show:");
            var enteredTag = Console.ReadLine();
            return enteredTag.ToLower();
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
        public Recipe EditPropertiesMethod(Recipe recipe)
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
                        recipe = EditIngredientsMethod(recipe);
                        break;
                    case "10":
                        Console.WriteLine("Tags:");
                        foreach (var tag in recipe.Tags)
                        {
                            Console.Write($"\"{tag.Name}\", ");
                        }
                        Console.WriteLine();
                        recipe = EditTagsMethod(recipe);
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
        public Recipe EditIngredientsMethod(Recipe recipe)
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
        public Recipe EditTagsMethod(Recipe recipe)
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
                        var tags = AddTags();
                        recipe.Tags.AddRange(tags);
                        break;
                    case '2':
                        foreach (var tag in recipe.Tags)
                        {
                            Console.Write($"\"{tag.Id}. {tag.Name}\", ");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Please enter tag id:");
                        int tagId;
                        var enteredId = _console.ReadLine();
                        Int32.TryParse(enteredId, out tagId);
                        var tagToRemove = recipe.Tags.FirstOrDefault(t => t.Id == tagId);
                        recipe.Tags.Remove(tagToRemove);
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

        public int IdHandling()
        {
            int id = 0;
            var enteredIdIsValid = false;
            do
            {
                var enteredId = _console.ReadLine();
                if (enteredId == "n")
                {
                    return 0;
                }
                Int32.TryParse(enteredId, out id);
                if (id > 0)
                {
                    enteredIdIsValid = true;
                }
                else
                {
                    Console.WriteLine("Entered id is invalid. Please enter valid id or enter 'n' to go back:");
                }
            }
            while (enteredIdIsValid == false);
            return id;
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

        public void RecipesSeed(List<Recipe> recipes)
        {
            _recipeService.Items.AddRange(recipes);
        }
        public void TagsSeed(List<Tag> tags)
        {
            _tagService.Items.AddRange(tags);
        }

        public string AddRecipeDescription()
        {
            Console.WriteLine("Please enter description:");
            var description = _console.ReadLine();
            return description;
        }
        public int AddRecipeNumberOfPortions()
        {
            Console.WriteLine("Please enter number of portions:");
            var portions = _console.ReadLine();
            int numberOfPortions;
            Int32.TryParse(portions, out numberOfPortions);
            return numberOfPortions;
        }
        public DifficultyLevels AddRecipeDifficultyLevel()
        {
            Console.WriteLine("Please choose difficulty level:");
            foreach (var item in Enum.GetValues(typeof(DifficultyLevels)))
            {
                Console.WriteLine($"{(int)item}. {Enum.GetName(typeof(DifficultyLevels), item)}");
            }
            var difficulty = _console.ReadKeyChar();
            Console.WriteLine();
            DifficultyLevels level;
            Enum.TryParse(difficulty.ToString(), out level);
            if ((int)level != 1 && (int)level != 2 && (int)level != 3 && (int)level != 4)
            {
                level = DifficultyLevels.Unknown;
            }
            return level;
        }
        public string AddRecipePreparationTime()
        {
            Console.WriteLine("Please enter average preparation time:");
            var preparationTime = _console.ReadLine();
            return preparationTime;
        }
        public string AddRecipeName()
        {
            Console.WriteLine("Please enter name for recipe:");
            var name = _console.ReadLine();
            return name;
        }
        public List<Ingredient> AddIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            bool addIngredients = true;
            while (addIngredients == true)
            {
                Console.WriteLine("Please enter name for ingredient:");
                var ingredientName = _console.ReadLine();
                Console.WriteLine("Please enter amount of that ingredient:");
                var ingredientAmount = _console.ReadLine();
                Ingredient ingredient = new Ingredient(ingredientName, ingredientAmount);
                ingredients.Add(ingredient);
                Console.WriteLine("Do you want to add next ingredient? Please enter y/n:");
                var nextIngredientDecision = _console.ReadKeyChar();
                Console.WriteLine();
                if (nextIngredientDecision != 'y')
                {
                    addIngredients = false;
                }
            }
            return ingredients;
        }
        public List<Tag> AddTags()
        {
            var tags = _tagService.GetAllItems();
            List<Tag> tagstoreturn = new List<Tag>();
            bool decisionAddNewTag = false;
            do
            {
                Console.WriteLine("Please enter tag name:");
                var enteredTag = _console.ReadLine().ToLower();
                var tag = tags.FirstOrDefault(t=>t.Name == enteredTag);
                if (tag is null)
                {
                    var lastTagId = _tagService.GetLastId();
                    tag = new Tag(lastTagId + 1, enteredTag);
                    _tagService.AddItem(tag);
                }
                tagstoreturn.Add(tag);
                Console.WriteLine("Do you want to add more tags? Please enter y/n:");
                var enteredDecision = _console.ReadKeyChar();
                Console.WriteLine();
                if (enteredDecision != 'y')
                {
                    decisionAddNewTag = false;
                }
                else
                {
                    decisionAddNewTag = true;
                }
            }
            while (decisionAddNewTag == true);
            return tagstoreturn;
        }

    }
}
