using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Beta;
using Abide.HaloLibrary.Halo2BetaMap;
using System.IO;

namespace Abide.Wpf.Modules.Editors.Halo2.Beta
{
    /// <summary>
    /// Interaction logic for Halo2BetaMapEditor.xaml
    /// </summary>
    [AddOn]
    public partial class Halo2BetaMapEditor : FileEditorControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo2BetaMapEditor"/> class.
        /// </summary>
        public Halo2BetaMapEditor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Determines if this <see cref="IFileEditor"/> is valid for a specified file.
        /// </summary>
        /// <param name="path">The path of the file name.</param>
        /// <returns>your mum lul</returns>
        public override bool IsValidEditor(string path)
        {
            //Prepare
            bool succeeded = true;

            try
            {
                //Open file
                using (FileStream fs = File.OpenRead(path))
                {
                    //Check length
                    if (fs.Length < 6144) succeeded = false;

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
                            succeeded = false;
                        else if (version != 8)
                            succeeded = false;
                        else if (fileLength != fs.Length)
                            succeeded = false;
                        else if (checksum != 0)
                            succeeded = false;
                    }
                }
            }
            catch { succeeded = false; }

            //Return
            return succeeded;
        }
        /// <summary>
        /// Loads the specified file into the editor.
        /// </summary>
        /// <param name="path">The path of the file name.</param>
        public override void Load(string path)
        {
            base.Load(path);

            if (File.Exists(path))
            {
                HaloMapFile mapFile = HaloMapFile.Load(path);

                HaloMapViewModel model = new HaloMapViewModel(mapFile);
                DataContext = model;
            }
            else DataContext = new HaloMapViewModel();
        }
    }
}
