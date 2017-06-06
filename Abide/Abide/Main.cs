using Abide.Dialogs;
using Abide.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Abide
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            recentFiles_InitUiHalo2();
        }

        private void recentFiles_InitUiHalo2()
        {
            //Hide
            recentHalo2MapsToolStripMenuItem.Visible = false;
            fileToolStripSeparator2.Visible = false;

            //Clear
            recentHalo2MapsToolStripMenuItem.DropDownItems.Clear();

            //Add...
            recentHalo2MapsToolStripMenuItem.DropDownItems.Add(clearRecentHalo2MapsToolStripMenuItem);
            recentHalo2MapsToolStripMenuItem.DropDownItems.Add(recentHalo2MapsToolStripSeparator1);

            //Check
            if (RegistrySettings.Halo2RecentFiles.Length > 0)
            {
                //Set
                recentHalo2MapsToolStripMenuItem.Visible = true;
                fileToolStripSeparator2.Visible = true;
                //Loop
                for (int i = 0; i < RegistrySettings.Halo2RecentFiles.Length; i++)
                {
                    //Create
                    ToolStripMenuItem recentItem = new ToolStripMenuItem($"&{i + 1}: {RegistrySettings.Halo2RecentFiles[i].GetCompactPath(40)}");
                    recentItem.Tag = RegistrySettings.Halo2RecentFiles[i];
                    recentItem.Click += recentItem_Click;

                    //Add
                    recentHalo2MapsToolStripMenuItem.DropDownItems.Add(recentItem);
                }
            }
        }

        private void file_Open(string filename)
        {
            //Check
            if (!File.Exists(filename)) return;

            //Get Map Version
            switch (MapHelper.GetMapVersion(filename))
            {
                case HaloLibrary.MapVersion.HaloCE:
                    break;
                case HaloLibrary.MapVersion.HaloCEx:
                    break;
                case HaloLibrary.MapVersion.Halo2:
                    //Add
                    List<string> files = new List<string>(RegistrySettings.Halo2RecentFiles);
                    files.Remove(filename); files.Insert(0, filename);
                    RegistrySettings.Halo2RecentFiles = files.ToArray();
                    recentFiles_InitUiHalo2();

                    //Setup Editor
                    Form editor = new Halo2.Editor(filename);
                    editor.MdiParent = this;

                    //Show
                    editor.Show();
                    break;
                case HaloLibrary.MapVersion.Halo2b:
                    break;
                case HaloLibrary.MapVersion.Halo2v:
                    break;
                case HaloLibrary.MapVersion.Halo3:
                    break;
                case HaloLibrary.MapVersion.Halo3b:
                    break;
                case HaloLibrary.MapVersion.HaloReach:
                    break;
            }
        }

        private void recentItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = null;

            //Get File name from tag...
            if (sender is ToolStripMenuItem && ((ToolStripMenuItem)sender).Tag is string)
                filename = (string)((ToolStripMenuItem)sender).Tag;

            //Check
            if (!string.IsNullOrEmpty(filename)) file_Open(filename);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Halo Map Files (*.map)|*.map";
                openDlg.Title = "Open Map File...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open) file_Open(filename);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void createAddOnPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "AddOn Assemblies (*.dll;*.exe)|*.dll;*.exe";
                openDlg.Title = "Open Assembly...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Get Info
                FileInfo info = new FileInfo(filename);

                //Prepare
                using (PackageAddOnDialog packDlg = new PackageAddOnDialog())
                {
                    //Setup
                    packDlg.PrimaryAssembly = Path.GetFileName(filename);
                    packDlg.PackageName = info.Name.Replace(".exe", string.Empty).Replace(".dll", string.Empty);
                    packDlg.LoadDirectory(Path.GetDirectoryName(filename));

                    //Create
                    packDlg.ShowDialog();
                }
            }
        }

        private void addOnManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Manager
            using (AddOnManager manager = new AddOnManager())
                manager.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Options Dialog
            using (OptionsDialog optDlg = new OptionsDialog())
                optDlg.ShowDialog();
        }

        private void clearRecentHalo2MapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create
            string[] files = new string[10];
            for (int i = 0; i < 10; i++) files[i] = string.Empty;

            //Set
            RegistrySettings.Halo2RecentFiles = files;

            //Init
            recentFiles_InitUiHalo2();
        }
    }
}
