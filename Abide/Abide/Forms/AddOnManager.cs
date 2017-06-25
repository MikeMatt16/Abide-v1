using Abide.AddOnApi;
using Abide.Classes;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Forms
{
    public partial class AddOnManager : Form
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
            AddOnFactory[] factories = new AddOnFactory[addOnListView.SelectedItems.Count];

            //Get selected factories...
            foreach (int i in addOnListView.SelectedIndices)
            {
                //Set...
                if (addOnListView.Items[i].Tag is AddOnFactory)
                    factories[i] = (AddOnFactory)addOnListView.Items[i].Tag;

                //Remove...
                addOnListView.Items.RemoveAt(i);
            }

            //Remove
            foreach (AddOnFactory factory in factories)
                Program.Container.RemoveDirectory(factory.AddOnDirectory);

            //Delete Directories?
            bool failed = false;
            try { foreach (AddOnFactory factory in factories) { Directory.Delete(factory.AddOnDirectory, true); } }
            catch { failed |= true; }

            //Check
            if (failed) if (MessageBox.Show("The directories cannot be deleted. The application must be in safe-mode to delete. " +
                "Would you like to restart the program in safe mode?", "Restart required",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { Security.Restart("-s"); Application.Exit(); }
        }
    }
}
