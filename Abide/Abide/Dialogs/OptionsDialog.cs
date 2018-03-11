using Abide.AddOnApi;
using Abide.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abide.Dialogs
{
    internal partial class OptionsDialog : Form, IHost
    {
        private readonly SettingsAddOnContainer container;

        public OptionsDialog()
        {
            InitializeComponent();

            //Prepare Container
            container = new SettingsAddOnContainer();
            container.BeginInit(this);

            //Load Settings Pages
            foreach (var settingsPage in container.GetSettingsPages())
            {
                //Prepare
                settingsPage.UserInterface.Dock = DockStyle.Fill;

                //Create
                TabPage page = new TabPage(settingsPage.Name);
                page.Controls.Add(settingsPage.UserInterface);
                page.Name = settingsPage.Name;

                //Add
                settingsTabControl.TabPages.Add(page);
            }

            //Load Settings
            halo2SpSharedFilePathTextBox.Text = AbideRegistry.Halo2SpShared.GetCompactPath(35);
            halo2SharedFilePathTextBox.Text = AbideRegistry.Halo2Shared.GetCompactPath(35);
            halo2MainmenuFilePathTextBox.Text = AbideRegistry.Halo2Mainmenu.GetCompactPath(35);
        }

        private void spSharedBrowseButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "SP Shared Maps (single_player_shared.map)|single_player_shared.map";
                openDlg.Title = "Open SP Shared...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Find...
                scanDirectoryForResourceMaps(Path.GetDirectoryName(filename), ResourceMapFileKind.Shared, ResourceMapFileKind.Mainmenu);

                //Set...
                halo2SpSharedFilePathTextBox.Text = filename.GetCompactPath(34);
                AbideRegistry.Halo2SpShared = filename;
            }
        }

        private void sharedBrowseButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Shared Maps (shared.map)|shared.map";
                openDlg.Title = "Open Shared...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Find...
                scanDirectoryForResourceMaps(Path.GetDirectoryName(filename), ResourceMapFileKind.SPShared, ResourceMapFileKind.Mainmenu);

                //Set...
                halo2SharedFilePathTextBox.Text = filename.GetCompactPath(34);
                AbideRegistry.Halo2Shared = filename;
            }
        }

        private void mainmenuBrowseButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Mainmenu Maps (mainmenu.map)|mainmenu.map";
                openDlg.Title = "Open Mainmenu...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Find...
                scanDirectoryForResourceMaps(Path.GetDirectoryName(filename), ResourceMapFileKind.Shared, ResourceMapFileKind.SPShared);

                //Set...
                halo2MainmenuFilePathTextBox.Text = filename.GetCompactPath(34);
                AbideRegistry.Halo2Mainmenu = filename;
            }
        }

        private void scanDirectoryForResourceMaps(string directory, params ResourceMapFileKind[] resourceMapKinds)
        {
            //Prepare
            Dictionary<ResourceMapFileKind, string> foundResourceMaps = new Dictionary<ResourceMapFileKind, string>();

            //Check
            if (!Directory.Exists(directory)) return;

            //Loop
            foreach (ResourceMapFileKind kind in resourceMapKinds)
                switch (kind)
                {
                    case ResourceMapFileKind.Mainmenu:
                        if (File.Exists(Path.Combine(directory, "mainmenu.map")))
                            foundResourceMaps.Add(ResourceMapFileKind.Mainmenu, Path.Combine(directory, "mainmenu.map"));
                        break;
                    case ResourceMapFileKind.Shared:
                        if (File.Exists(Path.Combine(directory, "shared.map")))
                            foundResourceMaps.Add(ResourceMapFileKind.Shared, Path.Combine(directory, "shared.map"));
                        break;
                    case ResourceMapFileKind.SPShared:
                        if (File.Exists(Path.Combine(directory, "single_player_shared.map")))
                            foundResourceMaps.Add(ResourceMapFileKind.SPShared, Path.Combine(directory, "single_player_shared.map"));
                        break;
                }

            //Check
            if(foundResourceMaps.Count > 0 && MessageBox.Show($"We found other resource map(s) in the following directory: " + 
                $"{Environment.NewLine}{directory}{Environment.NewLine}Would you like to set them as well?") == DialogResult.Yes)
                foreach (var foundResourceMap in foundResourceMaps)
                    switch (foundResourceMap.Key)
                    {
                        case ResourceMapFileKind.Mainmenu:
                            halo2MainmenuFilePathTextBox.Text = foundResourceMap.Value.GetCompactPath(34);
                            AbideRegistry.Halo2Mainmenu = foundResourceMap.Value;
                            break;
                        case ResourceMapFileKind.Shared:
                            halo2SharedFilePathTextBox.Text = foundResourceMap.Value.GetCompactPath(34);
                            AbideRegistry.Halo2Mainmenu = foundResourceMap.Value;
                            break;
                        case ResourceMapFileKind.SPShared:
                            halo2SpSharedFilePathTextBox.Text = foundResourceMap.Value.GetCompactPath(34);
                            AbideRegistry.Halo2SpShared = foundResourceMap.Value;
                            break;
                    }
        }

        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Represents an enumerator containing resource map types.
        /// </summary>
        private enum ResourceMapFileKind
        {
            /// <summary>
            /// mainmenu.map
            /// </summary>
            Mainmenu,
            /// <summary>
            /// shared.map
            /// </summary>
            Shared,
            /// <summary>
            /// single_player_shared.map
            /// </summary>
            SPShared
        };
    }
}
