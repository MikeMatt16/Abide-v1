using System;
using System.Windows.Forms;
using System.Xml;

namespace Tag_Data_Editor.Controls
{
    public class MetaControl : UserControl
    {
        public int FieldOffset
        {
            get
            {
                int offset = 0;
                if (int.TryParse(PluginElement?.Attributes["offset"]?.Value, out offset)) return offset;
                else return -1;
            }
        }
        public int FieldSize
        {
            get
            {
                int size = 0;
                if (int.TryParse(PluginElement?.Attributes["size"]?.Value, out size)) return size;
                else return -1;
            }
        }

        public LabelReference Label
        {
            get { return labelRef; }
            set { labelRef = value; }
        }
        public virtual string ControlName { get; set; }
        public virtual string Type { get; set; }
        public XmlNode PluginElement { get; set; }
        public long Address { get; set; }

        private LabelReference labelRef = new LabelReference();
        
        public MetaControl() { }

        public override string ToString()
        {
            //Return
            return labelRef?.Text;
        }
    }

    public class LabelReference : IEquatable<LabelReference>, IComparable<LabelReference>, IEquatable<string>, IComparable<string>
    {
        public event EventHandler TextUpdated
        {
            add { textUpdated += value; }
            remove { textUpdated -= value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; OnTextUpdated(new EventArgs()); }
        }

        private string text = string.Empty;
        private event EventHandler textUpdated;

        public LabelReference() { }
        public LabelReference(string text)
        {
            this.text = text;
        }
        public bool Equals(LabelReference other)
        {
            return text.Equals(other.text);
        }
        public bool Equals(string other)
        {
            return text.Equals(other);
        }
        public int CompareTo(LabelReference other)
        {
            return text.CompareTo(other.text);
        }
        public int CompareTo(string other)
        {
            return text.CompareTo(other);
        }
        public override string ToString()
        {
            if (text != null) return text;
            return "null";
        }
        protected virtual void OnTextUpdated(EventArgs e)
        {
            //Invoke
            textUpdated?.Invoke(this, e);
        }
    }
}
