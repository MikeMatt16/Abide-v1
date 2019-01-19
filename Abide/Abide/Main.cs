using Abide.Dialogs;
using Abide.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using YeloDebug;
using YeloDebug.Exceptions;

namespace Abide
{
    internal partial class Main : Form
    {
        /// <summary>
        /// Gets and returns the primary Debug Xbox connection.
        /// </summary>
        public Xbox DebugXbox
        {
            get { return debugXbox;}
        }

        private readonly Xbox debugXbox = new Xbox(Application.StartupPath);

        public Main()
        {
            InitializeComponent();
            mainMenuStrip.Renderer = new AbideToolStripRenderer();
            recentFiles_Init();
        }

        private void recentFiles_Init()
        {
            //Init
            bool showSeparator = false;
            showSeparator |= recentFiles_InitUiHalo2();
            showSeparator |= recentFiles_InitUiHalo2b();

            //Show
            fileToolStripSeparator2.Visible = showSeparator;
        }

        private bool recentFiles_InitUiHalo2()
        {
            //Prepare
            bool containsItems = false;

            //Hide
            recentHalo2MapsToolStripMenuItem.Visible = false;

            //Clear
            recentHalo2MapsToolStripMenuItem.DropDownItems.Clear();

            //Add...
            recentHalo2MapsToolStripMenuItem.DropDownItems.Add(clearRecentHalo2MapsToolStripMenuItem);
            recentHalo2MapsToolStripMenuItem.DropDownItems.Add(recentHalo2MapsToolStripSeparator1);

            //Check
            if (AbideRegistry.Halo2RecentFiles.Length > 0)
            {
                //Set
                recentHalo2MapsToolStripMenuItem.Visible = true;
                containsItems = true;

                //Loop
                for (int i = 0; i < AbideRegistry.Halo2RecentFiles.Length; i++)
                {
                    //Create
                    ToolStripMenuItem recentItem = new ToolStripMenuItem($"&{i + 1}: {AbideRegistry.Halo2RecentFiles[i].GetCompactPath(40)}");
                    recentItem.Tag = AbideRegistry.Halo2RecentFiles[i];
                    recentItem.Click += recentItem_Click;

                    //Add
                    recentHalo2MapsToolStripMenuItem.DropDownItems.Add(recentItem);
                }
            }

            //Return
            return containsItems;
        }

        private bool recentFiles_InitUiHalo2b()
        {
            //Prepare
            bool containsItems = false;

            //Hide
            recentHalo2BetaMapsToolStripMenuItem.Visible = false;

            //Clear
            recentHalo2BetaMapsToolStripMenuItem.DropDownItems.Clear();

            //Add...
            recentHalo2BetaMapsToolStripMenuItem.DropDownItems.Add(clearRecentHalo2BetaMapsToolStripMenuItem);
            recentHalo2BetaMapsToolStripMenuItem.DropDownItems.Add(recentHalo2BetaMapsToolStripSeparator1);

            //Check
            if (AbideRegistry.Halo2bRecentFiles.Length > 0)
            {
                //Set
                recentHalo2BetaMapsToolStripMenuItem.Visible = true;
                containsItems = true;

                //Loop
                for (int i = 0; i < AbideRegistry.Halo2bRecentFiles.Length; i++)
                {
                    //Create
                    ToolStripMenuItem recentItem = new ToolStripMenuItem($"&{i + 1}: {AbideRegistry.Halo2bRecentFiles[i].GetCompactPath(40)}");
                    recentItem.Tag = AbideRegistry.Halo2bRecentFiles[i];
                    recentItem.Click += recentItem_Click;

                    //Add
                    recentHalo2BetaMapsToolStripMenuItem.DropDownItems.Add(recentItem);
                }
            }

            //Return
            return containsItems;
        }

        private void mapFile_Open(string filename)
        {
            //Prepare
            List<string> files = null;
            Form editor = null;

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
                    files = new List<string>(AbideRegistry.Halo2RecentFiles);
                    files.Remove(filename); files.Insert(0, filename);
                    AbideRegistry.Halo2RecentFiles = files.ToArray();
                    recentFiles_Init();

                    //Setup Editor
                    editor = new Halo2.Editor(filename) { MdiParent = this };
                    editor.Show();
                    break;
                case HaloLibrary.MapVersion.Halo2b:
                    //Add
                    files = new List<string>(AbideRegistry.Halo2bRecentFiles);
                    files.Remove(filename); files.Insert(0, filename);
                    AbideRegistry.Halo2bRecentFiles = files.ToArray();
                    recentFiles_Init();

                    //Setup Editor
                    editor = new Halo2Beta.Editor(filename) { MdiParent = this };
                    editor.Show();
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

        private void Main_Load(object sender, EventArgs e)
        {
            //Get Version string...
            versionToolStripMenuItem.Text = typeof(Main).Assembly.GetName().Version.ToString();

            //Check
            if (Program.UpdateManifest != null && !Program.IsAlpha && main_CheckForUpdate(Program.UpdateManifest) || Program.ForceUpdate)
                using (UpdateDialog updateDlg = new UpdateDialog(Program.UpdateManifest))
                    if (updateDlg.ShowDialog() == DialogResult.OK) Application.Exit();

            //Load Files
            FileInfo info = null;
            foreach (string filename in Program.Files)
            {
                //Initialize
                try
                {
                    info = new FileInfo(filename);
                    switch (info.Extension)
                    {
                        case ".map": mapFile_Open(filename); break;
                    }
                }
                catch { }
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            //Check for file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            //Check for file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Get Files
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                //Loop
                FileInfo info = null;
                foreach (string filename in files)
                {
                    //Initialize
                    try
                    {
                        info = new FileInfo(filename);
                        switch (info.Extension)
                        {
                            case ".map": mapFile_Open(filename); break;
                        }
                    }
                    catch { }
                }
            }
        }

        private void recentItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = null;

            //Get File name from Tag...
            if (sender is ToolStripMenuItem && ((ToolStripMenuItem)sender).Tag is string)
                filename = (string)((ToolStripMenuItem)sender).Tag;

            //Check
            if (!string.IsNullOrEmpty(filename)) mapFile_Open(filename);
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
            if (open) mapFile_Open(filename);
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

                //Create
                AddOnPackageManifest manifest = new AddOnPackageManifest(filename);

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
            AbideRegistry.Halo2RecentFiles = files;

            //Init
            recentFiles_Init();
        }

        private void clearRecentHalo2BetaMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create
            string[] files = new string[10];
            for (int i = 0; i < 10; i++) files[i] = string.Empty;

            //Set
            AbideRegistry.Halo2bRecentFiles = files;

            //Init
            recentFiles_Init();
        }

        private void registerFileTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Registration Dialog
            using (FileTypeRegistrationDialog regDlg = new FileTypeRegistrationDialog())
                if (regDlg.ShowDialog() == DialogResult.Yes)
                    if (MessageBox.Show("It is recommended that Windows Explorer is restarted. Would you like to restart now?",
                        "Restart?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Get Explorer Process
                        string filename = null;

                        //Loop through processes
                        foreach (Process process in Process.GetProcessesByName("explorer"))
                        {
                            try { filename = process.MainModule.FileName; process.Kill(); }
                            catch { }
                        }
                    }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            XmlDocument manifestDocument = new XmlDocument();
            UpdateManifest manifest = null;
            bool update = Program.ForceUpdate;

            try
            {
                //Load XML Document
                manifestDocument.Load(Program.UpdateManifestUrl);

                //Get Update Manifest
                manifest = new UpdateManifest(manifestDocument);

                //Check
                update |= main_CheckForUpdate(manifest);
            }
            catch { update = false; }

            //Check
            if (update)
            {
                using (UpdateDialog updateDlg = new UpdateDialog(manifest))
                    if (updateDlg.ShowDialog() == DialogResult.OK) { Application.Exit(); }
            }
            else MessageBox.Show("Abide is up to date.", "Up to date", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void quickConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get Current State
            bool state = debugXbox.Connected;

            //Connect/Disconnect...
            if (debugXbox.Connected) debugXbox.Disconnect();
            else try { debugXbox.Connect(); }
                catch (ApiException) { MessageBox.Show("Invalid XBDM version. YeloDebug requires v1.00.7887.1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            //Set Text
            if (debugXbox.Connected) quickConnectToolStripMenuItem.Text = $"Disconnect {debugXbox.DebugName}";
            else quickConnectToolStripMenuItem.Text = "Quick Connect";

            //State Changed
            if (state != debugXbox.Connected)
                Classes.Globals.OnXboxChanged(e);
        }
        
        private bool main_CheckForUpdate(UpdateManifest manifest)
        {
            //Prepare
            string root = Application.StartupPath;
            AssemblyName assemblyName = null;
            bool update = false;

            //Loop
            foreach (var info in Program.UpdateManifest)
            {
                //Check
                if (!File.Exists(Path.Combine(root, info.Filename))) update |= true;
                else if (info.AssemblyName != null)
                {
                    //Get Name
                    assemblyName = AssemblyName.GetAssemblyName(Path.Combine(root, info.Filename));
                    update |= info.AssemblyName.Version > assemblyName.Version;
                }

                //Check
                if (update) break;
            }

            //Return
            return update;
        }
    }
}
