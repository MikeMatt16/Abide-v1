using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abide.Tag;
using Abide.HaloLibrary;
using Abide.Tag.Cache.Generated;

namespace Guerilla
{
    public partial class GuerillaEditor : UserControl
    {
        public GuerillaEditor()
        {
            InitializeComponent();
        }
        public void Prepare(TagFourCc tag)
        {
            //Create group
            ITagGroup group = TagLookup.CreateTagGroup(tag);
            if (group != null) Prepare(group); //Prepare
        }
        public void Prepare(ITagGroup group)
        {

        }
    }
}
