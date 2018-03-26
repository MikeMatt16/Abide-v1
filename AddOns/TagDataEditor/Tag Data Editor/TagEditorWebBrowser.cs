using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tag_Data_Editor
{
    [ComVisible(true)]
    public sealed class TagEditorWebBrowser : WebBrowser
    {
        public ScriptEventCallback TagButtonClickCallback
        {
            get { return tagButtonClick; }
            set { tagButtonClick = value; }
        }
        public ScriptEventCallback StringIdButtonClickCallback
        {
            get { return stringIdButtonClick; }
            set { stringIdButtonClick = value; }
        }
        public ScriptEventCallback ValueSetCallback
        {
            get { return valueSet; }
            set { valueSet = value; }
        }
        public ScriptEventCallback EnumSetCallback
        {
            get { return enumSet; }
            set { enumSet = value; }
        }
        public ScriptEventCallback BitmaskSetCallback
        {
            get { return bitmaskSet; }
            set { bitmaskSet = value; }
        }
        public ScriptEventCallback StringSetCallback
        {
            get { return stringSet; }
            set { stringSet = value; }
        }
        public ScriptEventCallback UnicodeSetCallback
        {
            get { return unicodeSet; }
            set { unicodeSet = value; }
        }

        private ScriptEventCallback tagButtonClick;
        private ScriptEventCallback stringIdButtonClick;
        private ScriptEventCallback valueSet;
        private ScriptEventCallback enumSet;
        private ScriptEventCallback bitmaskSet;
        private ScriptEventCallback stringSet;
        private ScriptEventCallback unicodeSet;

        public TagEditorWebBrowser()
        {
            ObjectForScripting = this;
        }
        public void TagButtonClick(string uidString, string id)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                tagButtonClick?.Invoke(uid, id ?? string.Empty);
        }
        public void StringIDButtonClick(string uidString, string id)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                stringIdButtonClick?.Invoke(uid, id ?? string.Empty);
        }
        public void SetValue(string uidString, string value)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                valueSet?.Invoke(uid, value ?? string.Empty);
        }
        public void SetEnum(string uidString, string value)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                enumSet?.Invoke(uid, value ?? string.Empty);
        }
        public void SetBitmask(string uidString, string value)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                bitmaskSet?.Invoke(uid, value ?? string.Empty);
        }
        public void SetString(string uidString, string value)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                stringSet?.Invoke(uid, value ?? string.Empty);
        }
        public void SetUnicode(string uidString, string value)
        {
            //Parse
            if (int.TryParse(uidString, out int uid))
                unicodeSet?.Invoke(uid, value ?? string.Empty);
        }
    }

    public delegate void ScriptEventCallback(int uniqueId, string valueString);
}
