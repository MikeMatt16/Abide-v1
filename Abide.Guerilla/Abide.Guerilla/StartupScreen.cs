using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public partial class StartupScreen : Form
    {
        private Properties.Settings Settings => Properties.Settings.Default;

        public StartupScreen()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //Close the form
            Close();
        }

        private void StartupScreen_Load(object sender, EventArgs e)
        {
            //Defaults
            if (!Directory.Exists(Library.RegistrySettings.WorkspaceDirectory))
                workspaceDirectoryTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abide", "Guerilla");
            else workspaceDirectoryTextBox.Text = Library.RegistrySettings.WorkspaceDirectory;

            //Set
            mainmenuFileNameTextBox.Text = Library.RegistrySettings.MainmenuFileName;
            sharedFileNameTextBox.Text = Library.RegistrySettings.SharedFileName;
            singlePlayerSharedTextBox.Text = Library.RegistrySettings.SinglePlayerSharedFileName;
        }

        private void selectWorkspacePathButton_Click(object sender, EventArgs e)
        {
            //Create
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                folderDlg.Description = "Select output tags path.";
                if (folderDlg.ShowDialog() == DialogResult.OK)
                    workspaceDirectoryTextBox.Text = folderDlg.SelectedPath;
            }
        }

        private void browseSharedMapButton_Click(object sender, EventArgs e)
        {
            //Create
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.FileName = "shared.map";
                openDlg.Filter = "Shared Map Files (shared.map)|shared.map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    sharedFileNameTextBox.Text = openDlg.FileName;
            }
        }

        private void browseMainmenuButton_Click(object sender, EventArgs e)
        {
            //Create
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.FileName = "mainmenu.map";
                openDlg.Filter = "Mainmenu Map Files (mainmenu.map)|mainmenu.map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    mainmenuFileNameTextBox.Text = openDlg.FileName;
            }
        }

        private void browseSinglePlayerSharedButton_Click(object sender, EventArgs e)
        {
            //Create
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.FileName = "single_player_shared.map";
                openDlg.Filter = "Single Player Shared Map Files (single_player_shared.map)|single_player_shared.map";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    singlePlayerSharedTextBox.Text = openDlg.FileName;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //Check
            if(!Directory.Exists(workspaceDirectoryTextBox.Text))
                try { Directory.CreateDirectory(workspaceDirectoryTextBox.Text); }
                catch(Exception ex){ throw ex; }

            //Set
            Library.RegistrySettings.WorkspaceDirectory = workspaceDirectoryTextBox.Text;
            Library.RegistrySettings.MainmenuFileName = mainmenuFileNameTextBox.Text;
            Library.RegistrySettings.SharedFileName = sharedFileNameTextBox.Text;
            Library.RegistrySettings.SinglePlayerSharedFileName = singlePlayerSharedTextBox.Text;

            //Save
            Settings.FirstRun = false;
            Settings.Save();

            //Restart
            Application.Restart();
        }
    }
}
