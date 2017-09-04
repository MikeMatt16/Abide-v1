using System;
using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;

namespace Unicode_Editor.Halo2
{
    public partial class UnicodeEditor : AbideMenuButton
    {
        public UnicodeEditor()
        {
            Author = "Click16";
            Description = "Editor for unicode strings.";
            Icon = Properties.Resources.UnicodeEditor;
            Name = "Unicode Editor";
        }

        protected override void OnSelectedEntryChanged(EventArgs e)
        {
            base.OnSelectedEntryChanged(e);
        }
        protected override void OnIntialize(AddOnHostEventArgs e)
        {
            ApplyTagFilter = true;
            TagFilter.Add(new Abide.HaloLibrary.Tag("cinu"));
        }
    }
}
