using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class Recipe : BaseEntity
    {
        [XmlElement]
        public string PreparationTime { get; set; }
        [XmlElement]
        public DifficultyLevels DifficultyLevel{ get; set; }
        [XmlElement]
        public int NumberOfPortions { get; set; }
        [XmlElement]
        public string Description { get; set; }
        [XmlElement]
        public bool IsFavourite { get; set; }
        [XmlElement]
        public bool IsTodaysRecipe { get; set; }
        [XmlElement]
        public List<Ingredient> Ingredients { get; set; }
        [XmlElement]
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
