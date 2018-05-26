using Abide.HaloLibrary.Halo2BetaMap;
using System.Windows.Forms;

namespace Texture_Editor.Halo2Beta.Dialogs
{
    /// <summary>
    /// Represents a new texture dialog for Halo 2 beta maps.
    /// </summary>
    public partial class NewTextureDialog : Form
    {
        private MapFile map;

        /// <summary>
        /// Creates a new instance of the <see cref="NewTextureDialog"/> class.
        /// </summary>
        public NewTextureDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes the dialog, preparing to accept a new texture.
        /// </summary>
        /// <param name="map">The map file.</param>
        public void Initialize(MapFile map)
        {
            //Set
            this.map = map;
        }
    }
}
