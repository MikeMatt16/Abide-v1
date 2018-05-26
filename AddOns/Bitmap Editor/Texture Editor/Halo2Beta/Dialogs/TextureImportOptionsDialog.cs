using System;
using System.Windows.Forms;

namespace Texture_Editor.Halo2Beta.Dialogs
{
    public partial class TextureImportOptionsDialog : Form
    {
        private const int LodMax = 6;
        
        public int OriginalWidth
        {
            get { return originalWidth; }
            set { originalWidth = value; }
        }
        public int OriginalHeight
        {
            get { return originalHeight; }
            set { originalHeight = value; }
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
    }
}
