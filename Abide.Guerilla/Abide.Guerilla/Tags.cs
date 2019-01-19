using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abide.Guerilla
{
    public static class Tags
    {
        public static TagFourCc[] GetExportedTagGroups()
        {
            //Prepare
            Assembly srcAsm = typeof(Tag.Cache.Generated.TagLookup).Assembly;
            List<TagFourCc> tags = new List<TagFourCc>();
            Tag.Group tagGroup = null;

            //Get exported types
            Type[] exportedTypes = srcAsm.GetExportedTypes();

            //Loop
            foreach (Type type in exportedTypes)
            {
                if(type.BaseType == typeof(Tag.Group))
                {
                    tagGroup = (Tag.Group)srcAsm.CreateInstance(type.FullName);
                    tags.Add(tagGroup.GroupTag);
                }
            }

            //Return
            return tags.ToArray();
        }
    }
}
