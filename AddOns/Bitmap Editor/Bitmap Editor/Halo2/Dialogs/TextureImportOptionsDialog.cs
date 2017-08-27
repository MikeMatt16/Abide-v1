using System;
using System.Windows.Forms;

namespace Bitmap_Editor.Halo2.Dialogs
{
    public partial class TextureImportOptionsDialog : Form
    {
        private const int LodMax = 6;
        
        public bool Swizzle
        {
            get { return swizzleCheckBox.Checked; }
        }
        public int OriginalWidth
        {
            get { return originalWidth; }
            set { originalWidth = value; swizzle_Check(); }
        }
        public int OriginalHeight
        {
            get { return originalHeight; }
            set { originalHeight = value; swizzle_Check(); }
        }
        public int MaxLodLevels
        {
            get { return (int)lodLevelUpDown.Maximum; }
            set { lodLevelUpDown.Maximum = Math.Min(LodMax, value); }
        }
        public int LodLevels
        {
            get { return (int)lodLevelUpDown.Value; }
            set { lodLevelUpDown.Value = Math.Min(MaxLodLevels, value); }
        }
        public HaloBitmap.BitmapFormat Format
        {
            get { return (HaloBitmap.BitmapFormat)formatComboBox.SelectedItem; }
            set { formatComboBox.SelectedItem = value; }
        }
        public bool DeleteLods
        {
            get { return deleteLodsCheckBox.Checked; }
            set { deleteLodsCheckBox.Checked = value; }
        }

        private int originalHeight = 0;
        private int originalWidth = 0;

        public TextureImportOptionsDialog()
        {
            InitializeComponent();

            //Setup Format
            foreach (var enumValue in Enum.GetValues(typeof(HaloBitmap.BitmapFormat)))
                if (!HaloBitmap.BitmapFormat.Null.Equals(enumValue)) formatComboBox.Items.Add(enumValue);
            formatComboBox.SelectedItem = HaloBitmap.BitmapFormat.A8r8g8b8;
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check
            swizzle_Check();
        }

        private void swizzle_Check()
        {
            //Prepare
            bool swizzle = true;

            //Check
            switch (Format)
            {
                case HaloBitmap.BitmapFormat.Dxt1:
                case HaloBitmap.BitmapFormat.Dxt3:
                case HaloBitmap.BitmapFormat.Dxt5: swizzle = false; break;
            }

            //Check width / height
            if (swizzle) swizzle &= Math.Log(originalHeight, 2) % 1 == 0 && Math.Log(originalHeight, 2) % 1 == 0;

            //Check
            swizzleCheckBox.Enabled = swizzle;
            swizzleCheckBox.Checked = swizzle;
        }
    }
}
