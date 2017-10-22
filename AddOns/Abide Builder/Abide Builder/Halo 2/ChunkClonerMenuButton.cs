using Abide.AddOnApi.Halo2;
using System;
using System.Windows.Forms;

namespace Abide.Builder.Halo_2
{
    /// <summary>
    /// Represents the chunk cloner menu button.
    /// </summary>
    public sealed class ChunkClonerMenuButton : AbideContextMenuButton
    {
        public ChunkClonerMenuButton() : base()
        {
            //
            // this
            //
            this.Icon = Properties.Resources.ChunkCloner;
            this.Name = "Chunk Cloner";
            this.Description = "Tag structure editor.";
            this.Author = "Click16";
            this.Click += ChunkClonerMenuButton_Click;
        }

        private void ChunkClonerMenuButton_Click(object sender, EventArgs e)
        {
            //Check
            if(SelectedEntry != null)
            {
                //Initialize
                ChunkCloner chunkCloner = new ChunkCloner(Map, SelectedEntry);
                chunkCloner.FormClosed += ChunkCloner_FormClosed;

                //Show
                chunkCloner.Show();
            }
        }

        private void ChunkCloner_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Dispose
            ((IDisposable)sender).Dispose();
        }
    }
}
