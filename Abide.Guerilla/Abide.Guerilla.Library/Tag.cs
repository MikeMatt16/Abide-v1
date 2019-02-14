using Abide.Tag;
using Abide.Tag.Definition;

namespace Abide.Guerilla.Library
{
    public static class Tag
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagField"></param>
        /// <returns></returns>
        public static BlockList GetBlockList(Field tagField)
        {
            if (tagField.Type == FieldType.FieldBlock)
                return ((BaseBlockField)tagField).BlockList;
            return null;
        }
    }
}
