using Abide.Compiler;
using Abide.Decompiler;
using Abide.Guerilla.Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public partial class AbideGuerilla : Form
    {
        public AbideGuerilla()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void decompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CacheDecompiler instance...
            using (CacheDecompiler decompiler = new CacheDecompiler() { Icon = Properties.Resources.abide_icon, StartPosition = FormStartPosition.CenterParent })
                decompiler.ShowDialog();    //Show
        }

        private void compilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize CaceCompiler instance...
            using (CacheCompiler compiler = new CacheCompiler() { Icon = Properties.Resources.abide_icon, StartPosition = FormStartPosition.CenterParent })
                compiler.ShowDialog();  //Show
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            TagGroupFile file = null;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "All files (*.*)|*.*";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
#if DEBUG
                    //Load file
                    file = new TagGroupFile();
                    file.Load(openDlg.FileName);
#else
                    try
                    {
                        using (Stream stream = openDlg.OpenFile())
                        {
                            //Load file
                            file = new TagGroupFile();
                            file.Load(stream);
                        }
                    }
                    catch { file = null; MessageBox.Show($"An error occured while opening {openDlg.FileName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } 
#endif
                }
            }
        }
    }
}
