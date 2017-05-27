using Abide.AddOnApi;
using Abide.Classes;
using Abide.HaloLibrary.Halo2Map;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.Forms
{
    public partial class AddOnManager : Form, IHost
    {
        private readonly AddOnContainer<MapFile, IndexEntry, Xbox> container;

        public AddOnManager()
        {
            InitializeComponent();

            //Initialize Container
            container = new AddOnContainer<MapFile, IndexEntry, Xbox>(HaloLibrary.MapVersion.Halo2);

            //Load AddOns
            AddOnManifest manifest = new AddOnManifest();
            foreach (string directory in Directory.EnumerateDirectories(RegistrySettings.AddOnsDirectory))
            {
                //Get Manifest Path
                manifest.LoadXml(Path.Combine(directory, "Manifest.xml"));

                //Load
                string assemblyPath = Path.Combine(directory, manifest.PrimaryAssemblyFile);
                if (File.Exists(assemblyPath)) container.AddAssemblySafe(assemblyPath);
            }

            //Initialize AddOns
            container.BeginInit(this);

            //Loop
            foreach (var factory in container.GetFactories())
            {
                DirectoryInfo info = new DirectoryInfo(factory.AddOnDirectory);
                ListViewItem item = addOnListView.Items.Add(info.Name);
                item.SubItems.Add(string.Join(", ", factory.GetAddOnTypes().Select(t => t.Name)));
                item.SubItems.Add(factory.AddOnDirectory);
            }
        }

        public object Request(IAddOn sender, string request, params object[] args)
        {
            return null;
        }
    }
}
