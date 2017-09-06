using Abide.AddOnApi;
using System.Windows.Forms;

namespace Tag_Data_Editor
{
    public partial class TagEditorSettings : SettingsPage
    {
        public TagEditorSettings()
        {
            InitializeComponent();
        }

        private void TagEditorSettings_Load(object sender, System.EventArgs e)
        {
            h2PluginsPathBox.Text = AbideRegistry.Halo2PluginsDirectory;
        }

        private void h2PluginsBrowseButton_Click(object sender, System.EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Set
                folderDlg.SelectedPath = AbideRegistry.Halo2PluginsDirectory;

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    h2PluginsBrowseButton.Text = folderDlg.SelectedPath;
                    AbideRegistry.Halo2PluginsDirectory = folderDlg.SelectedPath;
                }
            }
        }
    }
}
