using CookBook.App.Managers;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CookBook
{
    public class TagsSeed
    {
        public static void Seed(TagManager tagManager)
        {
            string tagsJsonFile = File.ReadAllText(@"..\..\..\Jsons\Seeders\TagsSeed.json");

            var tagsFromJson = JsonConvert.DeserializeObject<List<Tag>>(tagsJsonFile);

            tagManager.TagsSeed(tagsFromJson);
        }
    }
}
