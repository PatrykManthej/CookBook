﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }
        List<T> GetAllItems();
        int AddItem(T item);
        int UpdateItem(T item);
        void RemoveItem(T item);
        int GetLastId();
        T GetItemById(int id);
    }
}
