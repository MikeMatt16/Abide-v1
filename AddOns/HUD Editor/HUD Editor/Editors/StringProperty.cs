using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using HUD_Editor.Halo2;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace HUD_Editor.Editors
{
    public sealed class StringProperty
    {
        public StringId ID
        {
            get { return id; }
        }

        private readonly StringId id;
        private readonly string text;

        public StringProperty(StringId id, string text)
        {
            //Check
            if (text == null) throw new ArgumentNullException(nameof(text));

            //Set
            this.id = id;
            this.text = text;
        }

        public override string ToString()
        {
            return text;
        }

        public sealed class StringIDEditor : UITypeEditor
        {
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                //Prepare
                MapFile map = null;
                StringProperty sid = new StringProperty(StringId.Zero, string.Empty);

                //Check
                if (value is StringProperty) sid = (StringProperty)value;

                //Get Map
                if (context.Instance is MapFileContainer)
                    map = ((MapFileContainer)context.Instance).Map;

                //Check
                //if (map != null)
                //    using (StringSelectDialog stringDlg = new StringSelectDialog(map.Strings))
                //    {
                //        //Set
                //        stringDlg.SelectedString = sid.ID;
                //
                //        //Show
                //        if (stringDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) sid = new StringProperty(stringDlg.SelectedString, map.Strings[stringDlg.SelectedString.Index]);
                //        else return value;
                //    }
                //
                ////Return
                //return sid;
                return null;
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }
        }
    }
}
