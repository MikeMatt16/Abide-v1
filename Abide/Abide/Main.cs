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

namespace Abide
{
    public partial class Main : Form
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
            if (AbideRegistry.Halo2RecentFiles.Length > 0)
            {
                //Set
                recentHalo2MapsToolStripMenuItem.Visible = true;
                fileToolStripSeparator2.Visible = true;
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
                    List<string> files = new List<string>(AbideRegistry.Halo2RecentFiles);
                    files.Remove(filename); files.Insert(0, filename);
                    AbideRegistry.Halo2RecentFiles = files.ToArray();
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

        private void Main_Load(object sender, EventArgs e)
        {
            //Extend
            if (main_ExtendFrame()) mainMenuStrip.Renderer = new AeroToolStripRenderer();
            else mainMenuStrip.Renderer = new AbideToolStripRenderer();

            //Get Version string...
            versionToolStripMenuItem.Text = typeof(Main).Assembly.GetName().Version.ToString();

            //Check
            if (Program.UpdateManifest != null && main_CheckForUpdate(Program.UpdateManifest) || Program.ForceUpdate)
                using (UpdateDialog updateDlg = new UpdateDialog(Program.UpdateManifest))
                    if (updateDlg.ShowDialog() == DialogResult.OK) Application.Exit();
        }

        private void recentItem_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = null;

            //Get File name from Tag...
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
            recentFiles_InitUiHalo2();
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
            //Connect/Disconnect...
            if (debugXbox.Connected) debugXbox.Disconnect();
            else try { debugXbox.Connect(); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            //Set Text
            if (debugXbox.Connected) quickConnectToolStripMenuItem.Text = $"Disconnect {debugXbox.DebugName}";
            else quickConnectToolStripMenuItem.Text = "Quick Connect";
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

        private bool main_ExtendFrame()
        {
            //Prepare
            bool success = false;

            //Check
            if (Environment.OSVersion.Version.Major >= 6)
            {
                if (DwmIsCompositionEnabled(out success) == IntPtr.Zero && success)
                    success = DwmExtendFrameIntoClientArea(Handle, new MARGINS(0, 0, mainMenuStrip.Height, 0)) == IntPtr.Zero;
            }
            else return false;

            //Return
            return success;
        }

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hWnd">The handle to the window in which the frame will be extended into the client area.</param>
        /// <param name="pMarInset">A <see cref="MARGINS"/> structure that describes the margins to use when extending the frame into the client area.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("Dwmapi.dll")]
        private static extern IntPtr DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMarInset);

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
        /// </summary>
        /// <param name="pfEnabled">When this function returns successfully, receives true if DWM composition is enabled; otherwise, false.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("Dwmapi.dll")]
        private static extern IntPtr DwmIsCompositionEnabled(out bool pfEnabled);

        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            /// <summary>
            /// Gets or sets the width of the left border that retains its size.
            /// </summary>
            public int Left
            {
                get { return left; }
                set { left = value; }
            }
            /// <summary>
            /// Gets or sets the width of the right border that retains its size.
            /// </summary>
            public int Right
            {
                get { return right; }
                set { right = value; }
            }
            /// <summary>
            /// Gets or sets the height of the top border that retains its size.
            /// </summary>
            public int Top
            {
                get { return top; }
                set { top = value; }
            }
            /// <summary>
            /// Gets or sets the height of the bottom border that retains its size.
            /// </summary>
            public int Bottom
            {
                get { return bottom; }
                set { bottom = value; }
            }

            private int left, right, top, bottom;

            /// <summary>
            /// Initializes a new <see cref="MARGINS"/> structure using the specified left, right, top, and bottom values.
            /// </summary>
            /// <param name="left">The width of the left border.</param>
            /// <param name="right">The width of the right border.</param>
            /// <param name="top">The height of the top border.</param>
            /// <param name="bottom">The height of the bottom border.</param>
            public MARGINS(int left, int right, int top, int bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
            }
        }
    }
}
