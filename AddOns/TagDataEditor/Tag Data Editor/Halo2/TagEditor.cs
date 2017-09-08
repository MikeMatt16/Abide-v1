using System;
using Abide.AddOnApi.Halo2;
using System.Windows.Forms;
using Abide.AddOnApi;

namespace Tag_Data_Editor.Halo2
{
    public partial class TagEditor : AbideTool
    {
        public TagEditor()
        {
            InitializeComponent();
        }

        private void TagEditor_Initialize(object sender, AddOnHostEventArgs e)
        {
            //Set ownership of the editor.
            tagDataEditor.Owner = this;
        }

        private void TagEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            return;
            //Change entry
            tagDataEditor.Entry = SelectedEntry;
        }

        private void TagEditor_MapLoad(object sender, EventArgs e)
        {
            //Set map
            tagDataEditor.Map = Map;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Create editor
            PopoutEditor editor = new PopoutEditor(this, Map, SelectedEntry);
            editor.FormClosed += Editor_FormClosed;

            //Setup
            editor.Text = $"{Map.Name} - {SelectedEntry.Filename}.{SelectedEntry.Root}";
            editor.Show();
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Dispose
            if (sender is IDisposable) ((IDisposable)sender).Dispose();
        }
    }
}
