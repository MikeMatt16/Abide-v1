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
                DirectoryInfo info = new DirectoryInfo(factory.AddOnDirectory);
                ListViewItem item = addOnListView.Items.Add(info.Name);
                item.SubItems.Add(string.Join(", ", factory.GetAddOnTypes().Select(t => t.Name)));
                item.SubItems.Add(factory.AddOnDirectory);
            }
        }
    }
}
