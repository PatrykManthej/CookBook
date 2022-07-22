using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CookBook.Tests.UnitTests
{
    public class RecipeToFileManagerUnitTests
    {
        private static List<MenuAction> menuActions = new List<MenuAction>()
            {
                new MenuAction(1, "Save as Json", "SaveToFileMenu"),
                new MenuAction(2, "Save as Xml", "SaveToFileMenu")
            };
        [Fact]
        public void RecipeToFileView_WithProperRecipeAndSelectedSpecificFormat_ReturnsValidFileFormatId()
        {
            //Arrange
            var recipe = new Recipe()
            {
                Id = 1,
                Name = "Roladki z mięsem mielonym i cukinią",
                PreparationTime = "50 min",
                DifficultyLevel = DifficultyLevels.Easy,
                NumberOfPortions = 8,
                Description = @"Krok 1
W garnku na rozgrzanym tłuszczu podsmaż mielone mięso. Smaż aż odparują wszystkie soki, dodaj wtedy pomidory z puszki oraz Knorr Naturalnie smaczne - doprawi on sos do smaku oraz sprawi, że sos uzyska odpowiednią gęstość. Wymieszaj i gotuj około 3-4 minuty.",
                IsFavourite = true,
                IsTodaysRecipe = true,
                Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 1, Name = "zielona cukinia", Amount = "3 sztuki"},
                       new Ingredient(){ Id = 2, Name = "mozarella", Amount = "100g"},
                       new Ingredient(){ Id = 3, Name = "tortilla", Amount = "1 opakowanie" }
                   },
                Tags = new List<Tag>() { new Tag(1, "tag") }
            };
            var mockConsole = new Mock<IConsole>();
            var mockMenuService = new Mock<MenuActionService>();
            mockMenuService.Setup(m => m.GetMenuActionsByMenuName(It.IsAny<string>())).Returns(menuActions);
            mockConsole.Setup(c => c.ReadLine()).Returns("1");

            var manager = new RecipeToFileManager(mockConsole.Object, mockMenuService.Object);
            //Act
            var fileformatId = manager.RecipeToFileView(recipe);
            //Assert
            fileformatId.Should().Be(1);
        }
        [Fact]
        public void RecipeToFileView_WithProperRecipeAndSelectedInvalidSpecificFormat_Returns0()
        {
            //Arrange
            var recipe = new Recipe()
            {
                Id = 1,
                Name = "Roladki z mięsem mielonym i cukinią",
                PreparationTime = "50 min",
                DifficultyLevel = DifficultyLevels.Easy,
                NumberOfPortions = 8,
                Description = @"Krok 1
W garnku na rozgrzanym tłuszczu podsmaż mielone mięso. Smaż aż odparują wszystkie soki, dodaj wtedy pomidory z puszki oraz Knorr Naturalnie smaczne - doprawi on sos do smaku oraz sprawi, że sos uzyska odpowiednią gęstość. Wymieszaj i gotuj około 3-4 minuty.",
                IsFavourite = true,
                IsTodaysRecipe = true,
                Ingredients = new List<Ingredient>()
                   {
                       new Ingredient(){ Id = 1, Name = "zielona cukinia", Amount = "3 sztuki"},
                       new Ingredient(){ Id = 2, Name = "mozarella", Amount = "100g"},
                       new Ingredient(){ Id = 3, Name = "tortilla", Amount = "1 opakowanie" }
                   },
                Tags = new List<Tag>() { new Tag(1, "tag") }
            };
            var mockConsole = new Mock<IConsole>();
            var mockMenuService = new Mock<MenuActionService>();
            mockMenuService.Setup(m => m.GetMenuActionsByMenuName(It.IsAny<string>())).Returns(menuActions);
            mockConsole.Setup(c => c.ReadLine()).Returns("3");

            var manager = new RecipeToFileManager(mockConsole.Object, mockMenuService.Object);
            //Act
            var fileformatId = manager.RecipeToFileView(recipe);
            //Assert
            fileformatId.Should().Be(0);
        }
        [Fact]
        public void RecipeToFileView_WithNullRecipeAndSelectedSpecificFormat_Returns0()
        {
            //Arrange
            Recipe recipe = null;
            var mockConsole = new Mock<IConsole>();
            var mockMenuService = new Mock<MenuActionService>();
            mockMenuService.Setup(m => m.GetMenuActionsByMenuName(It.IsAny<string>())).Returns(menuActions);
            mockConsole.Setup(c => c.ReadLine()).Returns("1");

            var manager = new RecipeToFileManager(mockConsole.Object, mockMenuService.Object);
            //Act
            var fileformatId = manager.RecipeToFileView(recipe);
            //Assert
            fileformatId.Should().Be(0);
        }
    }
}
