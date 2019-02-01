using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System.Collections.Generic;
using System.Linq;

namespace Abide.TagEditor.Wpf.Model
{
    public static class TagProvider
    {
        public static List<Item> GetItems(this MapFile map)
        {
            //Prepare
            Dictionary<string, Item> items = new Dictionary<string, Item>();
            Dictionary<string, Item> currentItem = null;
            DirectoryItem directoryItem = null;
            TagItem tagItem = null;
            string fullPath = null;
            string[] parts = null;

            //Loop
            foreach (var entry in map.IndexEntries)
            {
                //Setup
                fullPath = $"{entry.Filename}.{entry.Root}";
                currentItem = items;

                //Split and loop
                parts = fullPath.Split('\\');
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    //Create
                    if(!currentItem.ContainsKey(parts[i]))
                    {
                        directoryItem = new DirectoryItem() { Path = string.Join(@"\", parts, 0, i + 1), Name = parts[i] };
                        currentItem.Add(parts[i], directoryItem);
                    }

                    //Get
                    currentItem = ((DirectoryItem)currentItem[parts[i]]).Collection;
                }

                //Add
                tagItem = new TagItem(entry.Id) { Name = parts.Last(), Path = fullPath };
                currentItem.Add(parts.Last(), tagItem);
            }

            //Return
            return items.Values.ToList();
        }
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public sealed class DirectoryItem : Item
    {
        public List<Item> Items => Collection.Values.ToList();
        public Dictionary<string, Item> Collection { get; } = new Dictionary<string, Item>();
    }

    public sealed class TagItem : Item
    {
        public TagId Id { get; }

        public TagItem(TagId id)
        {
            Id = id;
        }
    }
}
