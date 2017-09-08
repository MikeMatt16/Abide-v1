using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using HUD_Editor.Halo2;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace HUD_Editor.Editors
{
    public sealed class TagProperty
    {
        public TagId ID
        {
            get { return id; }
        }

        private readonly TagId id;
        private readonly IndexEntry entry;

        public TagProperty(TagId id, IndexEntry entry)
        {
            //Set
            this.id = id;
            this.entry = entry;
        }

        public override string ToString()
        {
            if (entry == null || id == TagId.Null) return "null";
            string[] parts = entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return $"{parts[parts.Length - 1]}.{entry.Root}";
        }

        public sealed class TagEditor : UITypeEditor
        {
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                //Prepare
                MapFile map = null;
                TagProperty id = new TagProperty(TagId.Null, null);

                //Check
                if (value is TagProperty) id = (TagProperty)value;

                //Get Map
                if (context.Instance is MapFileContainer)
                    map = ((MapFileContainer)context.Instance).Map;

                //Check
                //if (map != null)
                //    using (TagBrowserDialog tagDlg = new TagBrowserDialog(map.IndexEntries, map.Name))
                //    {
                //        //Set
                //        tagDlg.SelectedID = id.ID;
                //
                //        //Show
                //        if (tagDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) return new TagProperty(tagDlg.SelectedID, map.IndexEntries[tagDlg.SelectedID]);
                //        else return value;
                //    }
                //else throw new InvalidOperationException();
                return null;
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }
        }
    }
}
