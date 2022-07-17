﻿using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CookBook.Tests.IntegrationTests
{
    public class RecipeManagerIntegrationTests
    {
        private static List<Tag> tags = new List<Tag>()
            {
                new Tag(1, "obiad"),
                new Tag(2, "dania z piekarnika"),
                new Tag(3, "cebula"),
                new Tag(4, "kotlet"),
                new Tag(5, "drób"),
                new Tag(6, "sałatki"),
                new Tag(7, "ciasto"),
                new Tag(8, "wege"),
                new Tag(9, "serniki"),
            };
        private static List<Recipe> recipes = new List<Recipe>()
           {

               new Recipe(){ Id = 1, Name = "Roladki z mięsem mielonym i cukinią", PreparationTime = "50 min", DifficultyLevel = DifficultyLevels.Easy, NumberOfPortions = 8, Description = @"Krok 1
W garnku na rozgrzanym tłuszczu podsmaż mielone mięso. Smaż aż odparują wszystkie soki, dodaj wtedy pomidory z puszki oraz Knorr Naturalnie smaczne - doprawi on sos do smaku oraz sprawi, że sos uzyska odpowiednią gęstość. Wymieszaj i gotuj około 3-4 minuty.", IsFavourite = true, IsTodaysRecipe = true, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 1, Name = "zielona cukinia", Amount = "3 sztuki"},
                       new Ingredient(){ Id = 2, Name = "mozarella", Amount = "100g"},
                       new Ingredient(){ Id = 3, Name = "tortilla", Amount = "1 opakowanie" }
                   },
                   Tags = new List<Tag>(){tags[0], tags[1]} },

               new Recipe(){ Id = 2, Name = "Kotlety z kalafiora i kaszy jaglanej", PreparationTime = "45 min", DifficultyLevel = DifficultyLevels.Easy, NumberOfPortions = 4, Description = @"Krok 1
Kasze jaglaną ugotuj w 200 ml wody,100 ml mleka z kostką rosołową do miękkości. Kasza może być lekko rozgotowania.", IsFavourite = true, IsTodaysRecipe = true, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 4, Name = "kasza jaglana", Amount = "100g" },
                       new Ingredient(){ Id = 5, Name = "mleko", Amount = "100ml" },
                       new Ingredient(){ Id = 6, Name = "cebula", Amount = "1 sztuka" }
                   },
                   Tags = new List<Tag>(){tags[2], tags[3], tags[0]} },

               new Recipe(){ Id = 3, Name = "Krem z brokułów", PreparationTime = "30 min", DifficultyLevel = DifficultyLevels.Easy, NumberOfPortions = 4, Description = @"Krok 1
Rozpuść kostki rosołowe Rosół z kury z pietruszką i lubczykiem Knorr w 1 litrze wrzątku.", IsFavourite = true, IsTodaysRecipe = false, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 7, Name = "cebula", Amount = "2 sztuki"},
                       new Ingredient(){ Id = 8, Name = "brokuły", Amount = "500g"},
                       new Ingredient(){ Id = 9, Name = "olej", Amount = "1 łyżka"}
                   },
                   Tags = new List<Tag>(){tags[2], tags[0]} },

               new Recipe(){ Id = 4, Name = "Sałatka ze szpinakiem i kurczakiem", PreparationTime = "35 min", DifficultyLevel = DifficultyLevels.Medium, NumberOfPortions = 4, Description = @"Krok 1
Ugotuj torebkę komosy ryżowej, według przepisu producenta. Po ugotowaniu odstaw na bok aby kasza odciekła z wody i się schłodziła.", IsFavourite = true, IsTodaysRecipe = false, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 10, Name = "komosa ryżowa", Amount = "100g"},
                       new Ingredient(){ Id = 11, Name = "szpinak", Amount = "100g"},
                       new Ingredient(){ Id = 12, Name = "ser feta", Amount = "30g"}
                   },
                   Tags = new List<Tag>(){tags[4], tags[5]} },

               new Recipe(){ Id = 5, Name = "Tiramisu", PreparationTime = "60 min", DifficultyLevel = DifficultyLevels.Hard, NumberOfPortions = 5, Description = @"Krok 1
Białka oddzielamy od żółtek. Żółtka ucieramy z cukrem na puszysty krem. W kilku turach dodajemy schłodzony serek mascarpone nadal ubijając. Wlewamy kieliszek likieru Amaretto i ponownie mieszamy.", IsFavourite = false, IsTodaysRecipe = true, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 13, Name = "serek mascarpone", Amount = "750g"},
                       new Ingredient(){ Id = 14, Name = "jajka", Amount = "4 sztuki"},
                       new Ingredient(){ Id = 15, Name = "biszkopty", Amount = "800g"}
                   },
                   Tags = new List<Tag>(){tags[6], tags[7]} },

               new Recipe(){ Id = 6, Name = "Sernik z krówkami", PreparationTime = "30 min", DifficultyLevel = DifficultyLevels.Easy, NumberOfPortions = 4, Description = @"Krok 1
Formę do pieczenia smarujemy masłem. Zarówno dno formy jaki i brzegi wykładamy papierem do pieczenia. Ciasteczka drobno kruszymy. Masło roztapiamy i łączymy z rozdrobnionymi ciasteczkami. Całość dokładnie mieszamy.", IsFavourite = false, IsTodaysRecipe = false, Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){Id = 16, Name = "ciastka owsiane", Amount = "170g"},
                       new Ingredient(){Id = 17, Name = "twaróg", Amount = "700g"},
                       new Ingredient(){Id = 18, Name = "jajka", Amount = "3 sztuki"}
                   },
                   Tags = new List<Tag>(){tags[6], tags[7], tags[8]} }
           };
        [Fact]
        public void RecipesForTodayList_ReturnsListOfRecipesWhichPropertyIsTodaysRecipeIsTrue()
        {
            //Arrange
            IService<Recipe> recipeService = new RecipeService();
            IService<Tag> tagService = new TagService();
            tagService.Items.AddRange(tags);
            recipeService.Items.AddRange(recipes);
            var manager = new RecipeManager(new MenuActionService(), recipeService, tagService, new ConsoleWrapper());
            var allRecipes = recipeService.GetAllItems();
            var recipesForToday = allRecipes.Where(r=>r.IsTodaysRecipe is true).ToList();
            //Act
            var returnedRecipes = manager.RecipesForTodayList();
            bool areEqualLists = Enumerable.SequenceEqual(recipesForToday, returnedRecipes);

            //Assert
            returnedRecipes.Should().BeOfType(typeof(List<Recipe>));
            returnedRecipes.Should().NotBeNull();
            areEqualLists.Should().BeTrue();
        }
        [Fact]
        public void FavouriteRecipesList_ReturnsListOfRecipesWhichPropertyIsFavouriteIsTrue()
        {
            //Arrange
            IService<Recipe> recipeService = new RecipeService();
            IService<Tag> tagService = new TagService();
            tagService.Items.AddRange(tags);
            recipeService.Items.AddRange(recipes);
            var manager = new RecipeManager(new MenuActionService(), recipeService, tagService, new ConsoleWrapper());
            var allRecipes = recipeService.GetAllItems();
            var favouriteRecipes = allRecipes.Where(r => r.IsFavourite is true).ToList();
            //Act
            var returnedRecipes = manager.FavouriteRecipesList();
            bool areEqualLists = Enumerable.SequenceEqual(favouriteRecipes, returnedRecipes);

            //Assert
            returnedRecipes.Should().BeOfType(typeof(List<Recipe>));
            returnedRecipes.Should().NotBeNull();
            areEqualLists.Should().BeTrue();
        }
        [Fact]
        public void RemoveRecipeById_WithProperId_CanDeleteRecipe()
        {
            //Arrange
            Recipe recipe = new Recipe()
            {
                Id = 7,
                Name = "Falafel",
                PreparationTime = "30 min",
                DifficultyLevel = DifficultyLevels.Medium,
                NumberOfPortions = 2,
                Description = @"Krok 1 W garnku na rozgrzanym tłuszczu podsmaż falafelka.",
                IsFavourite = true,
                IsTodaysRecipe = true,
                Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 19, Name = "ciecierzyca", Amount = "400g"},
                       new Ingredient(){ Id = 20, Name = "czosnek", Amount = "50g"},
                       new Ingredient(){ Id = 21, Name = "ogórek", Amount = "1 sztuka" }
                   },
                Tags = new List<Tag>() { new Tag(10,"pita"), new Tag(11,"izrael") }
            };
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddItem(recipe);
            var manager = new RecipeManager(new MenuActionService(), recipeService, new TagService(), new ConsoleWrapper());
            //Act
            //manager.RemoveRecipeById(recipe.Id);
            //Assert
            recipeService.GetItemById(recipe.Id).Should().BeNull();
        }
        [Fact]
        public void RecipesByTagList_WithProperTagName_ReturnsListOfRecipes()
        {
            //Arrange
            IService<Recipe> recipeService = new RecipeService();
            IService<Tag> tagService = new TagService();
            tagService.Items.AddRange(tags);
            recipeService.Items.AddRange(recipes);
            var tag = "obiad";
            var manager = new RecipeManager(new MenuActionService(), recipeService,tagService, new ConsoleWrapper());
            var allRecipes = recipeService.GetAllItems();
            var recipesWithTag = allRecipes.Where(r=>r.Tags.Any(t=>t.Name == tag)).ToList();
            //Act
            var returnedRecipes = manager.RecipesByTagList(tag);
            bool areEqualLists = Enumerable.SequenceEqual(recipesWithTag, returnedRecipes);
            //Assert
            returnedRecipes.Should().NotBeNull();
            returnedRecipes.Should().BeOfType(typeof(List<Recipe>));
            areEqualLists.Should().BeTrue();
        }
        [Fact]
        public void EditRecipeById_WithProperId_CanEditRecipe()
        {

        }
    }
}
