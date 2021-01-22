using Abide.Guerilla.Library;
using Abide.Tag;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Test
{
    class Program
    {
        static string TagsDirectory { get; } = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags");

        static void Main(string[] args)
        {
            var tagReferences = new List<string>();
            var file = new AbideTagGroupFile();
            file.Load(args[0]);

            DiscoverReferences(tagReferences, file.TagGroup);
        }

        private static void DiscoverReferences(List<string> tagReferences, Group tagGroup)
        {
            foreach (var tagBlock in tagGroup.TagBlocks)
            {
                DiscoverReferences(tagReferences, tagBlock);
            }
        }

        private static void DiscoverReferences(List<string> tagReferences, Block tagBlock)
        {
            foreach (var field in tagBlock)
            {
                switch (field)
                {
                    case TagReferenceField tagReferenceField:
                        if (!string.IsNullOrEmpty(tagReferenceField.Value))
                        {
                            if (!tagReferences.Contains(tagReferenceField.Value))
                            {
                                string fileName = Path.Combine(TagsDirectory, tagReferenceField.Value);
                                if (File.Exists(fileName))
                                {
                                    tagReferences.Add(tagReferenceField.Value);
                                    var file = new AbideTagGroupFile();
                                    file.Load(fileName);
                                    DiscoverReferences(tagReferences, file.TagGroup);
                                }
                            }
                        }
                        break;

                    case BlockField blockField:
                        foreach (var block in blockField.BlockList)
                        {
                            DiscoverReferences(tagReferences, block);
                        }
                        break;

                    case StructField structField:
                        DiscoverReferences(tagReferences, structField.Block);
                        break;
                }
            }
        }
    }
}
