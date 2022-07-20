using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.App.Managers
{
    public class TagManager : BaseManager
    {
        private IService<Tag> _tagService;

        public TagManager(IService<Tag> tagService, IConsole console) : base(console)
        {
            _tagService = tagService;
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
        public List<Tag> AddTags()
        {
            var tags = _tagService.GetAllItems();
            List<Tag> tagstoreturn = new List<Tag>();
            bool decisionAddNewTag = false;
            do
            {
                Console.WriteLine("Please enter tag name:");
                var enteredTag = _console.ReadLine().ToLower();
                var tag = tags.FirstOrDefault(t => t.Name == enteredTag);
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
        public Recipe EditRecipeTags(Recipe recipe)
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

        public void TagsSeed(List<Tag> tags)
        {
            _tagService.Items.AddRange(tags);
        }
    }
}
