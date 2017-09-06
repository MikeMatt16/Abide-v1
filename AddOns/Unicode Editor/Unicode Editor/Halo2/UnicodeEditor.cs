using Abide.AddOnApi.Halo2;
using System;

namespace Unicode_Editor.Halo2
{
    public partial class UnicodeEditor : AbideMenuButton
    {
        private const string unicTag = "unic";

        public UnicodeEditor()
        {
            Author = "Click16";
            Description = "Editor for unicode strings.";
            Icon = Properties.Resources.UnicodeEditor;
            Name = "Unicode Editor";
            ApplyTagFilter = true;
            TagFilter.Add(unicTag);
            Click += UnicodeEditor_Click;
        }

        private void UnicodeEditor_Click(object sender, EventArgs e)
        {
            //Check root
            if (SelectedEntry?.Root == unicTag)
                using (UnicodeEditorForm editor = new UnicodeEditorForm(Map, SelectedEntry.Strings))
                    editor.ShowDialog();
        }
    }
}
