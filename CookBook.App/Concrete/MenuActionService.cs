using CookBook.App.Common;
using CookBook.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }

        private void Initialize()
        {
            AddItem(new MenuAction(1, "Recipes for today", "Main"));
            AddItem(new MenuAction(2, "Recipes by tag", "Main"));
            AddItem(new MenuAction(3, "Favourite recipes", "Main"));
            AddItem(new MenuAction(4, "Add recipe", "Main"));
            AddItem(new MenuAction(5, "Remove recipe", "Main"));
            AddItem(new MenuAction(6, "Tags", "Main"));
            AddItem(new MenuAction(7, "Edit recipe", "Main"));
            AddItem(new MenuAction(8, "All recipes", "Main"));
            AddItem(new MenuAction(9, "Recipe to Json", "Main"));
        }
    }
}
