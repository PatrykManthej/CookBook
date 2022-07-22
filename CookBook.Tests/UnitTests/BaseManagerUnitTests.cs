using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CookBook.Tests.UnitTests
{
    public class BaseManagerUnitTests
    {
        [Fact]
        public void IdHandling_WithProperId_ReturnsValidParsedId()
        {
            //Arrange
            int enteredId = 5;
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(s => s.ReadLine()).Returns(enteredId.ToString());
            var manager = new BaseManager(mockConsole.Object);
            //Act
            var id = manager.IdHandling();
            //Assert
            id.Should().Be(enteredId);
            id.Should().BeOfType(typeof(int));
        }
        [Fact]
        public void IdHandling_WithEnteredLetterN_Returns0()
        {
            //Arrange
            string enteredString = "n";
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(s => s.ReadLine()).Returns(enteredString);
            var manager = new BaseManager(mockConsole.Object);
            //Act
            var id = manager.IdHandling();
            //Assert
            id.Should().Be(0);
            id.Should().BeOfType(typeof(int));
        }
    }
}
