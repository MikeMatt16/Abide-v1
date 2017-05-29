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
        private readonly SettingsPageContainer container;

        public OptionsDialog()
        {
            InitializeComponent();

            //Initialize
            container = new SettingsPageContainer();

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(RegistrySettings.AddOnsDirectory))
            {
                //Get Manifest Path
                manifest.LoadXml(Path.Combine(directory, "Manifest.xml"));

                //Load
                string assemblyPath = Path.Combine(directory, manifest.PrimaryAssemblyFile);
                if (File.Exists(assemblyPath)) container.AddAssembly(assemblyPath);
            }

            //Initialize AddOns
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

        bool IHost.InvokeRequired
        {
            get { return InvokeRequired; }
        }
        object IHost.Invoke(Delegate method)
        {
            return Invoke(method);
        }
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
