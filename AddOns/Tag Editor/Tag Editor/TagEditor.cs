using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abide.AddOnApi.Halo2;

namespace Tag_Editor
{
    public partial class TagEditor : AbideTool
    {
        public TagEditor()
        {
            InitializeComponent();
        }

        private void TagEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
        }
    }
}
