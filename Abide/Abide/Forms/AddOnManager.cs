using Abide.AddOnApi;
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

        }
    }
}
