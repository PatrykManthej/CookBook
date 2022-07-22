using CookBook.App.Abstract;
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
    public class TagManagerIntegrationTests
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

        [Fact]
        public void TagsSeed_WithProperListOfTags_CanAddListOfTags()
        {
            //Arrange
            IService<Tag> tagService = new TagService();
            var manager = new TagManager(tagService, new ConsoleWrapper());

            //Act
            manager.TagsSeed(tags);
            var seededTags = tagService.Items;

            //Assert
            bool areEqualLists = Enumerable.SequenceEqual(seededTags, tags);
            areEqualLists.Should().BeTrue();
        }
    }
}
