using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abide.Guerilla.Ui
{
    internal class CsWriter : IDisposable
    {
        private static readonly char[] LegalCharacters = new char[] { '_', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
        private bool indent = true;
        private List<string> enclosingHeirarcy = new List<string>();
        private Stream CsStream = null;
        private StreamWriter Writer = null;
        private int indentCount = 0;

        /// <summary>
        /// Gets or sets indentation while writing
        /// </summary>
        public bool Indent
        {
            get { return indent; }
            set { indent = value; }
        }
        
        public CsWriter(string fileName)
        {
            //Open
            CsStream = new FileStream(fileName, FileMode.Create);
            Writer = new StreamWriter(CsStream, new UTF8Encoding());
        }
        public void Close()
        {
            //Close
            Writer.Close();
            CsStream.Close();
        }
        public void Dispose()
        {
            //Cleanup
            Writer.Dispose();
            CsStream.Dispose();
        }

        public void WriteUsing(string nameSpace)
        {
            //Write
            Writer.Write("using"); WriteSpace();
            Writer.Write(GetSafeString(nameSpace)); WriteLineEnd();
        }
        public void WriteUsing(string typeName, string nameSpace)
        {
            //Write
            Writer.Write("using"); WriteSpace();
            Writer.Write(GetSafeString(typeName)); WriteSpace();
            Writer.Write("="); WriteSpace();
            Writer.Write(GetSafeString(nameSpace)); WriteEndExpression();
        }
        public void WriteStartNamespace(string nameSpace)
        {
            //Write
            Writer.Write("namespace"); WriteSpace();
            Writer.Write(GetSafeString(nameSpace)); WriteEndExpression();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndNamespace()
        {
            indentCount--;
            Writer.Write("}"); WriteEndExpression();
        }
        public void WriteStartEnum(string name, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write("enum"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteOption(string name)
        {
            string safeName = GetSafeString(name, NameType.Field);

            WriteIndent();
            Writer.Write(safeName); Writer.Write(','); WriteEndExpression();
        }
        public void WriteEndEnum()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
            if (enclosingHeirarcy.Count > 0)
                enclosingHeirarcy.RemoveAt(enclosingHeirarcy.Count - 1);
        }
        public void WriteAttribute(string type, params object[] args)
        {
            //Indent
            WriteIndent();

            //Write Open Bracket
            Writer.Write("[");

            //Write Name
            Writer.Write(type);

            //Arguments
            if (args != null && args.Length > 0)
            {
                //Write Open Parenthesis
                Writer.Write("(");

                //Write Args
                for (int i = 0; i < args.Length; i++)
                {
                    //Write Argument
                    Writer.Write(args[i]);

                    //Write Comma
                    if (i + 1 < args.Length)
                        Writer.Write(", ");
                }

                //Write Close Parenthesis
                Writer.Write(")");
            }
            //Write Close Bracket
            Writer.Write("]"); WriteEndExpression();
        }
        public void WriteStartClass(string name)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Writer
            Writer.Write("class"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartClass(string name, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write("class"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartClass(string name, string[] modifiers, string[] hierarchy)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write("class"); WriteSpace();
            Writer.Write(safeName);
            if (hierarchy.Length > 0)
            {
                //Write colon
                Writer.Write(" : ");

                //Write Modifiers
                for (int i = 0; i < hierarchy.Length; i++)
                {
                    Writer.Write(hierarchy[i]);
                    if (i + 1 < hierarchy.Length) Writer.Write(", ");
                }
            }
            WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndClass()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
            if (enclosingHeirarcy.Count > 0)
                enclosingHeirarcy.RemoveAt(enclosingHeirarcy.Count - 1);
        }
        public void WriteStartInterface(string name)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Writer
            Writer.Write("interface"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartInterface(string name, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write("interface"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndInterface()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
            if (enclosingHeirarcy.Count > 0)
                enclosingHeirarcy.RemoveAt(enclosingHeirarcy.Count - 1);
        }
        public void WriteStartStruct(string name)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Writer
            Writer.Write("struct"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartStruct(string name, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Class);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write("struct"); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndStruct()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
            if (enclosingHeirarcy.Count > 0)
                enclosingHeirarcy.RemoveAt(enclosingHeirarcy.Count - 1);
        }
        public void WriteStartField(string name, string type)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Property);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Write Indent
            WriteIndent();

            //Writer
            Writer.Write(type); WriteSpace();
            Writer.Write(safeName);
            WriteEndExpression();
            indentCount++;
        }
        public void WriteStartField(string name, string type, params string[] modifiers)
        {
            string safeName = GetSafeString(name, NameType.Property);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);

            //Write Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write(type); WriteSpace();
            Writer.Write(safeName);
            WriteEndExpression();
            indentCount++;
        }
        public void WriteEndField()
        {
            indentCount--;
            WriteIndent();
            Writer.Write(";"); WriteEndExpression();
        }
        public void WriteStartProperty(string name, string type)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Property);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);
            enclosingHeirarcy.Add(safeName);

            //Write Indent
            WriteIndent();

            //Writer
            Writer.Write(type); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartProperty(string name, string type, params string[] modifiers)
        {
            string safeName = GetSafeString(name, NameType.Property);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);

            //Write Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write(type); WriteSpace();
            Writer.Write(safeName); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndProperty()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
        }
        public void WriteAccessor(AccessorType accessor)
        {
            //Indent
            WriteIndent();

            //Write Accessor
            switch (accessor)
            {
                case AccessorType.Get: Writer.Write("get"); break;
                case AccessorType.Set: Writer.Write("set"); break;
                case AccessorType.Add: Writer.Write("add"); break;
                case AccessorType.Remove: Writer.Write("remove"); break;
            }

            //End
            Writer.Write(";"); WriteEndExpression();
        }
        public void WriteStartAccessor(AccessorType accessor)
        {
            //Indent
            WriteIndent();

            //Write Accessor
            switch (accessor)
            {
                case AccessorType.Get: Writer.Write("get"); break;
                case AccessorType.Set: Writer.Write("set"); break;
                case AccessorType.Add: Writer.Write("add"); break;
                case AccessorType.Remove: Writer.Write("remove"); break;
            }

            WriteSpace(); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteStartAccessor(AccessorType accessor, params string[] modifiers)
        {
            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Write Accessor
            switch (accessor)
            {
                case AccessorType.Get: Writer.Write("get"); break;
                case AccessorType.Set: Writer.Write("set"); break;
                case AccessorType.Add: Writer.Write("add"); break;
                case AccessorType.Remove: Writer.Write("remove"); break;
            }

            WriteSpace(); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndAccessor()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
        }
        public void WriteField(string type, string name, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Field);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);

            //Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Writer
            Writer.Write(type); WriteSpace();
            Writer.Write(safeName);
            Writer.Write(';'); WriteEndExpression();
        }
        public void WriteMethodLine(string name, string returnType, TypeNamePair[] arguments, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Function);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);

            //Write Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Write
            Writer.Write(returnType); WriteSpace();
            Writer.Write(safeName);
            Writer.Write("(");
            if (arguments != null && arguments.Length > 0)
                for (int i = 0; i < arguments.Length; i++)
                {
                    foreach (string mod in arguments[i].Modifiers)
                        Writer.Write(mod + ' ');
                    Writer.Write(arguments[i].Type + ' ');
                    Writer.Write(GetSafeString(arguments[i].Name, NameType.Field));
                    if (i < (arguments.Length - 1))
                        Writer.Write(", ");
                }
            Writer.Write(");");
            WriteEndExpression();
        }
        public void WriteMethod(string name, string returnType, TypeNamePair[] arguments, params string[] modifiers)
        {
            //Check...
            string safeName = GetSafeString(name, NameType.Function);
            while (enclosingHeirarcy.Count > 0 && enclosingHeirarcy[enclosingHeirarcy.Count - 1] == safeName)
                safeName = string.Format("_{0}", safeName);

            //Write Indent
            WriteIndent();

            //Write Modifiers
            foreach (string mod in modifiers)
                Writer.Write(mod + ' ');

            //Write
            Writer.Write(returnType); WriteSpace();
            Writer.Write(safeName);
            Writer.Write("(");
            if (arguments != null && arguments.Length > 0)
                for (int i = 0; i < arguments.Length; i++)
                {
                    foreach (string mod in arguments[i].Modifiers)
                        Writer.Write(mod + ' ');
                    Writer.Write(arguments[i].Type + ' ');
                    Writer.Write(GetSafeString(arguments[i].Name, NameType.Field));
                    if (i < (arguments.Length - 1))
                        Writer.Write(", ");
                }
            Writer.Write(")"); WriteEndExpression();
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndMethod()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
        }
        public void WriteSpace()
        {
            Writer.Write(' ');
        }
        public void WriteStartScope()
        {
            WriteIndent();
            Writer.Write("{"); WriteEndExpression();
            indentCount++;
        }
        public void WriteEndScope()
        {
            indentCount--;
            WriteIndent();
            Writer.Write("}"); WriteEndExpression();
        }
        public void Write(string text)
        {
            //Indent
            WriteIndent();

            //Write
            Writer.Write(text);
            WriteEndExpression();
        }
        public void WriteUnIndented(string text)
        {
            //Write
            Writer.Write(text);
            WriteEndExpression();
        }
        public void WriteEndExpression()
        {
            if (indent)
                Writer.Write("\r\n");
            else
                Writer.Write(' ');
        }
        private void WriteLineEnd()
        {
            Writer.Write(';');
            WriteEndExpression();
        }
        private void WriteIndent()
        {
            if (indent)
                for (int i = 0; i < indentCount; i++)
                    Writer.Write('\t');
        }
        private string GetSafeString(string input)
        {
            //Get List
            List<char> Characters = new List<char>(LegalCharacters);
            string safe = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (Characters.Contains(input[i]))
                    safe += input[i];
                else
                    safe += '_';
            }

            int n; bool numeric = int.TryParse(safe.Substring(0, 1), out n);
            if (numeric)
                safe = safe.Insert(0, "_");

            return safe;
        }
        public static string GetSafeString(string value, NameType type)
        {
            //Check
            if (string.IsNullOrEmpty(value)) value = "Empty";

            //Get List
            List<char> Characters = new List<char>(LegalCharacters);

            //Handle
            switch (type)
            {
                case NameType.Struct:
                case NameType.Class:
                case NameType.Field:
                case NameType.Property:
                case NameType.Function:
                    Characters.Remove('.');
                    break;
            }

            string safe = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                if (Characters.Contains(value[i]))
                    safe += value[i];
                else
                    safe += '_';
            }

            bool numeric = int.TryParse(safe.Substring(0, 1), out int n);
            if (numeric) safe = safe.Insert(0, "_");

            return safe;
        }

        public enum AccessorType
        {
            Get,
            Set,
            Add,
            Remove
        }
        public enum NameType
        {
            Namespace,
            Class,
            Struct,
            Field,
            Property,
            Function,
            Attribute
        }
        public class TypeNamePair
        {
            public string Type
            {
                get { return type; }
            }
            public string Name
            {
                get { return name; }
            }
            public string[] Modifiers
            {
                get { return modifiers; }
            }

            private readonly string type;
            private readonly string name;
            private readonly string[] modifiers;

            public TypeNamePair(string type, string name)
            {
                this.type = type;
                this.name = name;
                modifiers = new string[0];
            }
            public TypeNamePair(string type, string name, params string[] modifiers)
            {
                this.type = type;
                this.name = name;
                this.modifiers = modifiers;
            }
        }
    }
}
