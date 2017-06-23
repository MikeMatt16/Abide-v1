using Abide.AddOnApi;
using Abide.Classes;
using System;
using System.Windows.Forms;

namespace Abide.Dialogs
{
    public partial class OptionsDialog : Form, IHost
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
                openDlg.Filter = "SP Shared Maps (singleplayershared.map)|singleplayershared.map";
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
                //Set...
                halo2MainmenuFilePathTextBox.Text = filename.GetCompactPath(34);
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
                //Set...
                halo2MainmenuFilePathTextBox.Text = filename.GetCompactPath(34);
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
                //Set...
                halo2MainmenuFilePathTextBox.Text = filename.GetCompactPath(34);
                AbideRegistry.Halo2Mainmenu = filename;
            }
        }

        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
