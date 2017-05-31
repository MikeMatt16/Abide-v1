using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tag_Editor
{
    internal static class Files
    {
        private static readonly string ExecutingCodeBase = Assembly.GetExecutingAssembly().CodeBase;

        public static string Editor
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetEditorStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Bitmask
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetBitmaskStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Enum
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetEnumStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Reflexive
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetReflexiveStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string String
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetStringStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string StringID
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetStringIDStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Tag
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetTagStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Unicode
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetUnicodeStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Value
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetValueStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string EditorStyles
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetEditorStylesStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string Script
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetScriptStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }
        public static string EnumOption
        {
            get
            {
                string value = string.Empty;
                using (StreamReader reader = new StreamReader(GetEnumOptionStream()))
                    value = reader.ReadToEnd();
                return value;
            }
        }

        public static Stream GetEditorStream()
        {
            return GetFileStream("Editor.html", Properties.Resources.Editor);
        }
        public static Stream GetEditorStylesStream()
        {
            return GetFileStream("Editor Styles.css", Properties.Resources.Editor_Styles);
        }
        public static Stream GetScriptStream()
        {
            return GetFileStream("Script.js", Properties.Resources.Script);
        }
        public static Stream GetBitmaskStream()
        {
            return GetFileStream("Bitmask.html", Properties.Resources.Bitmask);
        }
        public static Stream GetEnumStream()
        {
            return GetFileStream("Enum.html", Properties.Resources.Enum);
        }
        public static Stream GetReflexiveStream()
        {
            return GetFileStream("Reflexive.html", Properties.Resources.Reflexive);
        }
        public static Stream GetStringStream()
        {
            return GetFileStream("String.html", Properties.Resources.String);
        }
        public static Stream GetStringIDStream()
        {
            return GetFileStream("StringID.html", Properties.Resources.StringID);
        }
        public static Stream GetTagStream()
        {
            return GetFileStream("Tag.html", Properties.Resources.Tag);
        }
        public static Stream GetUnicodeStream()
        {
            return GetFileStream("Unicode.html", Properties.Resources.Unicode);
        }
        public static Stream GetValueStream()
        {
            return GetFileStream("Value.html", Properties.Resources.Value);
        }
        public static Stream GetEnumOptionStream()
        {
            return GetFileStream("EnumOption.html", Properties.Resources.Value);
        }
        private static Stream GetFileStream(string fileName, string defaultContents)
        {
            //Prepare
            string directoryPath = GetPath("Meta Editor");
            string filePath = Path.Combine(directoryPath, fileName);

            //Create Directory
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            //Write?
            if (!File.Exists(filePath))
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.Write(defaultContents);
                    writer.Flush();
                }

            //Return
            return new FileStream(filePath, FileMode.Open);
        }

        private static string GetPath(string filePath)
        {
            string codeBase = new Uri(ExecutingCodeBase).LocalPath;
            return Path.Combine(Path.GetDirectoryName(codeBase), filePath);
        }
    }
}
