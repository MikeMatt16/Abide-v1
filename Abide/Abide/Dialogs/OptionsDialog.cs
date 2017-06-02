using Abide.AddOnApi;
using Abide.Classes;
using Abide.HaloLibrary;
using System.IO;
using System.Windows.Forms;
using System;

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
        }
        
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
