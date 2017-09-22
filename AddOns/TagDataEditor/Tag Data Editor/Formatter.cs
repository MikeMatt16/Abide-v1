using System.Text;

namespace Tag_Data_Editor
{
    public class TagDocumentFormatter : TagDataFormatter
    {
        private readonly StringBuilder body;

        public TagDocumentFormatter()
        {
            //Setup
            body = new StringBuilder();
        }
        public override void Clear()
        {
            //Initialize
            body.Clear();
        }
        public override void AddBitField(int uniqueId, string metaType, int bitCount, string name)
        {
            //Append
            body.AppendFormat(Files.Bitmask, uniqueId, metaType, bitCount, name);
        }
        public override void AddEnum(int uniqueId, string metaType, string options, string name)
        {
            //Append
            body.AppendFormat(Files.Enum, uniqueId, metaType, options, name);
        }
        public override void AddTagBlock(int uniqueId, long translation, int length, string name, string chunkOptions, string childContent)
        {
            //Append
            body.AppendFormat(Files.Reflexive, uniqueId, translation, length, name, chunkOptions, childContent);
        }
        public override void AddString(int uniqueId, int stringLength, string typeName, string name)
        {
            //Append
            body.AppendFormat(Files.String, uniqueId, stringLength, typeName, name);
        }
        public override void AddStringId(int uniqueId, string typeName, string name)
        {
            //Append
            body.AppendFormat(Files.StringID, uniqueId, typeName, name);
        }
        public override void AddTagId(int uniqueId, string typeName, string name)
        {
            //Append
            body.AppendFormat(Files.Tag, uniqueId, typeName, name);
        }
        public override void AddUnicode(int uniqueId, int stringLength, string typeName, string name)
        {
            //Append
            body.AppendFormat(Files.Unicode, uniqueId, stringLength, typeName, name);
        }
        public override void AddValue(int uniqueId, string metaType, string name)
        {
            //Append
            body.AppendFormat(Files.Value, uniqueId, metaType, name);
        }
        public override void AddBlockSelect(int uniqueId, string metaType, string options, string name)
        {
            //Append
            body.AppendFormat(Files.BlockSelect, uniqueId, metaType, options, name);
        }
        public string GetHtml()
        {
            string html = string.Format(Files.Editor, Files.Script, Files.EditorStyles, body.ToString());
            return html;
        }
    }

    public class TagBlockFormatter : TagDataFormatter
    {
        private readonly StringBuilder content;

        public TagBlockFormatter()
        {
            //Setup
            content = new StringBuilder();
        }
        public override void Clear()
        {
            //Initialize
            content.Clear();
        }
        public override void AddBitField(int uniqueId, string metaType, int bitCount, string name)
        {
            //Append
            content.AppendFormat(Files.Bitmask, uniqueId, metaType, bitCount, name);
        }
        public override void AddEnum(int uniqueId, string metaType, string options, string name)
        {
            //Append
            content.AppendFormat(Files.Enum, uniqueId, metaType, options, name);
        }
        public override void AddTagBlock(int uniqueId, long translation, int length, string name, string chunkOptions, string childContent)
        {
            //Append
            content.AppendFormat(Files.Reflexive, uniqueId, translation, length, name, chunkOptions, childContent);
        }
        public override void AddString(int uniqueId, int stringLength, string typeName, string name)
        {
            //Append
            content.AppendFormat(Files.String, uniqueId, stringLength, typeName, name);
        }
        public override void AddStringId(int uniqueId, string typeName, string name)
        {
            //Append
            content.AppendFormat(Files.StringID, uniqueId, typeName, name);
        }
        public override void AddTagId(int uniqueId, string typeName, string name)
        {
            //Append
            content.AppendFormat(Files.Tag, uniqueId, typeName, name);
        }
        public override void AddUnicode(int uniqueId, int stringLength, string typeName, string name)
        {
            //Append
            content.AppendFormat(Files.Unicode, uniqueId, stringLength, typeName, name);
        }
        public override void AddValue(int uniqueId, string metaType, string name)
        {
            //Append
            content.AppendFormat(Files.Value, uniqueId, metaType, name);
        }
        public override void AddBlockSelect(int uniqueId, string metaType, string options, string name)
        {
            //Append
            content.AppendFormat(Files.BlockSelect, uniqueId, metaType, options, name);
        }
        public string GetContent()
        {
            return content.ToString();
        }
    }

    public abstract class TagDataFormatter
    {
        public static string CreateOption(int value, string name)
        {
            //Return
            return string.Format(Files.EnumOption, value, name);
        }

        public abstract void Clear();
        public abstract void AddBitField(int uniqueId, string metaType, int bitCount, string name);
        public abstract void AddEnum(int uniqueId, string metaType, string options, string name);
        public abstract void AddTagBlock(int uniqueId, long translation, int length, string name, string chunkOptions, string childContent);
        public abstract void AddString(int uniqueId, int stringLength, string typeName, string name);
        public abstract void AddStringId(int uniqueId, string typeName, string name);
        public abstract void AddTagId(int uniqueId, string typeName, string name);
        public abstract void AddUnicode(int uniqueId, int stringLength, string typeName, string name);
        public abstract void AddValue(int uniqueId, string metaType, string name);
        public abstract void AddBlockSelect(int uniqueId, string metaType, string options, string name);
    }
}
