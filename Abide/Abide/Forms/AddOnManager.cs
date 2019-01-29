using Abide.AddOnApi;
using Abide.Classes;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Forms
{
    internal partial class AddOnManager : Form
    {
        public AddOnManager()
        {
            InitializeComponent();

            //Check
            if (!Security.IsAdmin()) Security.ElevateButton(deleteButton);

            //Loop
            foreach (var factory in Program.Container.GetFactories())
            {
                //Get Name
                string name = factory.ToString();
                if (Directory.Exists(factory.AddOnDirectory)) name = new DirectoryInfo(factory.AddOnDirectory).Name;

                //Create Item
                ListViewItem item = addOnListView.Items.Add(name);
                item.SubItems.Add(string.Join(", ", factory.GetAddOnTypes().Select(t => t.Name)));
                item.SubItems.Add(factory.AddOnDirectory);
                item.Tag = factory;
            }
        }

        private void openAddOnsFolderButton_Click(object sender, EventArgs e)
        {
            //Check
            if (Directory.Exists(AbideRegistry.AddOnsDirectory))
                System.Diagnostics.Process.Start(AbideRegistry.AddOnsDirectory);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addOnListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Prepare
            bool enable = false;

            //Get selected factories...
            for (int i = 0; i < addOnListView.SelectedItems.Count; i++)
            {
                if (addOnListView.SelectedItems[i].Tag is AddOnFactory)
                    enable |= true;
            }

            //Enable?
            deleteButton.Enabled = enable;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //Prepare
            int count = addOnListView.SelectedItems.Count;
            AddOnFactory[] factories = new AddOnFactory[count];
            ListViewItem[] items = new ListViewItem[count];

            //Loop through selected items
            for (int i = 0; i < count; i++)
            {
                items[i] = addOnListView.Items[addOnListView.SelectedIndices[i]];
                if (items[i].Tag is AddOnFactory factory)
                    factories[i] = factory;
            }
            
            //Remove Directories
            foreach (AddOnFactory factory in factories)
                Program.Container.RemoveDirectory(factory.AddOnDirectory);

            //Delete Directories?
            bool failed = false;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    Directory.Delete(factories[i].AddOnDirectory, true);
                    addOnListView.Items.Remove(items[i]);
                }
            }
            catch { failed |= true; }

            //Check
            if (failed) if (MessageBox.Show("The directories cannot be deleted. The application must be in safe-mode to delete. " +
                "Would you like to restart the program in safe mode?", "Restart required",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { Security.Restart("-s"); Application.Exit(); }
        }
    }
}
