using Abide.AddOnApi;
using Abide.AddOnApi.Halo2Beta;
using System;
using Texture_Editor.Halo2Beta.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texture_Editor.Halo2Beta
{
    /// <summary>
    /// Represents a <see cref="NewTextureButton"/>
    /// </summary>
    [AddOn]
    public sealed class NewTextureButton : AbideMenuButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTextureButton"/> class.
        /// </summary>
        public NewTextureButton() : base()
        {
            Author = "Click16";
            Name = "New Texture";
            Icon = Properties.Resources.new_texture;
            Click += NewTextureButton_Click;
        }

        private void NewTextureButton_Click(object sender, EventArgs e)
        {
            //Setup
            using (NewTextureDialog dlg = new NewTextureDialog())
            {
                //Initialize
                dlg.Initialize(Map);

                //Show
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }
    }
}
