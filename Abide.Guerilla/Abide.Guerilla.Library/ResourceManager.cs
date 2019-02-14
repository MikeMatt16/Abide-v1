using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;

namespace Abide.Guerilla.Library
{
    public sealed class ResourceManager
    {
        public event ReferenceResolveEventHandler ResolveResource;

        private readonly List<string> m_ResourceList = new List<string>();
        private readonly List<string> m_StringsList = new List<string>();

        public ResourceManager()
        {
            m_StringsList.Add(string.Empty);
        }

        public void Clear()
        {
            m_ResourceList.Clear();
            m_StringsList.Clear();
        }

        public void CollectResources(ITagGroup tagGroup)
        {
            //Check
            if (tagGroup == null) throw new ArgumentNullException(nameof(tagGroup));

            //Collect resources
            foreach (ITagBlock tagBlock in tagGroup)
                CollectResources(tagBlock);
        }

        public void CollectResources(ITagBlock tagBlock)
        {
            //Check
            if (tagBlock == null) throw new ArgumentNullException(nameof(tagBlock));

            //Loop through fields
            foreach (Field field in tagBlock.Fields)
            {
                //Get value
                string value = field.Value?.ToString() ?? string.Empty;
                BaseBlockField blockField = field as BaseBlockField;

                //Handle type
                switch (field.Type)
                {
                    case FieldType.FieldStruct:
                        CollectResources((ITagBlock)field.Value);
                        break;

                    case FieldType.FieldBlock:
                        foreach (ITagBlock nestedTagBlock in blockField.BlockList)
                            CollectResources(nestedTagBlock);
                        break;

                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        if (!string.IsNullOrEmpty(value) && !m_StringsList.Contains(value))
                            m_StringsList.Add(value);
                        break;

                    case FieldType.FieldTagIndex:
                    case FieldType.FieldTagReference:
                        if (!string.IsNullOrEmpty(value) && !m_ResourceList.Contains(value))
                        {
                            m_ResourceList.Add(value);
                            CollectResources(ResolveResource.Invoke(value));
                        }
                        break;
                }
            }
        }

        public IEnumerable<string> GetResources()
        {
            foreach (string resource in m_ResourceList)
                yield return resource;
        }

        public IEnumerable<string> GetStrings()
        {
            foreach (string str in m_StringsList)
                yield return str;
        }
    }

    public delegate ITagGroup ReferenceResolveEventHandler(string resourceName);
}
