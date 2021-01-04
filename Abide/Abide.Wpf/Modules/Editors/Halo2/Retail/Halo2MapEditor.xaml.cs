using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using System.IO;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.Retail
{
    /// <summary>
    /// Interaction logic for Halo2MapEditor.xaml
    /// </summary>
    [AddOn]
    public partial class Halo2MapEditor : FileEditorControl
    {
        public Halo2MapEditor()
        {
            InitializeComponent();
        }
        public override bool IsValidEditor(string path)
        {
            //Prepare
            bool succeeded;

            //Try to load file and perform checks
            try
            {
                //Open file
                using (FileStream fs = File.OpenRead(path))
                {
                    //Check length
                    if (fs.Length < 6144) return false;

                    //Create reader
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        //Read parts of header
                        fs.Seek(0, SeekOrigin.Begin);
                        TagFourCc head = reader.Read<TagFourCc>();
                        uint version = reader.ReadUInt32();
                        uint fileLength = reader.ReadUInt32();
                        fs.Seek(720, SeekOrigin.Begin);
                        uint checksum = reader.ReadUInt32();
                        fs.Seek(2044, SeekOrigin.Begin);
                        TagFourCc foot = reader.Read<TagFourCc>();

                        //Check header...
                        if (head != HaloTags.head || foot != HaloTags.foot)
                            return false;
                        else if (version != 8)
                            return false;
                        else if (fileLength != fs.Length)
                            return false;
                        else if (checksum == 0)
                            return false;

                        //Return
                        return true;
                    }
                }
            }
            catch { succeeded = false; }

            //Return
            return succeeded;
        }
        public override void Load(string path)
        {
            //Prepare
            HaloMapViewModel model = null;

            //Base procedures
            base.Load(path);

            //Check
            if (File.Exists(path))
                model = new HaloMapViewModel(new HaloMap(path));

            //Set DataContext
            DataContext = model ?? new HaloMapViewModel();
        }

        private void ToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Check
            if (sender is FrameworkElement element && element.DataContext is ToolAddOn tool)
                ToolContent.Content = tool.Tool.Element;
        }
    }
}
