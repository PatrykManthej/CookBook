using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.App.Managers
{
    public class RecipePropertiesManager : BaseManager
    {
        public RecipePropertiesManager(IConsole console) : base(console)
        {

        }

        public string AddRecipeName()
        {
            Console.WriteLine("Please enter name for recipe:");
            var name = _console.ReadLine();
            return name;
        }
        public string AddRecipePreparationTime()
        {
            Console.WriteLine("Please enter average preparation time:");
            var preparationTime = _console.ReadLine();
            return preparationTime;
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
    }
}
