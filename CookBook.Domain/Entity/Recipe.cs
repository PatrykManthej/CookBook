using System;
using System.Collections.Generic;
using System.Text;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class Recipe : BaseEntity
    {
        public string PreparationTime { get; set; }
        public DifficultyLevels DifficultyLevel{ get; set; }
        public int NumberOfPortions { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsTodaysRecipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Tag> Tags { get; set; }

        public Recipe(int id, string name, string preparationTime, DifficultyLevels level, int portions, string description, bool favourite, bool todaysRecipe, List<Ingredient> ingredients, List<Tag> tags)
        {
            Id = id;
            Name = name;
            PreparationTime = preparationTime;
            DifficultyLevel = level;
            NumberOfPortions = portions;
            Description = description;
            IsFavourite = favourite;
            IsTodaysRecipe = todaysRecipe;
            Ingredients = ingredients;
            Tags = tags;
        }
        public Recipe()
        {

        }

    }
    public enum DifficultyLevels
    {
        Easy = 1,
        Medium,
        Hard,
        Unknown
    }
}
