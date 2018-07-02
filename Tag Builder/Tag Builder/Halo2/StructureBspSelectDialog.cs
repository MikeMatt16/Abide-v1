using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System.IO;
using System.Windows.Forms;

namespace Abide.TagBuilder.Halo2
{
    public partial class StructureBspSelectDialog : Form
    {
        public int SelectedBlockIndex
        {
            get { if (structureBspBlockComboBox.SelectedItem is StructureBspObject bsp) return bsp.Index; return 0; }
        }

        public StructureBspSelectDialog(IndexEntry scenario) : this()
        {
            //Prepare
            Block bspReference = new ScenarioStructureBspReferenceBlock();

            //Check
            if (scenario != null)
                using (BinaryReader reader = scenario.TagData.CreateReader())   //Create reader
                {
                    //Read scenario structure bsp reference tag block
                    scenario.TagData.Seek(scenario.Offset + 528, SeekOrigin.Begin);
                    int sbspRefsCount = reader.ReadInt32();
                    int sbspRefsOffset = reader.ReadInt32();
                    if (sbspRefsCount > 0)
                    {
                        //Goto
                        scenario.TagData.Seek(sbspRefsOffset, SeekOrigin.Begin);

                        //Loop
                        for (int i = 0; i < sbspRefsCount; i++)
                        {
                            //Read reference
                            bspReference.Read(reader);

                            //Add
                            StructureBspObject bspObject = new StructureBspObject(bspReference.ToString(), i);
                            structureBspBlockComboBox.Items.Add(bspObject);
                        }
                    }
                }
        }
        private StructureBspSelectDialog()
        {
            InitializeComponent();
            
            //Add none
            StructureBspObject none = new StructureBspObject("none", 0);
            structureBspBlockComboBox.Items.Add(none);

            //Select
            structureBspBlockComboBox.SelectedIndex = 0;
        }

        private class StructureBspObject
        {
            public int Index { get; }
            public string Name { get; }

            public StructureBspObject(string name, int index)
            {
                Index = index;
                Name = name;
            }
            public override string ToString()
            {
                return Name;
            }
        }
    }
}
