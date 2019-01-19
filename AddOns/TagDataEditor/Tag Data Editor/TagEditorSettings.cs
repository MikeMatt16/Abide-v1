using Abide.AddOnApi;
using System.Windows.Forms;

namespace Tag_Data_Editor
{
    [AddOn]
    public partial class TagEditorSettings : SettingsPage
    {
        public TagEditorSettings()
        {
            InitializeComponent();
        }

        private void TagEditorSettings_Load(object sender, System.EventArgs e)
        {
            //Set
            h2PluginsPathBox.Text = AbideRegistry.Halo2PluginsDirectory.GetCompactPath(26);
            h2bPluginsPathBox.Text = AbideRegistry.Halo2bPluginsDirectory.GetCompactPath(26);
        }

        private void h2PluginsBrowseButton_Click(object sender, System.EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Set
                folderDlg.SelectedPath = AbideRegistry.Halo2PluginsDirectory;
                folderDlg.Description = "Browse to Halo 2 Plugins path.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    h2PluginsPathBox.Text = folderDlg.SelectedPath.GetCompactPath(26);
                    AbideRegistry.Halo2PluginsDirectory = folderDlg.SelectedPath;
                }
            }
        }

        private void h2bPluginsBrowseButton_Click(object sender, System.EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Set
                folderDlg.SelectedPath = AbideRegistry.Halo2bPluginsDirectory;
                folderDlg.Description = "Browse to Halo 2 Beta Plugins path.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    h2bPluginsPathBox.Text = folderDlg.SelectedPath.GetCompactPath(26);
                    AbideRegistry.Halo2bPluginsDirectory = folderDlg.SelectedPath;
                }
            }
        }
    }
}
